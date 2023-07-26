using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MME.Data;
using MME.Model.Request;
using MME.Model.Response;
using MME.Web.Filters;

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    [Authorize(Policy = "MMEJwtScheme")]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class EventFeedbackApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public EventFeedbackApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpPost, Route("~/api/v1/save-event-feedback")]
        public EventFeedbackResponseModel Post(EventFeedbackResponseModel model)
        {
            if (model != null)
            {
                var eventFeedback = _context.EventFeedbacks.Where(e => e.EventId == model.eventid && e.UserId == model.userid).FirstOrDefault();
                if (eventFeedback != null)
                {
                    eventFeedback.DisLiked = model.disliked;
                    eventFeedback.Donation = model.donation;
                    eventFeedback.Feedback = model.feedback;
                    eventFeedback.Liked = model.liked;
                    eventFeedback.ReportAbuse = model.reportabuse;
                    eventFeedback.Suggestion = model.suggestion;
                    _context.EventFeedbacks.Update(eventFeedback);
                    _context.SaveChanges();

                    return getEventFeedback(model);
                }
                else
                {
                    var feedback = new Model.Shared.EventFeedbackModel
                    {
                        Created = DateTime.Now,
                        DisLiked = model.disliked,
                        Donation = model.donation,
                        EventId = model.eventid,
                        Feedback = model.feedback,
                        Liked = model.liked,
                        ReportAbuse = model.reportabuse,
                        Suggestion = model.suggestion,
                        UserId = model.userid,
                    };
                    _context.EventFeedbacks.Add(feedback);
                    _context.SaveChanges();

                    return getEventFeedback(model);

                }
            }
            else
            {
                return new EventFeedbackResponseModel();
            }
        }

        private EventFeedbackResponseModel getEventFeedback(EventFeedbackResponseModel model)
        {
            var EventFeedbacks = _context.EventFeedbacks.Where(e => e.EventId == model.eventid);
            if (EventFeedbacks.Any())
            {
                var EventFeedback = _context.EventFeedbacks.Where(e => e.UserId == model.userid && e.EventId == model.eventid).FirstOrDefault();
                if (EventFeedback != null)
                {
                    var EventFeedbackResponse = new EventFeedbackResponseModel
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

                    // counts 
                    EventFeedbackResponse.likes = EventFeedbacks.Where(c => c.Liked != null && c.Liked == true).Count();
                    EventFeedbackResponse.dislikes = EventFeedbacks.Where(c => c.DisLiked != null && c.DisLiked == true).Count();
                    EventFeedbackResponse.spams = EventFeedbacks.Where(c => c.ReportAbuse != null && c.ReportAbuse == true).Count();
                    // respose.participations = EventFeedbacks.Where(c => c.Participation != null && c.Participation == true).Count();
                    EventFeedbackResponse.donations = Convert.ToDecimal(EventFeedbacks.Sum(c => c.Donation));
                    EventFeedbackResponse.suggestions = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Suggestion)).Count();
                    EventFeedbackResponse.feedbacks = EventFeedbacks.Where(c => !string.IsNullOrEmpty(c.Feedback)).Count();
                    return EventFeedbackResponse;
                }
                else
                {
                    return new EventFeedbackResponseModel();
                }
            }
            else
            {
                return new EventFeedbackResponseModel();
            }
        }
    }
}
