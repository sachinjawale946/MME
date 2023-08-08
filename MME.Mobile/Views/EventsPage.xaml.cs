using MME.Mobile.ViewModels;
using MME.Model.Response;

namespace MME.Mobile.Views;

public partial class EventsPage : ContentPage
{
    EventViewModel viewModel;
    public EventsPage()
    {
        InitializeComponent();
        viewModel = new EventViewModel();
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        if (viewModel == null)
        {
            this.searchBar.Text = string.Empty;
            viewModel = new EventViewModel();
            this.BindingContext = viewModel;
        }
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

    private double _scale = 0;

    private void like_Clicked(object sender, EventArgs e)
    {
        ImageButton likeButton = sender as ImageButton;
        if (likeButton != null)
        {
            var stackLayout = (likeButton.CommandParameter) as StackLayout;
            var commandParameter = stackLayout.BindingContext as EventResponseModel;
            if (commandParameter != null && viewModel != null)
            {
                viewModel.LikeAction(commandParameter);
            }
            Animate(likeButton);
        }
    }

    private async void Animate(ImageButton button)
    {
        _scale = button.Scale;
        await button.ScaleTo(_scale * 1.2, 1000);
        await button.ScaleTo(_scale, 1000);
    }

    private void dislike_Clicked(object sender, EventArgs e)
    {
        ImageButton likeButton = sender as ImageButton;
        if (likeButton != null)
        {
            var stackLayout = (likeButton.CommandParameter) as StackLayout;
            var commandParameter = stackLayout.BindingContext as EventResponseModel;
            if (commandParameter != null && viewModel != null)
            {
                viewModel.DisLikeAction(commandParameter);
            }
            Animate(likeButton);
        }
    }

    private void spam_Clicked(object sender, EventArgs e)
    {
        ImageButton likeButton = sender as ImageButton;
        if (likeButton != null)
        {
            var stackLayout = (likeButton.CommandParameter) as StackLayout;
            var commandParameter = stackLayout.BindingContext as EventResponseModel;
            if (commandParameter != null && viewModel != null)
            {
                viewModel.SpamAction(commandParameter);
            }
            Animate(likeButton);
        }
    }

    private void BannerImage_Clicked(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;
        if(imageButton != null)
        {
            viewModel.Navigate(Guid.Parse(imageButton.CommandParameter.ToString()));
        }
    }
}