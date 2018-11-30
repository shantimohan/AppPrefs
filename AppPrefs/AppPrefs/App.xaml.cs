using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppPrefs
{
    public partial class App : Application
    {
        //DBOP: Initialize database 
        internal static SettingsDatabase SettingsDB;

        public const string APP_SETTINGS_KEY = "AppSettings";

        public static string appName = "App Prefs";
        public static string appVersion = "1.0";

        public App()
        {
            InitializeComponent();

            SettingsDB = SettingsDatabase.Initialize(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SettingsSQLite.db3"));

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Indigo,
                BarTextColor = Color.White
            };
        }

        internal static void SetAppTitleAndToday(Label lblAppTitle, Label lblTodayIs)
        {
            lblAppTitle.Text = $"{App.appName} {App.appVersion}";
            DateTime today = DateTime.Today;
            string dtSuffix = "";
            switch (today.Day)
            {
                case 1:
                    dtSuffix = "st";
                    break;
                case 2:
                    dtSuffix = "nd";
                    break;
                case 3:
                    dtSuffix = "rd";
                    break;
                default:
                    dtSuffix = "th";
                    break;
            }
            lblTodayIs.Text = $"{today.ToString("MMM")} {today.Day}{dtSuffix}, {today.Year}";
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
