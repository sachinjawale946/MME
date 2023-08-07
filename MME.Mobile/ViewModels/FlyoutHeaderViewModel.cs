using Bertuzzi.MAUI.EventAggregator;
using MME.Mobile.Services;
using MME.Model.Lookups;

namespace MME.Mobile.ViewModels
{
    internal class FlyoutHeaderViewModel : ViewModelBase
    {
        readonly IMemberService _memberService = new MemberService();

        public FlyoutHeaderViewModel()
        {
            ShowProfilePic();
            EventAggregator.Instance.RegisterHandler<string>(ProfilePictureHandler);

        }

        private string _profileImage;
        public string ProfileImage
        {
            get { return _profileImage; }
            set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private void ProfilePictureHandler(string Picture)
        {
            ProfileImage = Picture;
        }


        public async void ShowProfilePic()
        {

            Gender = await SecureStorage.Default.GetAsync(SecureStorage_Lookup.gender);
            if (ProfileImage == null || ProfileImage.Length > 0)
            {
                ProfileImage = await _memberService.GetProfileImage(Guid.Parse(SecureStorage.Default.GetAsync(SecureStorage_Lookup.userid).Result.ToString()), SecureStorage.Default.GetAsync(SecureStorage_Lookup.gender).Result.ToString());
            }
        }
    }
}
