using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Request
{
    public class EventRequestModel
    {
        public string? eventname { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
        public Guid userid { get;set; }
    }
}
