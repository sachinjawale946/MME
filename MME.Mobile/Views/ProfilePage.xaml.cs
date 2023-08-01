using MME.Mobile.ViewModels;

namespace MME.Mobile.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        // txtBirthDate.MaximumDate = DateTime.Now.AddDays(-1);
        this.BindingContext = new ProfileViewModel();
        base.OnAppearing();
    }

    private void txtFirstName_Completed(object sender, EventArgs e)
    {
        txtFirstName.IsEnabled = false;
        txtFirstName.IsEnabled = true;
    }

    private void txtLastName_Completed(object sender, EventArgs e)
    {
        txtLastName.IsEnabled = false;
        txtLastName.IsEnabled = true;
    }


    private void txtMiddleName_Completed(object sender, EventArgs e)
    {
        txtMiddleName.IsEnabled = false;
        txtMiddleName.IsEnabled = true;
    }

    private void txtMobile_Completed(object sender, EventArgs e)
    {
        txtMobile.IsEnabled = false;
        txtMobile.IsEnabled = true;
    }

   
}