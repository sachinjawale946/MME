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
}