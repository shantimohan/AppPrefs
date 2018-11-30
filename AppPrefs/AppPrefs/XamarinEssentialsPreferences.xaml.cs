using AppPrefs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrefs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XamarinEssentialsPreferences : ContentPage
	{
        public const string APP_SETTINGS_VERSION = "AppSettings Version 1000";

        private AppSettings myAppSettings = new AppSettings();

        public static bool AppSettingsVersionChanged = false;

        public XamarinEssentialsPreferences ()
		{
			InitializeComponent ();

            App.SetAppTitleAndToday(this.AppTitle, this.TodayIs);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckAppSettings();
        }

        private void CheckAppSettings()
        {
            // Read Settings
            myAppSettings.SetMyAppState(LocalStorageOps.LoadAppSettingsFromXEPrefs());

            // Check and set App Settings Version
            if (myAppSettings.AppSettingsVersion == null)
            {
                myAppSettings.AppSettingsVersion = APP_SETTINGS_VERSION;
                AppSettingsVersionChanged = false;
            }
            else if (myAppSettings.AppSettingsVersion != APP_SETTINGS_VERSION)
                AppSettingsVersionChanged = true;
            else
                AppSettingsVersionChanged = false;

            // Set default values for settings that have changed / added
            if (AppSettingsVersionChanged)
            {
                // Check and set settings that changed
                int currentVersion = Convert.ToInt32(myAppSettings.AppSettingsVersion.Substring(20));

                //switch (currentVersion)
                //{
                //    case 3000:
                //        myAppSettings.IsScreenLockingOnTimeoutEnabled = true;
                //        break;
                //    case 3001:
                //        myAppSettings.DelegateeEmailLanguage = "English";
                //        break;
                //}

                myAppSettings.AppSettingsVersion = APP_SETTINGS_VERSION;

                // Save current app settings
                LocalStorageOps.SaveAppSettingsToXEPrefs(myAppSettings.GetMyAppState());
            }

            BindingContext = myAppSettings;

            SetAdvtStatus();
        }  // end of: CheckAppSettings()

        private void SetAdvtStatus()
        {
            if (swtAdStatus.IsToggled)
                lblAdStatus.Text = "Consented";
            else
                lblAdStatus.Text = "NOT Consented";
        }

        private void swtAdStatus_Toggled(object sender, ToggledEventArgs e)
        {
            SetAdvtStatus();
        }

        private async void tbiSave_Clicked(object sender, EventArgs e)
        {
            // Save current app settings
            LocalStorageOps.SaveAppSettingsToXEPrefs(myAppSettings.GetMyAppState());
            await DisplayAlert("App Settings", "App Settings SAVED", "OK");
        }

    }  // end of: partial class XamarinEssentialsPreferences : ContentPage

}  // end of: namespace
