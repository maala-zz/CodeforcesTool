using CodeforcesTool.Entity;
using MainProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models.Helpers
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private CodeforcesContext _context = new CodeforcesContext();
        public void CalculateUsersCorrelation()
        {
            _context.UserCorrelation.RemoveRange(_context.UserCorrelation);
            _context.SaveChanges();

            var users = _context.Users.ToList();

            for (int i = 0; i < users.Count(); ++i)
            {
                for (int j = i + 1; j < users.Count(); ++j)
                {
                    CodeforcesTool.Entity.User User1 = users[i];
                    CodeforcesTool.Entity.User User2 = users[j];
                    Console.WriteLine(User1.Handle + " with " + User2.Handle);
                    double user1Average = _context.UserProblems.Where(up => up.UserId == User1.Id).Select(up => up.Rating).Average();
                    double user2Average = _context.UserProblems.Where(up => up.UserId == User2.Id).Select(up => up.Rating).Average();
                    var problems = _context.Problems.ToList();

                    double cov = 0;
                    double user1StandardDeviation = 0;
                    double user2StandardDeviation = 0;

                    for (int k = 0; k < problems.Count(); ++k)
                    {
                        Problem problem = problems[k];
                        Random random = new Random();
                        double user1rating = random.NextDouble() * (5.0 - 1.0) + 1.0;
                        double user2rating = random.NextDouble() * (5.0 - 1.0) + 1.0;

                        if (_context.UserProblems.Where(up => up.UserId == User1.Id && up.ProblemId == problem.Id).Count() != 0)
                        {
                            user1rating = _context.UserProblems.Where(up => up.UserId == User1.Id && up.ProblemId == problem.Id).Select(up => up.Rating).Single();
                        }

                        if (_context.UserProblems.Where(up => up.UserId == User2.Id && up.ProblemId == problem.Id).Count() != 0)
                        {
                            user2rating = _context.UserProblems.Where(up => up.UserId == User2.Id && up.ProblemId == problem.Id).Select(up => up.Rating).Single();
                        }

                        Console.WriteLine("problem " + problem.Name + " user1 rating = " + user1rating + ", user2 rating = " + user2rating);

                        cov += (user1rating - user1Average) * (user2rating - user2Average);
                        user1StandardDeviation += (user1rating - user1Average) * (user1rating - user1Average);
                        user2StandardDeviation += (user2rating - user2Average) * (user2rating - user2Average);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------");

                    _context.UserCorrelation.Add(
                        new Correlation
                        {
                            Id = Guid.NewGuid(),
                            UserId = User1.Id,
                            User2Id = User2.Id,
                            FirstUserHandle = User1.Handle,
                            SecondUserHandle = User2.Handle,
                            FirstUserAvatar = User1.Avatar,
                            SecondUserAvatar = User2.Avatar,
                            FirstUserRating = User1.Rating.ToString(),
                            SecondUserRating = User2.Rating.ToString(),
                            Value = cov / (user1StandardDeviation * user2StandardDeviation)
                        }
                        );
                }
            }
            if (_context.SaveChanges() > 0)
            {
                Console.WriteLine("done");
            }
            else
            {
                Console.WriteLine("failed");
            }
        }

        public void CalculateUsersProblemsCorrelation()
        {
            _context.UserCorrelation.RemoveRange(_context.UserCorrelation);
            _context.SaveChanges();

            var users = _context.Users.ToList();
            for (int i = 0; i < users.Count(); ++i)
            {
                var user = users[i];
                var problems = _context.Problems.
                        Where(pr => pr.UserProblems.Any(up => up.UserId == user.Id)).ToList();
                for (int j = 0; j < problems.Count(); ++j)
                {
                    var problem = problems[j];
                    var tags = _context.ProblemTags.Where(pt => pt.ProblemId == problem.Id).ToList();
                    for (int k = 0; k < tags.Count(); ++k)
                    {
                        var tag = _context.Tags.Where(t => t.Id == tags[k].TagId).SingleOrDefault();
                        var record = _context.UseTagRating.Where(ut => ut.UserId == user.Id
                                              && ut.TagId == tag.Id).SingleOrDefault();
                        if (record == null)
                        {
                            record = new UseTagRating { Id = Guid.NewGuid(), UserId = user.Id, TagId = tag.Id, Rating = 1 };
                            _context.UseTagRating.Add(record);
                            _context.SaveChanges();
                        }
                        else
                        {
                            record.Rating += 1;
                            _context.SaveChanges();
                        }
                        Console.WriteLine("user " + user.Handle + " tag " + tag.Title);
                    }
                }
            }
        }

        public List<Correlation> GetUserFriendSug(Guid userId)
        {
            var res = _context.UserCorrelation.Where(corr => corr.User2Id == userId || corr.UserId == userId).OrderByDescending(corr => corr.Value).ToList();
            Console.WriteLine(res.Count());
            return res;
        }

        public List<ProblemDto> GetUserProblemSug(Guid userId)
        {
            List<ProblemDto> items = new List<ProblemDto>();
            var res = _context.Problems.Where(p => 1 == 1).ToList();
            for (int i = 0; i < res.Count(); ++i)
            {
                var problem = res[i];
                var tags = _context.ProblemTags.Where(pt => pt.ProblemId == problem.Id).ToList();
                var ExcpectedRating = 0;
                for (int j = 0; j < tags.Count(); ++j)
                {
                    var tag = tags[j];
                    var record = _context.UseTagRating.
                        Where(utr => utr.TagId == tag.TagId && utr.UserId == userId).SingleOrDefault();
                    if (record != null)
                        ExcpectedRating += record.Rating;
                }
                if (ExcpectedRating != 0)
                {
                    if (items.Count() < 10)
                        items.Add(new ProblemDto {
                             Name = problem.Name,
                             Rating = problem.Rating,
                             Index = problem.Index,
                             Link = problem.Link
                        });
                }
            }

            return items;
        }
    }
}
