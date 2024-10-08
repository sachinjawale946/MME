﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Lookups;
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


        private int _totalEvents;
        public int TotalEvents
        {
            get { return _totalEvents; }
            set
            {
                _totalEvents = value;
                OnPropertyChanged(nameof(TotalEvents));
            }
        }


        private async void SearchMore()
        {
            if (isBusy) return;
            if (Events == null || Events.Count == 0) return;
            if (Events.Count >= TotalEvents) return;
            await Search(false, true);
        }

        private bool isBusy = false;
        private async Task Search(bool showloader = true, bool morecommand = false)
        {
            isBusy = true;
            if (showloader)
            {
                var busy = new BusyPage();
                await MopupService.Instance.PushAsync(busy);
            }
            if (Events == null) Events = new ObservableCollection<EventResponseModel>();
            if (SearchModel == null)
            {
                SearchModel = new EventRequestModel() { eventname = string.Empty, page = 1 };
            }
            else
            {
                if (morecommand)
                {
                    SearchModel.page = SearchModel.page + 1;
                }
                else
                {
                    SearchModel.page = 1;
                }
            }
            var result = await _eventService.Search(SearchModel);
            if (result != null && result.Events != null)
            {
                TotalEvents = result.EventsCount;
                for (int i = 0; i < result.Events.Count; i++)
                {
                    if (!Events.Contains(result.Events[i]))
                        Events.Add(result.Events[i]);
                }
            }
            if (showloader)
                await MopupService.Instance.PopAsync(true);
            isBusy = false;
        }

        public async void NewSearch(string SearchFilter = "")
        {
            if (isBusy) return;
            TotalEvents = 0;
            Events = new ObservableCollection<EventResponseModel>();
            SearchModel = new EventRequestModel() { eventname = SearchFilter, page = 1 };
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
                            userid = Guid.Parse(SecureStorage.Default.GetAsync(SecureStorage_Lookup.userid).Result.ToString()),
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
                            userid = Guid.Parse(SecureStorage.Default.GetAsync(SecureStorage_Lookup.userid).Result.ToString()),
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
                            userid = Guid.Parse(SecureStorage.Default.GetAsync(SecureStorage_Lookup.userid).Result.ToString()),
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
