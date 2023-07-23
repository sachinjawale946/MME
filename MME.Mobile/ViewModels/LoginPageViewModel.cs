using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        public Command NavigateCommand { get; }

        public LoginPageViewModel(Page page) : base(page)
        {
            NavigateCommand = new Command(OnNavigate);
        }

        private async void OnNavigate()
        {
            //await page.Navigation.PushAsync(new Login(), true);
        }
    }
}
