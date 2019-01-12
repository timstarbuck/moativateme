using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe.Services
{
    public interface IAchievementCalculator
    {
        Task<bool> CalculateAchievementsAsync();
    }
}
