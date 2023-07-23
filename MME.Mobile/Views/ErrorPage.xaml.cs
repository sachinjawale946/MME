using CommunityToolkit.Maui.Views;
namespace MME.Mobile.Views;

public partial class ErrorPage : Popup
{
	public ErrorPage(string errorMessage)
	{
		InitializeComponent();
		lblError.Text = errorMessage;
	}

    private void Close_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}