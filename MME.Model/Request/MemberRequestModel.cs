using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Request
{
    public class MemberRequestModel
    {
        public string membername { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
    }
}
