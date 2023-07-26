using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class EventResponseModel
    {
        public Guid eventid { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        public byte[] banner { get; set; }
        public string? location { get; set; }
        public DateTime eventdate { get; set; }
        public bool shownoimage { get; set; }
        public bool showbannerimage { get; set; }
    }
}
