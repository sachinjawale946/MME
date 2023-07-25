using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Model.Request;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MME.Mobile.Helpers;

namespace MME.Mobile.ViewModels
{
    internal class FlyoutHeaderViewModel : ViewModelBase
    {
        IMemberService _memberService = new  MemberService();

        public FlyoutHeaderViewModel()
        {
            ShowProfilePic();
        }

        private bool _showprofileimage;
        public bool showprofileimage
        {
            get { return _showprofileimage; }
            set
            {
                _showprofileimage = value;
                OnPropertyChanged(nameof(showprofileimage));
            }
        }

        private bool _shownoimage;
        public bool shownoimage
        {
            get { return _shownoimage; }
            set
            {
                _shownoimage = value;
                OnPropertyChanged(nameof(shownoimage));
            }
        }


        private byte[] _profileImage;
        public byte[] ProfileImage
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


        public async void ShowProfilePic()
        {
            Gender = Settings.gender;
            if (ProfileImage == null || ProfileImage.Length > 0)
            {
                ProfileImage = await _memberService.GetProfileImage(Settings.userid);
                if (ProfileImage == null || ProfileImage.Length > 0)
                {
                    showprofileimage = true;
                    shownoimage = false;
                }
                else
                {
                    showprofileimage = false;
                    shownoimage = true;
                }
            }
        }
    }
}
