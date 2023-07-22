using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class MemberResponseModel
    {
        public Guid userid { get; set; }
        public string firstname { get; set; }
        public string? middlename { get; set; }
        public string lastname { get; set; }
    }
}
