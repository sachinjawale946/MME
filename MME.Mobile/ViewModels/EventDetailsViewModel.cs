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

        private async Task GetEvent(Guid eventid)
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            Event = await _eventService.GetEventById(eventid);
            await MopupService.Instance.PopAsync(true);
        }
    }
}
