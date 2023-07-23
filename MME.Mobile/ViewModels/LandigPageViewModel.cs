using CommunityToolkit.Maui.Views;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class LandigPageViewModel : ViewModelBase
    {
        readonly ILanguageService _languageService = new LanguageService();
        public Command NavigateCommand { get; }

        public LandigPageViewModel(Page page) : base(page)
        {
            NavigateCommand = new Command(OnNavigate);
            GetLanguages();
        }

        private List<LanguageResponseModel> _languages;
        public List<LanguageResponseModel> Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        private LanguageResponseModel _language;
        public LanguageResponseModel Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public async void GetLanguages()
        {
             Languages = await _languageService.GetLanguages();
        }

        private async void OnNavigate()
        {
            if (Language != null && Language.languageid > 0)
            {
                await page.Navigation.PushAsync(new Login(), true);
            }
            else
            {
                var errorMessage = "Please select language to continue";
                ErrorPage errorPage = new ErrorPage(errorMessage);
                Application.Current.MainPage.ShowPopup(errorPage);
            }
        }
    }
}
