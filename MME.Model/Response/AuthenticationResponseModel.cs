using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class AuthenticationResponseModel
    {
        public Guid userid { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string? middlename { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string accesstoken { get; set; }
        public string? message { get; set; }
        public string password { get; set; }
        public byte[] passwordsalt { get; set; }
        public int roleid { get; set; }
        public bool isactive { get; set; }
        public byte[] profilepic { get; set; }
    }
}
