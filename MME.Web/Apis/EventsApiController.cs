using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var EventResponses = new List<EventResponseModel>();

                var events = _context.Events.Where(c => c.IsActive && c.ActivationDate <= DateTime.Now && (c.Event.ToLower().Contains(model.eventname.ToLower())))
                        .OrderByDescending(c => c.CreatedDate).ThenBy(c => c.Event)
                        .Skip((model.page - 1) * model.pagesize)
                        .Take(model.pagesize).ToList();

                if (events.Any())
                {
                    foreach (var item in events)
                    {
                        var respose = new EventResponseModel
                        {
                            eventid = item.EventId,
                            description = item.Description,
                            header = item.Event,
                            location = (string.IsNullOrEmpty(item.Location)) ? string.Empty : item.Location,
                            eventdate = item.EventDate,
                            banner = (string.IsNullOrEmpty(item.Banner)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, item.Banner)),
                        };
                        var EventFeedbacks = _context.EventFeedbacks.Where(e => e.EventId == item.EventId);
                        if (EventFeedbacks.Any())
                        {
                            var EventFeedback = _context.EventFeedbacks.Where(e => e.UserId == model.userid && e.EventId == item.EventId).FirstOrDefault();
                            if (EventFeedback != null)
                            {
                                respose.EventFeedback = new EventFeedbackResponseModel
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
                            respose.likes = EventFeedbacks.Where(c => c.Liked != null && c.Liked == true).Count();
                            respose.dislikes = EventFeedbacks.Where(c => c.DisLiked != null && c.DisLiked == true).Count();
                            respose.spams = EventFeedbacks.Where(c => c.ReportAbuse != null && c.ReportAbuse == true).Count();
                            // respose.participations = EventFeedbacks.Where(c => c.Participation != null && c.Participation == true).Count();
                            respose.donations = Convert.ToDecimal(EventFeedbacks.Sum(c => c.Donation));
                            respose.suggestions = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Suggestion)).Count();
                            respose.feedbacks = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Feedback)).Count();
                        }
                        EventResponses.Add(respose);
                    }
                }

                return EventResponses;
            }
            else
            {
                var EventResponses = new List<EventResponseModel>();

                var events = _context.Events.Where(c => c.IsActive && c.ActivationDate <= DateTime.Now)
                       .OrderByDescending(c => c.CreatedDate).ThenBy(c => c.Event)
                       .Skip((model.page - 1) * model.pagesize)
                       .Take(model.pagesize).ToList();

                if (events.Any())
                {
                    foreach (var item in events)
                    {
                        var respose = new EventResponseModel
                        {
                            eventid = item.EventId,
                            description = item.Description,
                            header = item.Event,
                            location = (string.IsNullOrEmpty(item.Location)) ? string.Empty : item.Location,
                            eventdate = item.EventDate,
                            banner = (string.IsNullOrEmpty(item.Banner)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, item.Banner)),
                        };
                        var EventFeedbacks = _context.EventFeedbacks.Where(e => e.EventId == item.EventId);
                        if (EventFeedbacks.Any())
                        {
                            var EventFeedback = _context.EventFeedbacks.Where(e => e.UserId == model.userid && e.EventId == item.EventId).FirstOrDefault();
                            if (EventFeedback != null)
                            {
                                respose.EventFeedback = new EventFeedbackResponseModel
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
                            respose.likes = EventFeedbacks.Where(c => c.Liked != null && c.Liked == true).Count();
                            respose.dislikes = EventFeedbacks.Where(c => c.DisLiked != null && c.DisLiked == true).Count();
                            respose.spams = EventFeedbacks.Where(c => c.ReportAbuse != null && c.ReportAbuse == true).Count();
                            // respose.participations = EventFeedbacks.Where(c => c.Participation != null && c.Participation == true).Count();
                            respose.donations = Convert.ToDecimal(EventFeedbacks.Sum(c => c.Donation));
                            respose.suggestions = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Suggestion)).Count();
                            respose.feedbacks = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Feedback)).Count();
                        }
                        EventResponses.Add(respose);
                    }
                }

                return EventResponses;
            }
        }


    }
}
