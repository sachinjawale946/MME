using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MME.Data;
using MME.Model.Request;
using MME.Model.Response;
using MME.Web.Filters;
using System.Reflection;

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    [Authorize(Policy = "MMEJwtScheme")]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class EventsApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;
        readonly IConfiguration _iconfiguration;

        public EventsApiController(MMEAppDBContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        [HttpPost, Route("~/api/v1/events-search")]
        public List<EventResponseModel> Search(EventRequestModel model)
        {
            var profilesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["eventpics"].ToString());

            if (model.page == 0) model.page = 1;
            if (model.pagesize == 0) model.pagesize = Convert.ToInt16(_iconfiguration["eventpagesize"].ToString());

            if (!string.IsNullOrEmpty(model.eventname))
            {
                return _context.Events.Where(c => c.IsActive && c.ActivationDate <= DateTime.Now && (c.Event.ToLower().Contains(model.eventname.ToLower())))
                        .OrderByDescending(c => c.CreatedDate).ThenBy(c => c.Event)
                        .Skip((model.page - 1) * model.pagesize)
                        .Take(model.pagesize)
                        .Select(o => new EventResponseModel
                        {
                            eventid = o.EventId,
                            description = o.Description,
                            header = o.Event,
                            location = (string.IsNullOrEmpty(o.Location)) ? string.Empty : o.Location,
                            eventdate = o.EventDate,
                            banner = (string.IsNullOrEmpty(o.Banner)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, o.Banner))
                        }).ToList();
            }
            else
            {
                return _context.Events.Where(c => c.IsActive && c.ActivationDate <= DateTime.Now)
                       .OrderByDescending(c => c.CreatedDate).ThenBy(c => c.Event)
                       .Skip((model.page - 1) * model.pagesize)
                       .Take(model.pagesize)
                       .Select(o => new EventResponseModel
                       {
                           eventid = o.EventId,
                           description = o.Description,
                           header = o.Event,
                           location = (string.IsNullOrEmpty(o.Location)) ? string.Empty : o.Location,
                           eventdate = o.EventDate,
                           banner = (string.IsNullOrEmpty(o.Banner)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, o.Banner))
                       }).ToList();
            }
        }


    }
}
