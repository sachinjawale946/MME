using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class LandigPage : ContentPage
{
	public LandigPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        this.BindingContext = new LandigPageViewModel(this);
        base.OnAppearing();
    }
}