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

        private int _totalMembers;
        public int TotalMembers
        {
            get { return _totalMembers; }
            set
            {
                _totalMembers = value;
                OnPropertyChanged(nameof(TotalMembers));
            }
        }

        private async void SearchMore()
        {
            if (Members == null || Members.Count == 0) return;
            if (Members.Count >= TotalMembers) return;
            await Search(false, true);
        }

        private async Task Search(bool showloader = true, bool morecommand = false)
        {
            if (showloader)
            {
                var busy = new BusyPage();
                await MopupService.Instance.PushAsync(busy);
            }
            if (Members == null) Members = new ObservableCollection<MemberResponseModel>();
            if (SearchModel == null)
            {
                SearchModel = new MemberRequestModel() { membername = string.Empty, page = 1 };
            }
            else
            {
                if(morecommand)
                {
                    SearchModel.page = SearchModel.page + 1;
                }
                else
                {
                    SearchModel.page = 1;
                }
            }
            var result = await _memberService.Search(SearchModel);
            if (result != null && result.Members != null && result.Members.Count > 0)
            {
                TotalMembers = result.MembersCount;
                for (int i = 0; i < result.Members.Count; i++)
                {
                    if (string.IsNullOrEmpty(result.Members[i].profilepicurl))
                    {
                        result.Members[i].showprofileimage = false;
                        result.Members[i].shownoimage = true;
                    }
                    else
                    {
                        result.Members[i].showprofileimage = true;
                        result.Members[i].shownoimage = false;
                    }
                    if (!Members.Contains(result.Members[i]))
                        Members.Add(result.Members[i]);
                }
            }
            else
            {
                // reset search
                TotalMembers= 0;
                Members.Clear();
                SearchModel.page = 0;
                SearchModel.membername = string.Empty;
            }
            if (showloader)
                await MopupService.Instance.PopAsync(true);
        }

        public async void NewSearch(string SearchFilter = "")
        {
            TotalMembers = 0;
            Members = new ObservableCollection<MemberResponseModel>();
            SearchModel = new MemberRequestModel() { membername = SearchFilter, page = 1 };
            await Search();
        }

    }
}
