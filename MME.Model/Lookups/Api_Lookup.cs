using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Lookups
{
    public class Api_Lookup
    {
        public const string BaseApi = "http://localhost:98";
        public const string getLanguageApi = BaseApi + "/api/v1/getlanguages";
        public const string loginApi = BaseApi + "/api/v1/authenticate-user";
        public const string memberSearchApi = BaseApi + "/api/v1/members-search";
        public const string memberProfilePicApi = BaseApi + "/api/v1/members-getprofilepicture/{0}";
    }
}
