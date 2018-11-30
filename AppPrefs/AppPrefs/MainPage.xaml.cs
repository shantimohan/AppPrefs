using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPrefs
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            App.SetAppTitleAndToday(this.AppTitle, this.TodayIs);
        }

        private void btnFileBasedSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FileBasedSettings(), true);
        }

        private void btnXamarinEssentialsPreferences_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new XamarinEssentialsPreferences(), true);
        }

        private void btnSQLiteSettingsTable_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SQLiteSettingsTable(), true);
        }
    }
}
