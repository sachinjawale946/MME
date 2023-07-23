using CommunityToolkit.Maui.Views;
using MME.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class ForgotPasswordViewModel : ViewModelBase
    {
        public Command NavigateCommand { get; }

        public ForgotPasswordViewModel(Page page) : base(page)
        {
            NavigateCommand = new Command(OnNavigate);
        }
        private async void OnNavigate()
        {
            await page.Navigation.PushAsync(new Login(), true);
        }
    }
}
