using CommunityToolkit.Maui.Views;

namespace MME.Mobile.Views;

public partial class SessionExpiredPage : Popup
{
	public SessionExpiredPage()
	{
		InitializeComponent();
	}

    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}