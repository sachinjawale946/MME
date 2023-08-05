using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Request;
using MME.Model.Response;
using Mopups.Services;
using System;
using System.Collections.ObjectModel;

namespace MME.Mobile.ViewModels
{
    internal class EventViewModel : ViewModelBase
    {
        readonly IEventService _eventService = new EventService();
        public Command SearchCommand { get; }
        public Command LoadMoreEventCommand { get; set; }

        public EventViewModel()
        {
            SearchCommand = new Command<string>(NewSearch);
            LoadMoreEventCommand = new Command(SearchMore);
            Task.Run(async () =>
            {
                await Search();
            });
        }


        private ObservableCollection<EventResponseModel> _events;
        public ObservableCollection<EventResponseModel> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private EventRequestModel _searchModel;
        public EventRequestModel SearchModel
        {
            get { return _searchModel; }
            set
            {
                _searchModel = value;
                OnPropertyChanged(nameof(SearchModel));
            }
        }

        private void SearchMore()
        {
            // Search();
        }

        private async Task Search()
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            if (Events == null) Events = new ObservableCollection<EventResponseModel>();
            if (SearchModel == null) SearchModel = new EventRequestModel() { eventname = string.Empty, page = 1 };
            var results = await _eventService.Search(SearchModel);
            if (results != null && results.Count > 0)
            {
                Events = new ObservableCollection<EventResponseModel>(results);
            }
            else
            {
                // reset search
                Events.Clear();
                SearchModel.page = 0;
                SearchModel.eventname = string.Empty;
            }
            await MopupService.Instance.PopAsync(true);
        }

        private async void NewSearch(string SearchFilter = "")
        {
            Events = new ObservableCollection<EventResponseModel>();
            if (SearchModel == null) SearchModel = new EventRequestModel();
            SearchModel.page = 0;
            SearchModel.eventname = SearchFilter;
            await Search();
        }

        public async void LikeAction(EventResponseModel like)
        {
            if (Events != null && Events.Count > 0 && like != null)
            {
                if (Events.Where(e => e.eventid == like.eventid).FirstOrDefault() != null)
                {
                    var eventFeedback = Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback;
                    if (eventFeedback == null || eventFeedback.eventid == Guid.Empty)
                    {
                        Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback = new EventFeedbackResponseModel()
                        {
                            liked = true,
                            eventid = like.eventid,
                            userid = Settings.userid,
                        };
                        // save feedback
                        var response = await _eventService.SaveFeedback(Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback);
                        SetFeedbackResponse(like.eventid, response);
                    }
                    else
                    {
                        if (Convert.ToBoolean(Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback.liked) == true)
                        {
                            Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback.liked = false;
                        }
                        else
                        {
                            Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback.liked = true;
                        }
                        // save feedback
                        var response = await _eventService.SaveFeedback(Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback);
                        SetFeedbackResponse(like.eventid, response);
                    }
                }
            }
        }

        public async void DisLikeAction(EventResponseModel dislike)
        {
            if (Events != null && Events.Count > 0 && dislike != null)
            {
                if (Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault() != null)
                {
                    var eventFeedback = Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback;
                    if (eventFeedback == null || eventFeedback.eventid == Guid.Empty)
                    {
                        Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback = new EventFeedbackResponseModel()
                        {
                            disliked = true,
                            eventid = dislike.eventid,
                            userid = Settings.userid,
                        };
                        // save feedback
                        var response = await _eventService.SaveFeedback(Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback);
                        SetFeedbackResponse(dislike.eventid, response);
                    }
                    else
                    {
                        if (Convert.ToBoolean(Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback.disliked) == true)
                        {
                            Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback.disliked = false;
                        }
                        else
                        {
                            Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback.disliked = true;
                        }
                        // save feedback
                        var response = await _eventService.SaveFeedback(Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback);
                        SetFeedbackResponse(dislike.eventid, response);
                    }
                }
            }
        }

        public async void SpamAction(EventResponseModel spam)
        {
            if (Events != null && Events.Count > 0 && spam != null)
            {
                if (Events.Where(e => e.eventid == spam.eventid).FirstOrDefault() != null)
                {
                    var eventFeedback = Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback;
                    if (eventFeedback == null || eventFeedback.eventid == Guid.Empty)
                    {
                        Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback = new EventFeedbackResponseModel()
                        {
                            reportabuse = true,
                            eventid = spam.eventid,
                            userid = Settings.userid,
                        };
                        // save feedback
                        var response = await _eventService.SaveFeedback(Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback);
                        SetFeedbackResponse(spam.eventid, response);
                    }
                    else
                    {
                        if (Convert.ToBoolean(Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback.reportabuse))
                        {
                            Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback.reportabuse = null;
                        }
                        else
                        {
                            Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback.reportabuse = true;
                        }
                        // save feedback
                        var response = await _eventService.SaveFeedback(Events.Where(e => e.eventid == spam.eventid).FirstOrDefault().EventFeedback);
                        SetFeedbackResponse(spam.eventid, response);
                    }
                }
            }
        }

        private void SetFeedbackResponse(Guid eventid, EventFeedbackResponseModel response)
        {
            if (response != null)
            {
                Events.Where(e => e.eventid == eventid).FirstOrDefault().EventFeedback = response;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().likes = response.likes;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().dislikes = response.dislikes;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().spams = response.spams;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().participations = response.participations;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().donations = response.donations;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().suggestions = response.suggestions;
                Events.Where(e => e.eventid == eventid).FirstOrDefault().feedbacks = response.feedbacks;
            }
        }

        public async void Navigate(Guid eventid)
        {
            await Shell.Current.GoToAsync($"EventDetailsPage?Event={eventid}");
        }
    }
}
