using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MME.Data;
using MME.Model.Shared;

namespace MME.Web.Controllers
{
    [Authorize(Policy = "MMECookieScheme")]
    public class EventsController : Controller
    {
        readonly MMEAppDBContext _context;
        readonly IConfiguration _iconfiguration;

        public EventsController(MMEAppDBContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        public IActionResult All()
        {
            if (TempData["SuccessMessage"] != null && !string.IsNullOrEmpty(TempData["SuccessMessage"].ToString()))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EventModel { EventTypes = _context.EventTypes.Where(e => e.IsActive).ToList(), EventDate = DateTime.Now, ActivationDate = DateTime.Now });
        }

        [HttpPost]
        public IActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var bannername = Guid.NewGuid().ToString();
                var fileUpload = false;
                if (model.BannerImage != null && model.BannerImage.Length > 0)
                {
                    var thumbwidth = Convert.ToInt16(_iconfiguration["thumbsize"].ToString());
                    var thumbheight = Convert.ToInt16(_iconfiguration["thumbsize"].ToString());
                    var eventsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["eventpics"].ToString());
                    var eventsThumbsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["eventpicthumbs"].ToString());
                    bannername = bannername + System.IO.Path.GetExtension(model.BannerImage.FileName);
                    var eventfilename = Path.Combine(eventsFolderPath, bannername);
                    var thumbfilename = Path.Combine(eventsThumbsFolderPath, bannername);
                    using (Stream fileStream = new FileStream(eventfilename, FileMode.Create, FileAccess.Write))
                    {
                        model.BannerImage.CopyTo(fileStream);
                    }

                    var file = new FileInfo(eventfilename);
                    using (MagickImage image = new MagickImage(file))
                    {
                        {
                            image.Thumbnail(new MagickGeometry(thumbwidth, thumbheight));
                            image.Write(thumbfilename);
                        }
                    }
                    fileUpload = true;
                }
                if (fileUpload)
                    model.Banner = bannername;
                model.IsActive = true;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = Guid.Parse("EDA55024-DBBA-4EF9-ACD3-08DB8CEB09E8");
                _context.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Event added successfully";
                return RedirectToAction("all");
            }
            return View();
        }

        public IActionResult Read(string draw, string start, int length)
        {
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var events = GetEvents();

            //Sorting  
            if ((!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)))
            {
                var sortValue = typeof(EventModel).GetProperty(sortColumn.ToUpper(), System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.Public);
                if (sortValue != null)
                {
                    if (sortColumnDirection == "asc")
                        events = events.OrderBy(c => sortValue.GetValue(c, null)).ToList();
                    else
                        events = events.OrderByDescending(c => sortValue.GetValue(c, null)).ToList();
                }
            }

            //Search  
            if (!string.IsNullOrEmpty(searchValue))
            {
                events = events.Where(m => m.Event.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            //total number of rows count 
            recordsTotal = events.Count;
            //Paging   

            if (string.IsNullOrEmpty(sortColumn))
            {
                events = events.Skip(skip).Take(length).OrderByDescending(m => m.CreatedDate).ToList();
            }

            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = events });
        }

        private List<EventModel> GetEvents()
        {
            return _context.Events.Include("EventType").Where(u => u.IsActive == true).ToList();
        }
    }
}
