using MotivateMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe.DataAccess
{
    public interface IDataAccess
    {
        Task<int> SaveActivityAsync(Activity activity);

        Task<int> DeleteItemAsync(Activity activity);

        Task<List<Activity>> GetActivitiesAsync();

        Task<List<Activity>> GetActivitiesRollingSevenAsync();

    }
}
