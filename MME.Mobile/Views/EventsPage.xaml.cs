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
        if(viewModel == null) viewModel= new EventViewModel();
        this.BindingContext = viewModel;
        base.OnAppearing();
    }

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
        }
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
        }
    }
}