using CommunityToolkit.Maui.Views;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Request;
using MME.Model.Response;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace MME.Mobile.ViewModels
{
    internal class MemberViewModel : ViewModelBase
    {
        IMemberService _memberService = new MemberService();
        public Command SearchCommand { get; }
        public Command LoadMoreMembersCommand { get; set; }

        public MemberViewModel()
        {
            SearchCommand = new Command<string>(NewSearch);
            LoadMoreMembersCommand = new Command(SearchMore);
            Task.Run(async () =>
            {
                await Search();
            });
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

        private void SearchMore()
        {
            // Search();
        }

        private async Task Search()
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            if (Members == null) Members = new ObservableCollection<MemberResponseModel>();
            if (SearchModel == null) SearchModel = new MemberRequestModel() { membername = string.Empty, page = 1 };
            var results = await _memberService.Search(SearchModel);
            if (results != null && results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].profilepic == null)
                    {
                        results[i].showprofileimage = false;
                        results[i].shownoimage = true;
                    }
                    else
                    {
                        results[i].showprofileimage = true;
                        results[i].shownoimage = false;
                    }
                    if (!Members.Contains(results[i]))
                        Members.Add(results[i]);
                }
            }
            else
            {
                // reset search
                Members.Clear();
                SearchModel.page = 0;
                SearchModel.membername = string.Empty;
            }
            await MopupService.Instance.PopAsync(true);
        }

        private void NewSearch(string SearchFilter = "")
        {
            Members = new ObservableCollection<MemberResponseModel>();
            if (SearchModel == null) SearchModel = new MemberRequestModel();
            SearchModel.page = 0;
            SearchModel.membername = SearchFilter;
            Search();
        }

    }
}
