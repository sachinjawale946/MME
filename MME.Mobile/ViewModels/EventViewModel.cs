using MME.Mobile.Services;
using MME.Model.Request;
using MME.Model.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class EventViewModel : ViewModelBase
    {
        IEventService _memberService = new EventService();
        public Command SearchCommand { get; }
        public Command LoadMoreEventCommand { get; set; }

        public EventViewModel()
        {
            SearchCommand = new Command<string>(Search);
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

        private async void SearchMore()
        {
            Search(SearchModel.eventname);
        }

        private async void Search(string SearchFilter = "")
        {
            Events = new ObservableCollection<EventResponseModel>();
            if (SearchModel == null) SearchModel = new EventRequestModel();
            if (!string.IsNullOrEmpty(SearchFilter.Trim()))
            {
                // both search terms not null
                if (!string.IsNullOrEmpty(SearchFilter.Trim()) && !string.IsNullOrEmpty(SearchModel.eventname))
                {
                    // search term changed
                    if (SearchFilter.Trim() != SearchModel.eventname.Trim())
                    {
                        Events.Clear();
                        SearchModel.page = 0;
                        SearchModel.eventname= SearchFilter.Trim();
                    }
                    else
                    {
                        // same search term but next page
                    }
                }
                else
                {
                    Events.Clear();
                    SearchModel.page = 0;
                    SearchModel.eventname = SearchFilter.Trim();
                }
            }
            else
            {
                SearchModel.eventname = string.Empty;
            }

            SearchModel.page = SearchModel.page + 1;

            //BusyPage busyPage = new BusyPage();
            //await Application.Current.MainPage.ShowPopupAsync(busyPage);
            var results = await _memberService.Search(SearchModel);
            if (results != null && results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (Events == null) Events = new ObservableCollection<EventResponseModel>();
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

    }
}
