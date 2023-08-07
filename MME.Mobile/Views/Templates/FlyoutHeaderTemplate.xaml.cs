namespace MME.Mobile.Views.Templates;
using MME.Mobile.Helpers;
using MME.Mobile.ViewModels;
using MME.Model.Lookups;

public partial class FlyoutHeaderTemplate : ContentView
{
	public FlyoutHeaderTemplate()
	{
		InitializeComponent();
        LabelWelcome.Text = Resx.AppResources.Welcome + " " + SecureStorage.Default.GetAsync(SecureStorage_Lookup.firstname).Result + " " + SecureStorage.Default.GetAsync(SecureStorage_Lookup.lastname).Result;
        this.BindingContext = new FlyoutHeaderViewModel();
    }
   
}