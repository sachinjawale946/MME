using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class EventFeedbackResponseModel
    {
        public Guid Id { get; set; }
        public bool? Liked { get; set; }
        public bool? DisLiked { get; set; }
        public bool? ReportAbuse { get; set; }
        public bool? Participation { get; }
        public decimal? Donation { get; set; }
        public string? Suggestion { get; set; }
        public string? Feedback { get; set; }
    }
}
