using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class EventResponseWrappeModel
    {
        public int EventsCount { get; set; }
        public List<EventResponseModel> Events { get; set; }
    }
}
