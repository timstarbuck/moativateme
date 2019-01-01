using MotivateMe.DataAccess;
using MotivateMe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe
{
    public class SqliteDataAccess : IDataAccess
    {
        readonly SQLiteAsyncConnection _database;

        private static SqliteDataAccess _instance;

        public SqliteDataAccess(string dbPath)
        {
            if (String.IsNullOrEmpty(dbPath))
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MotivateMe.db3");
            }
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Activity>().Wait();
        }

        public static SqliteDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqliteDataAccess(null);
            }
            return _instance;
        }

        public Task<List<Activity>> GetActivitiesAsync()
        {
            return _database.Table<Activity>().ToListAsync();
        }

            
        public Task<List<Activity>> GetActivitiesRollingSevenAsync()
        {
            var from = DateTime.Now.AddDays(-7);
            return _database.Table<Activity>().Where(a => a.Timestamp >= from).ToListAsync();
        }

        public Task<int> SaveActivityAsync(Activity activity)
        {
            if (activity.ID != 0)
            {
                return _database.UpdateAsync(activity);
            }
            else
            {
                return _database.InsertAsync(activity);
            }
        }

        public Task<int> DeleteItemAsync(Activity item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
