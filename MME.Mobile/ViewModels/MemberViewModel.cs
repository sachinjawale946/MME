using CommunityToolkit.Maui.Views;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Request;
using MME.Model.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MME.Mobile.ViewModels
{
    internal class MemberViewModel : ViewModelBase
    {
        IMemberService _memberService = new MemberService();
        public Command SearchCommand { get; }

        public MemberViewModel()
        {
            SearchCommand = new Command<string>(Search);
            Search();
        }


        private ObservableCollection<MemberResponseModel> _members;
        public ObservableCollection<MemberResponseModel> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }

        private MemberRequestModel _searchModel;
        public MemberRequestModel SearchModel
        {
            get { return _searchModel; }
            set
            {
                _searchModel = value;
                OnPropertyChanged(nameof(SearchModel));
            }
        }

        private async void Search(string SearchFilter = "")
        {
            // if (SearchModel != null && SearchModel.page == 0) Members = new List<MemberResponseModel>();
            Members = new ObservableCollection<MemberResponseModel>();
            if (SearchModel == null) SearchModel = new MemberRequestModel();
            if (!string.IsNullOrEmpty(SearchFilter.Trim()))
                SearchModel.membername = SearchFilter.Trim();
            else
                SearchModel.membername = string.Empty;

            //BusyPage busyPage = new BusyPage();
            //await Application.Current.MainPage.ShowPopupAsync(busyPage);
            var results = await _memberService.Search(SearchModel);
            if (results != null)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (Members == null) Members = new ObservableCollection<MemberResponseModel>();
                    if (!Members.Contains(results[i]))
                        Members.Add(results[i]);
                }
            }
            //busyPage.Close();
        }
    }
}
