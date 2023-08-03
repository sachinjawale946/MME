using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            var EventResponses = new List<EventResponseModel>();
            if (model.page == 0) model.page = 1;
            if (model.pagesize == 0) model.pagesize = Convert.ToInt16(_iconfiguration["eventpagesize"].ToString());

            if (!string.IsNullOrEmpty(model.eventname))
            {
                var events = _context.Events.Where(c => c.IsActive && c.ActivationDate <= DateTime.Now && (c.Event.ToLower().Contains(model.eventname.ToLower())))
                        .OrderByDescending(c => c.CreatedDate).ThenBy(c => c.Event)
                        .Skip((model.page - 1) * model.pagesize)
                        .Take(model.pagesize).ToList();

                if (events.Any())
                {
                    foreach (var item in events)
                    {
                        EventResponses.Add(returnReponseItem(item, model.userid));
                    }
                }

                return EventResponses;
            }
            else
            {
                var events = _context.Events.Where(c => c.IsActive && c.ActivationDate <= DateTime.Now)
                       .OrderByDescending(c => c.CreatedDate).ThenBy(c => c.Event)
                       .Skip((model.page - 1) * model.pagesize)
                       .Take(model.pagesize).ToList();

                if (events.Any())
                {
                    foreach (var item in events)
                    {
                        EventResponses.Add(returnReponseItem(item, model.userid));
                    }
                }

                return EventResponses;
            }
        }

        private EventResponseModel returnReponseItem(MME.Model.Shared.EventModel item, Guid userid)
        {
            // var profilesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["eventpicthumbs"].ToString());
            var profilesFolderPath = _iconfiguration["alleventimages"].ToString();
            var defaulteventimage = _iconfiguration["defaulteventimage"].ToString();

            var response = new EventResponseModel
            {
                eventid = item.EventId,
                event_type_id = item.EventTypeId,
                description = item.Description,
                header = item.Event,
                location = (string.IsNullOrEmpty(item.Location)) ? string.Empty : item.Location,
                eventdate = item.EventDate,
                // banner = (string.IsNullOrEmpty(item.Banner)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, item.Banner)),
                bannerurl = (string.IsNullOrEmpty(item.Banner)) ? defaulteventimage : profilesFolderPath + item.Banner,
            };
            var EventFeedbacks = _context.EventFeedbacks.Where(e => e.EventId == item.EventId);
            if (EventFeedbacks.Any())
            {
                var EventFeedback = _context.EventFeedbacks.Where(e => e.UserId == userid && e.EventId == item.EventId).FirstOrDefault();
                if (EventFeedback != null)
                {
                    response.EventFeedback = new EventFeedbackResponseModel
                    {
                        disliked = EventFeedback.DisLiked,
                        donation = EventFeedback.Donation,
                        eventid = EventFeedback.EventId,
                        feedback = EventFeedback.Feedback,
                        id = EventFeedback.Id,
                        liked = EventFeedback.Liked,
                        reportabuse = EventFeedback.ReportAbuse,
                        suggestion = EventFeedback.Suggestion,
                        userid = EventFeedback.UserId,
                    };
                }

                // counts 
                response.likes = EventFeedbacks.Where(c => c.Liked != null && c.Liked == true).Count();
                response.dislikes = EventFeedbacks.Where(c => c.DisLiked != null && c.DisLiked == true).Count();
                response.spams = EventFeedbacks.Where(c => c.ReportAbuse != null && c.ReportAbuse == true).Count();
                // respose.participations = EventFeedbacks.Where(c => c.Participation != null && c.Participation == true).Count();
                response.donations = Convert.ToDecimal(EventFeedbacks.Sum(c => c.Donation));
                response.suggestions = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Suggestion)).Count();
                response.feedbacks = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Feedback)).Count();
            }
            return response;
        }
    }
}
