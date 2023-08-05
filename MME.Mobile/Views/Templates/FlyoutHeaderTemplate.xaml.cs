namespace MME.Mobile.Views.Templates;
using MME.Mobile.Helpers;
using MME.Mobile.ViewModels;

public partial class FlyoutHeaderTemplate : ContentView
{
	public FlyoutHeaderTemplate()
	{
		InitializeComponent();
        LabelWelcome.Text = Resx.AppResources.Welcome + " " + Settings.firstname + " " + Settings.lastname;
        this.BindingContext = new FlyoutHeaderViewModel();
    }
   
}