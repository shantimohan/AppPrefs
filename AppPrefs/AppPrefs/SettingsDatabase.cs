using AppPrefs.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPrefs
{
    public class SettingsDatabase
    {
        private SQLiteAsyncConnection dbConn;
        public static SettingsDatabase instance;

        public SettingsDatabase() { }

        public SettingsDatabase(string dbPath)
        {
            dbConn = new SQLiteAsyncConnection(dbPath);
            dbConn.CreateTableAsync<Settings>().Wait();
        }

        public static SettingsDatabase Initialize(string filename)
        {
            if (instance != null)
                instance.dbConn.GetConnection().Dispose();

            instance = new SettingsDatabase(filename);

            return instance;
        }

        public async Task RecreateDB()
        {
            await dbConn.DropTableAsync<Settings>();
            await dbConn.CreateTableAsync<Settings>();
        }

        //DBOP: Get total item count from people table
        public async Task<int> GetTotalSettingsCount()
        {
            return await dbConn.Table<Settings>().CountAsync();
        }

        public async Task<bool> Add2SettingsAsync(string setting_str)
        {
            //DBOP: Insert item into table People
            await dbConn.InsertAsync(new Settings { strSettings = setting_str});

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateSettingsAsync(Settings appSettings)
        {
            //DBOP: Update item in table People
            await dbConn.UpdateAsync(appSettings);

            return await Task.FromResult(true);
        }

        public async Task<Settings> GetSettingsAsync(int id)
        {
            return await dbConn.ExecuteScalarAsync<Settings>($"SELECT * FROM [settings] WHERE [Id] = {id}");
        }

        //public async Task<bool> DeleteSettingsAsync(Settings appSettings)
        //{
        //    //DBOP: Delete item from table People
        //    await dbConn.DeleteAsync(appSettings);

        //    return await Task.FromResult(true);
        //}

        //DBOP: Get all existing data from people table
        //public Task<List<Settings>> GetAllPeopleAsync()
        //{
        //    // return a list of people saved to the Settings table in the database
        //    return dbConn.Table<Settings>().ToListAsync();
        //}


    }  // end of: class SettingsDatabase

}  // end of: namespace

