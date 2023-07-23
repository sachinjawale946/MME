namespace MME.Mobile.Views.Templates;
using MME.Mobile.Helpers;

public partial class FlyoutHeaderTemplate : ContentView
{
	public FlyoutHeaderTemplate()
	{
		InitializeComponent();
        LabelWelcome.Text = "Welcome, " + Settings.firstname + " " + Settings.lastname;
    }
}