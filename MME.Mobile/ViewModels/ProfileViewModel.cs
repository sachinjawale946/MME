﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Java.Lang;
using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.BindingModels.Mobile;
using MME.Model.Lookups;
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
        readonly IMemberService _memberService = new MemberService();
        readonly ICommonService _commonService = new CommonService();

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        SnackbarOptions snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(5),
            Font = Microsoft.Maui.Font.SystemFontOfSize(12),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
            CharacterSpacing = 0.1,
        };
        SnackbarOptions successSnackbarOptions = new SnackbarOptions
        {

            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(5),
            Font = Microsoft.Maui.Font.SystemFontOfSize(12),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
            CharacterSpacing = 0.1,
        };

        public ProfileViewModel()
        {
            MaxBirthDate = DateTime.Now.AddYears(-1);
            if (Profile == null)
            {
                Profile = new ProfileResponseModel { shownoimage = true };
            }
            Task.Run(async () =>
            {
                await GetProfile();
                await MasterData();
            });
        }

        private List<OccupationResponseModel> _occupations;
        public List<OccupationResponseModel> Occupations
        {
            get { return _occupations; }
            set
            {
                _occupations = value;
                OnPropertyChanged(nameof(Occupations));
            }
        }

        private OccupationResponseModel _occupation;
        public OccupationResponseModel Occupation
        {
            get
            {
                return _occupation;
            }
            set
            {
                _occupation = value;
                OnPropertyChanged(nameof(Occupation));
            }
        }

        private List<ReligionResponseModel> _religions;
        public List<ReligionResponseModel> Religions
        {
            get { return _religions; }
            set
            {
                _religions = value;
                OnPropertyChanged(nameof(Religions));
            }
        }

        private ReligionResponseModel _religion;
        public ReligionResponseModel Religion
        {
            get
            {
                return _religion;
            }
            set
            {
                _religion = value;
                OnPropertyChanged(nameof(Religion));
            }
        }

        private List<CasteResponseModel> _castes;
        public List<CasteResponseModel> Castes
        {
            get
            {
                return _castes;
            }
            set
            {
                _castes = value;
                OnPropertyChanged(nameof(Castes));
            }
        }

        private CasteResponseModel _caste;
        public CasteResponseModel Caste
        {
            get
            {
                return _caste;
            }
            set
            {
                _caste = value;
                OnPropertyChanged(nameof(Caste));
            }
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
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                if (value != null && value.stateid > 0) GetPincodes(value.stateid);
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

        private void GetPincodes(int stateid)
        {
            if (State == null) return;
            Pincodes = _commonService.GetPincodes(stateid).Result;
            if (Pincodes != null && Pincodes.Count > 0 && Profile != null && Convert.ToInt16(Profile.PincodeId) > 0)
            {
                Pincode = Pincodes.Where(s => s.pincodeid == Profile.PincodeId).FirstOrDefault();
            }
        }

        public async Task<string> AddProfiePicture(string fileextenstion)
        {
            var result = string.Empty;
            if (Profile != null && Profile.profilepic != null && Profile.profilepic.Length > 0 && !string.IsNullOrEmpty(fileextenstion))
            {
                result = await _memberService.SaveProfileImage(new ProfilePictureRequestModel
                {
                    picture = Profile.profilepic,
                    pictureextenstion = fileextenstion,
                    userid = Settings.userid
                });
                if (string.IsNullOrEmpty(result) || result == Api_Result_Lookup.Error)
                {
                    var snackbar = Snackbar.Make(Resx.AppResources.Validation_Message_Api_Error, null, string.Empty, TimeSpan.FromSeconds(8), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                else
                {
                    var snackbar = Snackbar.Make("Your profile picture is added successfully", null, string.Empty, TimeSpan.FromSeconds(8), successSnackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
            }
            return result;
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
                new DropdownModel{ Text = "Married", Value="Married" },
                new DropdownModel{ Text = "Unmarried", Value="Unmarried" },
                new DropdownModel{ Text = "Divorced", Value="Divorced" },
            };
            States = await _commonService.GetStates();
            Occupations = await _commonService.GetOccupations();
            Religions = await _commonService.GetReligions();
            Castes = await _commonService.GetCastes();
            SetDropdownSelections();
        }

        private void SetDropdownSelections()
        {
            if (States != null && States.Count > 0 && Profile != null && Convert.ToInt16(Profile.StateId) > 0)
            {
                State = States.Where(s => s.stateid == Profile.StateId).FirstOrDefault();
            }
            if (Occupations != null && Occupations.Count > 0 && Profile != null && Convert.ToInt16(Profile.OccupationId) > 0)
            {

                Occupation = Occupations.Where(o => o.occupationid == Profile.OccupationId).FirstOrDefault();
            }
            if (Religions != null && Religions.Count > 0 && Profile != null && Convert.ToInt16(Profile.ReligionId) > 0)
            {

                Religion = Religions.Where(o => o.religionid == Profile.ReligionId).FirstOrDefault();
            }
            if (Castes != null && Castes.Count > 0 && Profile != null && Convert.ToInt16(Profile.CasteId) > 0)
            {

                Caste = Castes.Where(o => o.casteid == Profile.CasteId).FirstOrDefault();
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

                if(Profile.BirthDate == null || !Profile.BirthDate.HasValue || Profile.BirthDate == DateTime.MinValue)
                {
                    Profile.BirthDate = null;
                }

                SetDropdownSelections();
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
