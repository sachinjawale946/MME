using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class ForgotPassword : ContentPage
{
	public ForgotPassword()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        this.BindingContext = new ForgotPasswordViewModel(this);
        base.OnAppearing();
    }
}