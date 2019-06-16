using CodeforcesTool.Entity;
using MainProject.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Services
{
    public interface IRepository
    {
        PagedList<Problem> GetProblems(HomePageParameters homePageParameters);

        ICollection<ProblemDto> toDto(ICollection<Problem> problems);

        bool IsSolved(Guid userId, Guid problemId);

        User GetUser(string userHandle);
    }
}
