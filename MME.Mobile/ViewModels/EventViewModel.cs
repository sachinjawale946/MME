using Microsoft.Maui.Controls.Shapes;
using MME.Mobile.Services;
using MME.Model.Request;
using MME.Model.Response;
using System.Collections.ObjectModel;

namespace MME.Mobile.ViewModels
{
    internal class EventViewModel : ViewModelBase
    {
        IEventService _memberService = new EventService();
        public Command SearchCommand { get; }
        public Command LoadMoreEventCommand { get; set; }

        public EventViewModel()
        {
            SearchCommand = new Command<string>(NewSearch);
            LoadMoreEventCommand = new Command(SearchMore);
            Search();
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
            Search();
        }

        private async void Search()
        {
            //BusyPage busyPage = new BusyPage();
            //await Application.Current.MainPage.ShowPopupAsync(busyPage);
            if (Events == null) Events = new ObservableCollection<EventResponseModel>();
            if (SearchModel == null) SearchModel = new EventRequestModel() { eventname = string.Empty, page = 1 };
            var results = await _memberService.Search(SearchModel);
            if (results != null && results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].banner == null)
                    {
                        results[i].showbannerimage = false;
                        results[i].shownoimage = true;
                    }
                    else
                    {
                        results[i].showbannerimage = true;
                        results[i].shownoimage = false;
                    }
                    if (!Events.Contains(results[i]))
                        Events.Add(results[i]);
                }
            }
            else
            {
                // reset search
                Events.Clear();
                SearchModel.page = 0;
                SearchModel.eventname = string.Empty;
            }
            //busyPage.Close();
        }

        private void NewSearch(string SearchFilter = "")
        {
            Events = new ObservableCollection<EventResponseModel>();
            if (SearchModel == null) SearchModel = new EventRequestModel();
            SearchModel.page = 0;
            SearchModel.eventname = SearchFilter;
            Search();
        }

        public void LikeAction(EventResponseModel like)
        {
            if (Events != null && Events.Count > 0 && like != null)
            {
                if(Events.Where(e => e.eventid == like.eventid).FirstOrDefault() != null)
                {
                    if(Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback == null)
                    {
                        Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback = new EventFeedbackResponseModel()
                        {
                            Liked = true,
                            DisLiked = null,
                        };
                    }
                    else
                    {
                        Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback.Liked = (Convert.ToBoolean(Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback.Liked) == true) ? false : true;
                        Events.Where(e => e.eventid == like.eventid).FirstOrDefault().EventFeedback.DisLiked = null;
                    }
                }
            }
        }

        public void DisLikeAction(EventResponseModel dislike)
        {
            if (Events != null && Events.Count > 0 && dislike != null)
            {
                if (Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault() != null)
                {
                    if (Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback == null)
                    {
                        Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback = new EventFeedbackResponseModel()
                        {
                            Liked = null,
                            DisLiked = true,
                        };
                    }
                    else
                    {
                        Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback.DisLiked = (Convert.ToBoolean(Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback.Liked) == true) ? false : true;
                        Events.Where(e => e.eventid == dislike.eventid).FirstOrDefault().EventFeedback.Liked = null;
                    }
                }
            }
        }
    }
}
