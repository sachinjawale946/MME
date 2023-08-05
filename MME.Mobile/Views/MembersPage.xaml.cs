using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class MembersPage : ContentPage
{
    MemberViewModel viewModel;

    public MembersPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        this.searchBar.Text = string.Empty;
        viewModel = new MemberViewModel();
        this.BindingContext = viewModel;
        base.OnAppearing();
    }

    private void searchResults_RemainingItemsThresholdReached(object sender, EventArgs e)
    {

    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (viewModel != null && string.IsNullOrEmpty(searchBar.Text))
        {
            viewModel.NewSearch(searchBar.Text.Trim());
        }
    }
}