using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Request;
using MME.Model.Response;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.CalendarContract;

namespace MME.Mobile.ViewModels
{
    internal class EventDetailsViewModel : ViewModelBase
    {
        readonly IEventService _eventService = new EventService();
        public EventDetailsViewModel(Guid eventid) 
        {
            Task.Run(async () =>
            {
                await GetEvent(eventid);
            });
        }

        private EventResponseModel _event;
        public EventResponseModel Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged(nameof(Event));
            }
        }

        private string _eventOn;
        public string EventOn
        {
            get { return _eventOn; }
            set
            {
                _eventOn = value;
                OnPropertyChanged(nameof(EventOn));
            }
        }

        private string _eventAt;
        public string EventAt
        {
            get { return _eventAt; }
            set
            {
                _eventAt = value;
                OnPropertyChanged(nameof(EventAt));
            }
        }

        private async Task GetEvent(Guid eventid)
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            Event = await _eventService.GetEventById(eventid);
            if(Event != null && Event.eventdate != null && Event.eventdate != DateTime.MinValue) 
            {
                EventOn = "Event On - " + Event.eventdate.ToString("dd MMM yyyy, HH mm");
            }
            if(Event != null && !string.IsNullOrEmpty(Event.location))
            {
                EventAt = "Place - " + Event.location;
            }
            await MopupService.Instance.PopAsync(true);
        }
    }
}
