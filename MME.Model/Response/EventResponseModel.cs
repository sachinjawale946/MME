using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class EventResponseModel : ModelBase
    {
        public Guid eventid { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        // public byte[] banner { get; set; }
        public string bannerurl { get; set; }
        public string? location { get; set; }
        public DateTime eventdate { get; set; }
        
        private int _event_type_id;
        public int event_type_id
        {
            get { return _event_type_id; }
            set
            {
                _event_type_id = value;
                OnPropertyChanged(nameof(event_type_id));
            }
        }

        public string? createdby { get; set; }
        private int _likes;
        public int likes
        {
            get { return _likes; }
            set
            {
                _likes = value;
                OnPropertyChanged(nameof(likes));
            }
        }

        private int _dislikes;
        public int dislikes
        {
            get { return _dislikes; }
            set
            {
                _dislikes = value;
                OnPropertyChanged(nameof(dislikes));
            }
        }

        private int _spams;
        public int spams
        {
            get { return _spams; }
            set
            {
                _spams = value;
                OnPropertyChanged(nameof(spams));
            }
        }

        private int _participations;
        public int participations
        {
            get { return _participations; }
            set
            {
                _participations = value;
                OnPropertyChanged(nameof(participations));
            }
        }

        private decimal _donations;
        public decimal donations
        {
            get { return _donations; }
            set
            {
                _donations = value;
                OnPropertyChanged(nameof(donations));
            }
        }

        private int _suggestions;
        public int suggestions
        {
            get { return _suggestions; }
            set
            {
                _suggestions = value;
                OnPropertyChanged(nameof(suggestions));
            }
        }

        private int _feedbacks;
        public int feedbacks
        {
            get { return _feedbacks; }
            set
            {
                _feedbacks = value;
                OnPropertyChanged(nameof(feedbacks));
            }
        }

        private EventFeedbackResponseModel? _eventFeedback;
        public EventFeedbackResponseModel? EventFeedback
        {
            get { return _eventFeedback; }
            set
            {
                _eventFeedback = value;
                OnPropertyChanged(nameof(EventFeedback));
            }
        }
    }
}
