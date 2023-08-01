using Plugin.Settings.Abstractions;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.Helpers
{
    internal static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string username
        {
            get
            {
                return AppSettings.GetValueOrDefault("username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("username", value);
            }
        }

        public static int roleid
        {
            get
            {
                return AppSettings.GetValueOrDefault("roleid", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("roleid", value);
            }
        }

        public static Guid userid
        {
            get
            {
                return AppSettings.GetValueOrDefault("userid", Guid.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("userid", value);
            }
        }

        public static string firstname
        {
            get
            {
                return AppSettings.GetValueOrDefault("firstname", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("firstname", value);
            }
        }

        public static string lastname
        {
            get
            {
                return AppSettings.GetValueOrDefault("lastname", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("lastname", value);
            }
        }

        public static string mobile
        {
            get
            {
                return AppSettings.GetValueOrDefault("mobile", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("mobile", value);
            }
        }

        public static string accesstoken
        {
            get
            {
                return AppSettings.GetValueOrDefault("accesstoken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("accesstoken", value);
            }
        }

        public static string gender
        {
            get
            {
                return AppSettings.GetValueOrDefault("gender", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("gender", value);
            }
        }

        public static string language
        {
            get
            {
                return AppSettings.GetValueOrDefault("language", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("language", value);
            }
        }
        
    }
}
