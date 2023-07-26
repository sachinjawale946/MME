using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class EventFeedbackResponseModel : ModelBase
    {
        public Guid id { get; set; }

        private bool? _liked;
        public bool? liked
        {
            get { return _liked; }
            set
            {
                _liked = value;
                OnPropertyChanged(nameof(liked));
            }
        }

        private bool? _disliked;
        public bool? disliked
        {
            get { return _disliked; }
            set
            {
                _disliked = value;
                OnPropertyChanged(nameof(disliked));
            }
        }

        private bool? _reportabuse;
        public bool? reportabuse
        {
            get { return _reportabuse; }
            set
            {
                _reportabuse = value;
                OnPropertyChanged(nameof(reportabuse));
            }
        }

        private bool? _participation;
        public bool? participation
        {
            get { return _participation; }
            set
            {
                _participation = value;
                OnPropertyChanged(nameof(participation));
            }
        }

        private decimal? _donation;
        public decimal? donation
        {
            get { return _donation; }
            set
            {
                _donation = value;
                OnPropertyChanged(nameof(donation));
            }
        }

        private string? _suggestion;
        public string? suggestion
        {
            get { return _suggestion; }
            set
            {
                _suggestion = value;
                OnPropertyChanged(nameof(suggestion));
            }
        }

        private string? _feedback;
        public string? feedback
        {
            get { return _feedback; }
            set
            {
                _feedback = value;
                OnPropertyChanged(nameof(feedback));
            }
        }

        public Guid eventid { get; set; }
        public Guid userid { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int spams { get; set; }
        public int participations { get; set; }
        public decimal donations { get; set; }
        public int suggestions { get; set; }
        public int feedbacks { get; set; }
    }
}
