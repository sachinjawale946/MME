using MME.Mobile.ViewModels;
using MME.Model.Response;

namespace MME.Mobile.Views;

public partial class EventsPage : ContentPage
{
    EventViewModel viewModel;
    public EventsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        this.searchBar.Text = string.Empty;
        viewModel = new EventViewModel();
        this.BindingContext = viewModel;
        base.OnAppearing();
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
}