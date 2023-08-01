using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.BindingModels.Mobile;
using MME.Model.Request;
using MME.Model.Response;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class ProfileViewModel : ViewModelBase
    {
        IMemberService _memberService = new MemberService();
        ICommonService _commonService = new CommonService();

        public ProfileViewModel()
        {
            Task.Run(async () =>
            {
                await GetProfile();
                await MasterData();
            });
        }

        private List<StateResponseModel> _states;
        public List<StateResponseModel> States
        {
            get { return _states; }
            set
            {
                _states = value;
                OnPropertyChanged(nameof(States));
            }
        }

        private StateResponseModel _state;
        public StateResponseModel State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (State != null) GetPincodes().RunSynchronously();
                OnPropertyChanged(nameof(State));
            }
        }

        private List<PincodeResponseModel> _pincodes;
        public List<PincodeResponseModel> Pincodes
        {
            get { return _pincodes; }
            set
            {
                _pincodes = value;
                OnPropertyChanged(nameof(Pincodes));
            }
        }

        private PincodeResponseModel _pincode;
        public PincodeResponseModel Pincode
        {
            get { return _pincode; }
            set
            {
                _pincode = value;
                OnPropertyChanged(nameof(Pincode));
            }
        }

        private DateTime _maxBirthDate;
        public DateTime MaxBirthDate
        {
            get { return _maxBirthDate; }
            set
            {
                _maxBirthDate = value;
                OnPropertyChanged(nameof(MaxBirthDate));
            }
        }

        private ProfileResponseModel _profile;
        public ProfileResponseModel Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        private List<DropdownModel> _genders;
        public List<DropdownModel> Genders
        {
            get { return _genders; }
            set
            {
                _genders = value;
                OnPropertyChanged(nameof(Genders));
            }
        }

        private DropdownModel _gender;
        public DropdownModel Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private List<DropdownModel> _martialStatues;
        public List<DropdownModel> MartialStatues
        {
            get { return _martialStatues; }
            set
            {
                _martialStatues = value;
                OnPropertyChanged(nameof(MartialStatues));
            }
        }

        private DropdownModel _martialStatus;
        public DropdownModel MartialStatus
        {
            get { return _martialStatus; }
            set
            {
                _martialStatus = value;
                OnPropertyChanged(nameof(MartialStatus));
            }
        }

        private async Task GetPincodes()
        {
            if (State == null) return;
            Pincodes = await _commonService.GetPincodes(State.stateid);
            if (Pincodes != null && Pincodes.Count > 0 && Profile != null && Convert.ToInt16(Profile.PincodeId) > 0)
            {
                Pincode = Pincodes.Where(s => s.pincodeid == Profile.PincodeId).FirstOrDefault();
            }
        }

        private async Task MasterData()
        {
            Genders = new List<DropdownModel>
            {
                new DropdownModel{ Text = "Male", Value="Male" },
                new DropdownModel{ Text = "Female", Value="Female" },
            };
            MartialStatues = new List<DropdownModel>
            {
                new DropdownModel{ Text = "Single", Value="Single" },
                new DropdownModel{ Text = "Married", Value="Married" },
                new DropdownModel{ Text = "Unmarried", Value="Unmarried" },
                new DropdownModel{ Text = "Divorced", Value="Divorced" },
            };
            MaxBirthDate = DateTime.Now.AddYears(-1);
            States = await _commonService.GetStates();
            if (States != null && States.Count > 0 && Profile != null && Convert.ToInt16(Profile.StateId) > 0)
            {
                State = States.Where(s => s.stateid == Profile.StateId).FirstOrDefault();
            }
        }
        private async Task GetProfile()
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            Profile = await _memberService.GetProfile(Settings.userid);
            if (Profile != null)
            {
                if (Profile.profilepic != null && Profile.profilepic.Length > 0)
                {
                    Profile.showprofileimage = true;
                    Profile.shownoimage = false;
                }
                else
                {
                    Profile.showprofileimage = false;
                    Profile.shownoimage = true;
                }

                if (!string.IsNullOrEmpty(Profile.Gender) && Genders != null && Genders.Count > 0)
                {
                    Gender = Genders.Where(g => g.Value == Profile.Gender).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(Profile.MaritalStatus) && MartialStatues != null && MartialStatues.Count > 0)
                {
                    MartialStatus = MartialStatues.Where(g => g.Value == Profile.MaritalStatus).FirstOrDefault();
                }
            }
            else
            {
                Profile.showprofileimage = false;
                Profile.shownoimage = true;
            }
            await MopupService.Instance.PopAsync(true);
        }
    }
}
