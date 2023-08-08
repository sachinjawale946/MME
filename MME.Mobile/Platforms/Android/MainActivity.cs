using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Plugin.Firebase.CloudMessaging;

namespace MME.Mobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HandleIntent(Intent);
            CreateNotificationChannelIfNeeded();
            if (!(CheckPermissionGranted(Manifest.Permission.PostNotifications)))
            {
                RequestPostNotificationsPermission();
            }
        }

        public bool CheckPermissionGranted(string Permissions)
        {
            // Check if the permission is already available.
            if (ActivityCompat.CheckSelfPermission(this, Permissions) != Permission.Granted)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int REQUEST_P0STNOTIFICATIONS = 10023;
        string[] reqestPermission = { "android.permission.POST_NOTIFICATIONS" };
        private void RequestPostNotificationsPermission()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, "android.permission.POST_NOTIFICATIONS"))
            {
                // Provide an additional rationale to the user if the permission was not granted
                // and the user would benefit from additional context for the use of the permission.
                // For example if the user has previously denied the permission.
                ActivityCompat.RequestPermissions(this, reqestPermission, REQUEST_P0STNOTIFICATIONS);
            }
            else
            {
                //P0STNOTIFICATIONS permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, reqestPermission, REQUEST_P0STNOTIFICATIONS);
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults); if (requestCode == REQUEST_P0STNOTIFICATIONS)
            {
                if (grantResults.Length <= 0)
                {
                    // If user interaction was interrupted, the permission request is cancelled and you 
                    // receive empty arrays.
                    // Log.Info("error", "User interaction was cancelled.");
                }
                else if (grantResults[0] == PermissionChecker.PermissionGranted)
                {
                    // Permission was granted.
                }
                else
                {
                    // Permission denied.
                    Toast.MakeText(this, " REQUEST_P0STNOTIFICATIONS Permission Denied", ToastLength.Long).Show();
                }
            }
        }


        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            HandleIntent(intent);
        }

        private static void HandleIntent(Intent intent)
        {
            FirebaseCloudMessagingImplementation.OnNewIntent(intent);
        }

        private void CreateNotificationChannelIfNeeded()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                CreateNotificationChannel();
            }
        }

        private void CreateNotificationChannel()
        {
            var channelId = $"{PackageName}.general";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var channel = new NotificationChannel(channelId, "General", NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(channel);
            FirebaseCloudMessagingImplementation.ChannelId = channelId;
            //FirebaseCloudMessagingImplementation.SmallIconRef = Resource.Drawable.ic_push_small;
        }

    }
}