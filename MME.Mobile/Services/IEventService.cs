using MME.Model.Request;
using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface IEventService
    {
        Task<EventResponseWrappeModel> Search(EventRequestModel model);
        Task<EventFeedbackResponseModel> SaveFeedback(EventFeedbackResponseModel model);
        Task<EventResponseModel> GetEventById(Guid EventId);
    }
}