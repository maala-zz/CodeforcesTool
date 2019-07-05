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
        List<Correlation> GetUserSug(Guid userId);
    }
}
