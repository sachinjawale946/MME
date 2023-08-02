using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MME.Mobile.ViewModels;
using Mopups.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace MME.Mobile.Views;

public partial class ProfilePage : ContentPage
{
    ProfileViewModel viewModel;

    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        viewModel = new ProfileViewModel();
        this.BindingContext = viewModel;
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

    private void txtAddressLine_Completed(object sender, EventArgs e)
    {
        txtAddressLine.IsEnabled = false;
        txtAddressLine.IsEnabled = true;
    }

    private void txtArea_Completed(object sender, EventArgs e)
    {
        txtArea.IsEnabled = false;
        txtArea.IsEnabled = true;
    }

    private void txtLandmark_Completed(object sender, EventArgs e)
    {
        txtLandmark.IsEnabled = false;
        txtLandmark.IsEnabled = true;
    }

    private void txtCity_Completed(object sender, EventArgs e)
    {
        txtCity.IsEnabled = false;
        txtCity.IsEnabled = true;
    }

    private void txtEmail_Completed(object sender, EventArgs e)
    {
        txtEmail.IsEnabled = false;
        txtEmail.IsEnabled = true;
    }

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    SnackbarOptions snackbarOptions = new SnackbarOptions
    {
        BackgroundColor = Colors.Red,
        TextColor = Colors.White,
        ActionButtonTextColor = Colors.Yellow,
        CornerRadius = new CornerRadius(5),
        Font = Microsoft.Maui.Font.SystemFontOfSize(12),
        ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
        CharacterSpacing = 0.1,
    };

    private async void AddImageButton_Clicked(object sender, EventArgs e)
    {

        var cameraPermission = await CheckCameraPermission();
        if (cameraPermission == false) return;

        try
        {
            var options = new StoreCameraMediaOptions { CompressionQuality = 0 };
            var result = await CrossMedia.Current.TakePhotoAsync(options);
            if (result is null) return;

            var fileInfo = new FileInfo(result?.Path);
            var fileLength = fileInfo.Length;
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            await new TaskFactory().StartNew(() =>
            {
                Thread.Sleep(1000);
            });
            viewModel.Profile.profilepic = read(result.Path);
            viewModel.Profile.shownoimage = false;
            viewModel.Profile.showprofileimage = true;
            await MopupService.Instance.PopAsync(true);
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make(ex.Message, null, Resx.AppResources.Ok, TimeSpan.FromSeconds(8), snackbarOptions, lblMiddleName);
            await snackbar.Show(cancellationTokenSource.Token);
        }

    }

    private async void RemoveImageButton_Clicked(object sender, EventArgs e)
    {
        if (viewModel.Profile.profilepic != null && viewModel.Profile.profilepic.Length > 0)
        {
            viewModel.Profile.profilepic = null;
            viewModel.Profile.shownoimage = true;
            viewModel.Profile.showprofileimage = false;
            var snackbar = Snackbar.Make(Resx.AppResources.Message_Profile_Picture_Delete, null, Resx.AppResources.Ok, TimeSpan.FromSeconds(8), snackbarOptions, lblMiddleName);
            await snackbar.Show(cancellationTokenSource.Token);
        }
        else
        {
            var snackbar = Snackbar.Make(Resx.AppResources.Validation_Message_Profile_Picture_Delete, null, Resx.AppResources.Ok, TimeSpan.FromSeconds(8), snackbarOptions, lblMiddleName);
            await snackbar.Show(cancellationTokenSource.Token);
        }
    }

    private async Task<bool> CheckCameraPermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status == PermissionStatus.Granted)
        {
            return true;
        }
        else
        {
            status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                var snackbar = Snackbar.Make(Resx.AppResources.Validation_Message_Camera_Permission, null, Resx.AppResources.Ok, TimeSpan.FromSeconds(8), snackbarOptions, lblMiddleName);
                await snackbar.Show(cancellationTokenSource.Token);
                return false;
            }
        }
    }

    private byte[] read(string path)
    {
        using (var streamReader = new StreamReader(path))
        {
            using (var memoryStream = new MemoryStream())
            {
                streamReader.BaseStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }


}