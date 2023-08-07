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
using static System.Net.Mime.MediaTypeNames;
using Bertuzzi.MAUI.EventAggregator;

namespace MME.Mobile.ViewModels
{
    internal class FlyoutHeaderViewModel : ViewModelBase
    {
        IMemberService _memberService = new MemberService();

        public FlyoutHeaderViewModel()
        {
            ShowProfilePic();
            EventAggregator.Instance.RegisterHandler<byte[]>(ProfilePictureHandler);

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

        private void ProfilePictureHandler(byte[]? Picture)
        {
            if (Picture != null && Picture.Length > 0)
            {
                ProfileImage = Picture;
                _showprofileimage = true;
                shownoimage = false;
            }
            else
            {
                ProfileImage = null;
                _showprofileimage = false;
                shownoimage = true;
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
