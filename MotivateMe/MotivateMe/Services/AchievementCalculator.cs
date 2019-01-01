using MotivateMe.DataAccess;
using MotivateMe.Models;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MotivateMe
{
    public class AchievementCalculator
    {
        private readonly IDataAccess _dac;
        private static  AchievementCalculator _instance;
        private readonly int _notifyId = 101;

        // todo make this configurable
        private class Goals
        {
            public static int Bronze = 1200;
            public static int Silver = 2000;
            public static int Gold = 3000;
        }

        public AchievementCalculator(IDataAccess dac)
        {
            _dac = dac;
        }

        public static AchievementCalculator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AchievementCalculator(SqliteDataAccess.GetInstance());
            }
            return _instance;
        }
        public async Task<bool> CalculateAchievementsAsync()
        {
            var activities = await _dac.GetActivitiesRollingSevenAsync();
            var total = 0.0;
            ((List<Activity>)activities).ForEach((a => total += a.MetricTwo));

            var title = "Uh Oh";
            var body = String.Format("We have some work to do. ({0:0,0})", total);
            var achievement = "Tin";

            if (total >= Goals.Gold)
            {
                title = "Achievement Unlocked!";
                body = String.Format("Keep up the good work! ({0:0,0})", total);
                achievement = "Gold";

            } else if (total >= Goals.Silver)
            {
                title = "Almost There!";
                body = String.Format("Don't stop now, gold is just around the corner! ({0:0,0})", total);
                achievement = "Silver";

            }
            else if (total >= Goals.Bronze)
            {
                title = "Great Start!";
                body = String.Format("Keep pushing! ({0:0,0})", total);
                achievement = "Bronze";

            }

            MessagingCenter.Send<AchievementCalculator, string>(this, "Achievement", achievement);
            Console.WriteLine("Local notification: {0}", body);
            CrossLocalNotifications.Current.Cancel(_notifyId);
            CrossLocalNotifications.Current.Show(title, body, _notifyId);

            return true;
        }
    }
}
