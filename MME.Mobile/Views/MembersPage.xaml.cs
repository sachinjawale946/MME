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
        this.searchBar.Text = string.Empty;
        this.BindingContext = new MemberViewModel();
        base.OnAppearing();
    }

    private void searchResults_RemainingItemsThresholdReached(object sender, EventArgs e)
    {

    }
}