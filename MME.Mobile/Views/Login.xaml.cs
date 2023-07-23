using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        this.BindingContext = new LoginPageViewModel(this);
        base.OnAppearing();
    }

    private void LoginEntry_Completed(object sender, EventArgs e)
    {
        LoginEntry.IsEnabled = false;
        LoginEntry.IsEnabled = true;
    }

    private void PasswordEntry_Completed(object sender, EventArgs e)
    {
        PasswordEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = true;
    }
}