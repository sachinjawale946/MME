using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Lookups
{
    public class Api_Lookup
    {
        // public const string BaseApi = "http://localhost:98";
        public const string BaseApi = "https://sachinjawale946.bsite.net";
        public const string getLanguageApi = BaseApi + "/api/v1/getlanguages";
        public const string loginApi = BaseApi + "/api/v1/authenticate-user";
        public const string memberSearchApi = BaseApi + "/api/v1/members-search";
        public const string memberGetProfilePicApi = BaseApi + "/api/v1/members-getprofilepicture/{0}";
        public const string memberSaveProfilePicApi = BaseApi + "/api/v1/members-saveprofilepicture";
        public const string memberProfileApi = BaseApi + "/api/v1/members-getprofile/{0}";
        public const string memberProfileFCMApi = BaseApi + "/api/v1/members-add-fcmtoken";
        public const string eventSearchApi = BaseApi + "/api/v1/events-search";
        public const string eventDetailsApi = BaseApi + "/api/v1/events-details/{0}/{1}";
        public const string eventFeedbackApi = BaseApi + "/api/v1/save-event-feedback";
        public const string statesApi = BaseApi + "/api/v1/getstates";
        public const string pincodesByStateApi = BaseApi + "/api/v1/getpincodes-bystate/{0}";
        public const string occupationsApi = BaseApi + "/api/v1/getoccupations";
        public const string religionsApi = BaseApi + "/api/v1/getreligions";
        public const string castesApi = BaseApi + "/api/v1/getcasts"; 
    }
}
