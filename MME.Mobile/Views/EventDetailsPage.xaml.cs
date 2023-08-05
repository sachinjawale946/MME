using Microsoft.Extensions.Logging;
using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

[QueryProperty(nameof(Event), "Event")]
public partial class EventDetailsPage : ContentPage
{
    public EventDetailsPage()
    {
        InitializeComponent();
    }

    string _event;
    public string Event
    {
        get => _event;
        set
        {
            _event = value;
            OnPropertyChanged();
        }
    }




    protected override void OnAppearing()
    {
        var viewModel = new EventDetailsViewModel(Guid.Parse(Event));
        this.BindingContext = viewModel;
        base.OnAppearing();
    }

    private double _scale = 0;
    private async void Animate(ImageButton button)
    {
        _scale = button.Scale;
        await button.ScaleTo(_scale * 1.2, 1000);
        await button.ScaleTo(_scale, 1000);
    }

    private void like_Clicked(object sender, EventArgs e)
    {

    }

    private void spam_Clicked(object sender, EventArgs e)
    {

    }

    private void dislike_Clicked(object sender, EventArgs e)
    {

    }
}