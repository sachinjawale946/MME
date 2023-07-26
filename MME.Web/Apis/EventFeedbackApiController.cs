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
                    return new EventFeedbackResponseModel()
                    {
                        disliked = eventFeedback.DisLiked,
                        donation = eventFeedback.Donation,
                        eventid = eventFeedback.EventId,
                        feedback = eventFeedback.Feedback,
                        id = eventFeedback.Id,
                        liked = eventFeedback.Liked,
                        reportabuse = eventFeedback.ReportAbuse,
                        suggestion = eventFeedback.Suggestion,
                        userid = eventFeedback.UserId,
                    };
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
                    return new EventFeedbackResponseModel()
                    {
                        disliked= feedback.DisLiked,
                        donation= feedback.Donation,    
                        eventid= feedback.EventId,
                        feedback = feedback.Feedback,
                        id= feedback.Id,
                        liked= feedback.Liked,
                        reportabuse = feedback.ReportAbuse, 
                        suggestion=feedback.Suggestion,
                        userid = feedback.UserId,
                    };
                }
            }
            else
            {
                return new EventFeedbackResponseModel();
            }
        }

    }
}
