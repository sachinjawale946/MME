using FirebaseAdmin.Messaging;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MME.Data;
using MME.Model.Lookups;
using MME.Model.Shared;
using System.Linq;

namespace MME.Web.Controllers
{

    public class EventsController : BaseController
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
                model.CreatedBy = GetUserId();
                _context.Add(model);
                _context.SaveChanges();
                SendEventNotification(model.EventId, NotificationType_Lookup.Create);
                TempData["SuccessMessage"] = "Event added successfully";
                return RedirectToAction("all");
            }
            return View();
        }

        [HttpGet("/Events/Edit/{Id}")]
        public IActionResult Edit(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var model = _context.Events.Where(e => e.EventId == Id).FirstOrDefault();
                if (model != null)
                {
                    model.EventTypes = _context.EventTypes.Where(e => e.IsActive).ToList();
                    if (!string.IsNullOrEmpty(model.Banner))
                    {
                        var eventsFolderPath = _iconfiguration["eventpics"].ToString();
                        model.BannerUrl = Path.Combine(eventsFolderPath, model.Banner);
                    }
                }
                return View(model);
            }
            else
            {
                return View(new EventModel { EventTypes = _context.EventTypes.Where(e => e.IsActive).ToList() });
            }
        }

        [HttpPost]
        public IActionResult Edit(EventModel model)
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

                if (!string.IsNullOrEmpty(model.Banner) && string.IsNullOrEmpty(model.BannerUrl))
                    model.Banner = string.Empty;

                if (fileUpload)
                    model.Banner = bannername;
                model.IsActive = true;
                model.LastUpdatedDate = DateTime.Now;
                model.LastUpdatedBy = GetUserId();
                _context.Update(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Event updated successfully";
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

            //total number of rows count 
            var events = GetEvents(searchValue, skip, length, out recordsTotal);

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

            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = events });
        }

        private List<EventModel> GetEvents(string SearchFilter, int skip, int length, out int recordsTotal)
        {
            var events = new List<EventModel>();
            if (!string.IsNullOrEmpty(SearchFilter))
                events = _context.Events.Include("EventType").Where(u => u.IsActive == true && u.Event.ToLower().Contains(SearchFilter.ToLower())).ToList();
            else
                events = _context.Events.Include("EventType").Where(u => u.IsActive == true).ToList();

            recordsTotal = events.Count;

            return events.Skip(skip).Take(length).OrderByDescending(m => m.CreatedDate).ToList();
        }

        [HttpGet("/Events/SendNotification/{EventId}/{NotificationType}/{NotificationTitle?}")]
        public IActionResult SendNotification(Guid EventId, string NotificationType, string NotificationTitle = "")
        {
            return Json(SendEventNotification(EventId, NotificationType, NotificationTitle));
        }

        private string SendEventNotification(Guid EventId, string NotificationType, string NotificationTitle = "")
        {
            if (EventId != Guid.Empty)
            {
                var Title = string.Empty;

                if (NotificationType == NotificationType_Lookup.Create)
                {
                    Title = (string.IsNullOrEmpty(NotificationTitle)) ? "New Event Added" : NotificationTitle;
                }
                else if (NotificationType == NotificationType_Lookup.Reminder)
                {
                    Title = (string.IsNullOrEmpty(NotificationTitle)) ? "Event Reminder" : NotificationTitle;
                }
                else if (NotificationType == NotificationType_Lookup.Cancelled)
                {
                    Title = (string.IsNullOrEmpty(NotificationTitle)) ? "Event Cancelled" : NotificationTitle;
                }
                else if (NotificationType == NotificationType_Lookup.Postponed)
                {
                    Title = (string.IsNullOrEmpty(NotificationTitle)) ? "Event Postponed" : NotificationTitle;
                }
                else if (NotificationType == NotificationType_Lookup.Rescheduled)
                {
                    Title = (string.IsNullOrEmpty(NotificationTitle)) ? "Event Rescheduled" : NotificationTitle;
                }

                var profilesFolderPath = _iconfiguration["alleventimages"].ToString();
                var defaulteventimage = _iconfiguration["defaulteventimage"].ToString();
                var eventDetails = _context.Events.Where(e => e.IsActive == true & e.EventId == EventId).FirstOrDefault();
                if (eventDetails != null)
                {
                    IReadOnlyList<string> tokens = _context.Users.Where(u => u.IsActive && !string.IsNullOrEmpty(u.FCMToken)).Select(f => f.FCMToken).ToList();

                    if (tokens != null && tokens.Count > 0)
                    {
                        var message = new MulticastMessage()
                        {
                            Notification = new Notification
                            {
                                Title = eventDetails.Event,
                                // Body = eventDetails.Event,
                                ImageUrl = (string.IsNullOrEmpty(eventDetails.Banner)) ? defaulteventimage : profilesFolderPath + eventDetails.Banner,
                            },
                            Android = new AndroidConfig()
                            {
                                Notification = new AndroidNotification()
                                {
                                    Sound = "default",
                                    Priority = NotificationPriority.MAX
                                }
                            },
                            Apns = new ApnsConfig()
                            {
                                Aps = new Aps()
                                {
                                    Sound = "default"
                                }
                            },
                            Tokens = tokens,
                        };
                        var messaging = FirebaseMessaging.DefaultInstance;
                        var result = messaging.SendMulticastAsync(message).Result;
                        return Api_Result_Lookup.Success;
                    }
                    return Api_Result_Lookup.Error;
                }
                return Api_Result_Lookup.Error;
            }
            return Api_Result_Lookup.Error;
        }

    }
}
