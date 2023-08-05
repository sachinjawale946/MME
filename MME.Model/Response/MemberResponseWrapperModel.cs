using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class MemberResponseWrapperModel
    {
        public int MembersCount { get; set; }
        public List<MemberResponseModel> Members { get; set; }
    }
}
