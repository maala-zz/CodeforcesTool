using CodeforcesTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProject.Models.Helpers;

namespace MainProject.Services
{
    public class Repository : IRepository
    {
        private CodeforcesContext _context = new CodeforcesContext();

        public PagedList<Problem> GetProblems(HomePageParameters homePageParameters)
        {
            var collectionBeforePaging = _context.Problems.OrderBy(p => p.Name).AsQueryable();


            // filtering by Tag Name
            if( ! string.IsNullOrEmpty(homePageParameters.TagName ))
            {
                var tagTitle = homePageParameters.TagName.Trim().ToLowerInvariant();
                var _tag = _context.Tags.FirstOrDefault(a => a.Title.ToLowerInvariant() == tagTitle);
                if( _tag != null)
                {
                    Guid tagId = _tag.Id;
                    collectionBeforePaging = collectionBeforePaging.Where( a=>  _context.ProblemTags.Where( b=> b.ProblemId == a.Id && b.TagId == tagId ).Any() );
                }
            }
    
            // filtering by [solved/not solved]
            if (!string.IsNullOrEmpty(homePageParameters.Solved))
            {
                User user = GetUser(homePageParameters.UserHandle);

                if ( homePageParameters.Solved.Equals("false") )
                {
                    collectionBeforePaging = collectionBeforePaging.Where(a => _context.UserProblems.Count( b => b.UserId==user.Id && b.ProblemId==a.Id ) ==0 );
                }
                else
                if (homePageParameters.Solved.Equals("true"))
                {
                    collectionBeforePaging = collectionBeforePaging.Where(a => _context.UserProblems.Count(b => b.UserId == user.Id && b.ProblemId == a.Id) == 1);
                }
            }

            // searching for a specific word
            if (!string.IsNullOrEmpty(homePageParameters.Search))
            {
                var word = homePageParameters.Search.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging.Where(a => a.Name.ToLowerInvariant().Contains(word));
            }

            return PagedList<Problem>.Create(collectionBeforePaging,homePageParameters.PageNumber,homePageParameters.PageSize);
        }

        public ICollection<ProblemDto> toDto(ICollection<Problem> problems) {
            var result = new List<ProblemDto>();
            var tags = new List<string>();
            tags.Add("tag1");
            tags.Add("tag2");
            tags.Add("tag3");
            foreach (var problem in problems)
            {
                result.Add(new ProblemDto()
                {
                    Id = problem.Id,
                    Name = problem.Name,
                    Rating = problem.Rating,
                    Solved = false,
                    Index = problem.Index,
                    Link = problem.Link,
                    Tags = tags 
                }
                );

            }
            return result;
        }

        public User GetUser(string userHandle)
        {
            return _context.Users.FirstOrDefault(a => a.Handle.ToLowerInvariant().Equals(userHandle.ToLowerInvariant()));
        }

        public bool IsSolved(Guid userId,Guid problemId)
        {
            return _context.UserProblems.Any(a => a.UserId == userId && a.ProblemId == problemId);
        }
    }
}
