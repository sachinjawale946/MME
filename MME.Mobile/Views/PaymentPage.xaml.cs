using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class PaymentPage : ContentPage
{
	public PaymentPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        var viewModel = new PaymentViewModel();
        this.BindingContext = viewModel;
        base.OnAppearing();
    }
}