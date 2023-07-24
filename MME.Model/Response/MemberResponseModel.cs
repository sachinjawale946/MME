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
        public string mobile { get; set; }
        public string username { get; set; }
        public string? fullname { get; set; }
        public string? occupation { get; set; }
        public string? fulladdress { get; set; }
        public string? gender { get; set; }
        public byte[] profilepic { get; set; }
        public bool showprofileimage { get; set; }
        public bool shownoimage { get; set; }
    }
}
