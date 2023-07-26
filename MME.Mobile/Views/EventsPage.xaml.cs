using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class EventsPage : ContentPage
{
	public EventsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        this.BindingContext = new EventViewModel();
        base.OnAppearing();
    }
}