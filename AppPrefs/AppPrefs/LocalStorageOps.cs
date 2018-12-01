using AppPrefs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XPA_FileOps_XFPU;

namespace AppPrefs
{
    public class LocalStorageOps
    {
        public static MyAppState SetDefaultAppSettings()
        {
            Debug.WriteLine("In SetDefaultAppSettings");

            MyAppState myAppState = new MyAppState();

            myAppState.AppSettingsVersion = null;
            myAppState.EmailAddress = "myname@domain.com";
            myAppState.AdvtsConsented = false;
            myAppState.CutoffDate = Convert.ToDateTime("1/1/2000");
            myAppState.SelectedSize = "Large";
            myAppState.EventsDeleteThreshold = 3;
            myAppState.MaxDiscountPct = 5;

            return myAppState;

        }  // end of: SetDefaultAppSettings()

        #region SQLite DB Table
        public static async Task<MyAppState> LoadAppSettingsFromDB()
        {
            MyAppState myAppState = await ReadTableAsync();

            if (myAppState == null)
            {
                // Set default settings
                MyAppState tState = SetDefaultAppSettings();

                // Save default settings
                await App.SettingsDB.Add2SettingsAsync(Serialize(tState));

                // Read the saved default settings
                myAppState = await ReadTableAsync();
            }

            return myAppState;
        }

        public static async Task SaveAppSettingsToDB(MyAppState myAppStateIn)
        {
            string str = Serialize(myAppStateIn);

            Settings settings = new Settings
            {
                Id = 1,
                strSettings = str,
            };

            await App.SettingsDB.UpdateSettingsAsync(settings);
        }

        private static async Task<MyAppState> ReadTableAsync()
        {
            MyAppState myAppState = null;

            var settings = await App.SettingsDB.GetSettingsAsync(1);

            if (settings != null  && settings.Count > 0)
                myAppState = Deserialize<MyAppState>(settings[0].strSettings);

            return myAppState;
        }

        #endregion SQLite DB Table

        #region File Based Settings
        public static async Task<MyAppState> LoadAppSettingsFromFile()
        {
            Debug.WriteLine("In LoadAppSettingsFromFile");

            MyAppState myAppState = await ReadObjectAsync(App.APP_SETTINGS_KEY);

            if (myAppState == null)
            {
                Debug.WriteLine("App Settings not exist - setting defaults");

                // Set default settings
                MyAppState tState = SetDefaultAppSettings();

                // Save default settings
                await SaveAppSettingsToFile(tState);

                // Read the saved default settings
                myAppState = await ReadObjectAsync(App.APP_SETTINGS_KEY);
            }

            return myAppState;
        }

        public static async Task SaveAppSettingsToFile(MyAppState myAppStateIn)
        {
            Debug.WriteLine("In SaveAppSettingsToFile");

            await SaveObjectAsync(App.APP_SETTINGS_KEY, myAppStateIn);
        }

        private static async Task<MyAppState> ReadObjectAsync(string filename)
        {
            Debug.WriteLine("In ReadObjectAsync");

            MyAppState myAppState = null;

            if (await FileHelper.ExistsAsync(filename))
            {
                string str = await FileHelper.ReadTextAsync(filename);
                Debug.WriteLine($"Read from file - str ({str.Length}): {str}");
                myAppState = Deserialize<MyAppState>(str);
            }

            return myAppState;
        }

        public static async Task SaveObjectAsync<T>(string filename, T objectToSave)
        {
            Debug.WriteLine("In SaveObjectAsync");

            string str = Serialize(objectToSave);
            Debug.WriteLine($"Writing to file - str: {str}");
            await FileHelper.WriteTextAsync(filename, str);
        }
        #endregion File Based Settings

        #region Xamarin Essentials Preferences
        public static MyAppState LoadAppSettingsFromXEPrefs()
        {
            MyAppState myAppState = null;
            string s = Preferences.Get(App.APP_SETTINGS_KEY, null);

            if (s != null)
                myAppState = GetObject<MyAppState>(App.APP_SETTINGS_KEY);

            if (myAppState == null)
            {
                // Set default settings
                MyAppState tState = SetDefaultAppSettings();

                // Save default settings
                SaveAppSettingsToXEPrefs(tState);

                // Read the saved default settings
                myAppState = GetObject<MyAppState>(App.APP_SETTINGS_KEY);
            }

            return myAppState;
        }

        public static void SaveAppSettingsToXEPrefs(MyAppState myAppStateIn)
        {
            SaveObject(App.APP_SETTINGS_KEY, myAppStateIn);
        }

        public static void ClearAppSettingsInXEPrefs()
        {
            DeleteObject(App.APP_SETTINGS_KEY);
        }

        public static T GetObject<T>(string key)
        {
            string str = Preferences.Get(key, "");
            Debug.WriteLine($"Read from Preferences - str ({str.Length}): {str}");
            return Deserialize<T>(str);
        }

        public static void SaveObject<T>(string key, T objectToSave)
        {
            string str = Serialize(objectToSave);
            Preferences.Set(key, str);
        }

        public static void DeleteObject(string key)
        {
            //Preferences.Remove(key);
        }
        #endregion Xamarin Essentials Preferences

        private static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static T Deserialize<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

    }  // end of: class LocalStorageOps

}  // end of : namespace

