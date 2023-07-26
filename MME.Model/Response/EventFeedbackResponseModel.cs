using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class EventFeedbackResponseModel
    {
        public Guid id { get; set; }
        public bool? liked { get; set; }
        public bool? disliked { get; set; }
        public bool? reportabuse { get; set; }
        public bool? participation { get; }
        public decimal? donation { get; set; }
        public string? suggestion { get; set; }
        public string? feedback { get; set; }
        public Guid eventid { get; set; }
        public Guid userid { get; set; }
    }
}
