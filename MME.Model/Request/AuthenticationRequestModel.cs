using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Request
{
    public class AuthenticationRequestModel
    {
        public AuthenticationRequestModel()
        {
            username = string.Empty;
            password = string.Empty;
        }

        public string username { get; set; }
        public string password { get; set; }
    }
}
