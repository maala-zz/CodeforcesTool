using CodeforcesTool.Entity;
using MainProject.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Services
{
    public interface IRecommendationRepository
    {
        void CalculateUsersCorrelation();
        void CalculateUsersProblemsCorrelation();
        List<Correlation> GetUserFriendSug(Guid userId);
        List<Problem> GetUserProblemSug(Guid userId);
    }
}
