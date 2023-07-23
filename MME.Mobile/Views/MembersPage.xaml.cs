using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class MembersPage : ContentPage
{
	public MembersPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        this.BindingContext = new MemberViewModel();
        base.OnAppearing();
    }
}