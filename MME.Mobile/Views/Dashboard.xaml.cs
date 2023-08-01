using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class Dashboard : ContentPage
{
	public Dashboard()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        var viewModel = new DashboardViewModel();
        this.BindingContext = viewModel;
        animate();
        base.OnAppearing();
    }

    private async void animate()
    {
        await EventsImage.RotateTo(360, 4000);
        await EventsImage.ScaleTo(2, 2000);
        await EventsImage.ScaleTo(1, 2000);
        await MembersImage.RotateTo(360, 4000);
        await MembersImage.ScaleTo(2, 2000);
        await MembersImage.ScaleTo(1, 2000);
        await ProfileImage.RotateTo(360, 4000);
        await ProfileImage.ScaleTo(2, 2000);
        await ProfileImage.ScaleTo(1, 2000);
    }
}