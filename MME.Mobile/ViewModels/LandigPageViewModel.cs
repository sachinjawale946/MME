﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
        
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        SnackbarOptions snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(5),
            Font = Microsoft.Maui.Font.SystemFontOfSize(12),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
            CharacterSpacing = 0.1
        };

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
                var _message = "Please select language to continue";
                var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
            }
        }
    }
}
