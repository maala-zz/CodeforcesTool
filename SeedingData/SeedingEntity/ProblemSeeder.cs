using CodeforcesTool.Entity;
using Newtonsoft.Json;
using SeedingData.EntityDto;
using SeedingData.EntityDto.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SeedingData.SeedingEntity
{
    class ProblemSeeder
    {
        private const int MAX_SUBMISSION = 100;
        static HttpClient client = new HttpClient();
        private CodeforcesContext _context = new CodeforcesContext();
        public ProblemSeeder()
        {
            _context.Problems.RemoveRange(_context.Problems);
            _context.ProblemTags.RemoveRange(_context.ProblemTags);
            _context.UserProblems.RemoveRange(_context.UserProblems);
            _context.SaveChanges();
            this.seed();
        }
        public void seed()
        {
            #region configue base url
            client.BaseAddress = new Uri("http://codeforces.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion
            GetAllUsers();
            Console.WriteLine("ProblemSeeder Seeding Done!");
        }
        public async void GetAllUsers()
        {
            List<User> Users = _context.Users.ToList();
            foreach (User user in Users)
            {
                Console.WriteLine("add user " + user.Handle + " accepted problems");
                await ConfigureSeedingAsync(user);
            }
        }
        /**
         * return Task<bool> cause GetAllUsers 
         * can't await void return type actions
         */
        public async Task<bool> ConfigureSeedingAsync(User user)
        {
            int from = 1, count = 50;
            while (from <= MAX_SUBMISSION)
            {
                ActionDto response = await CallApiAsync(user.Handle, from, count);
                if (response.status != null && response.status.Equals("OK"))
                {
                    foreach (SubmissionDto submission in response.result)
                    {
                        if (submission.verdict.Equals("OK"))
                        {
                            //          Console.WriteLine(userHandle + " submission " + submission.Problem.name +
                            //", " + submission.Problem.index + ", " + submission.verdict);
                            int contestId = submission.Problem.contestId;
                            string problemName = submission.Problem.name;
                            string index = submission.Problem.index;
                            int rating = submission.Problem.rating;
                            Problem p = _context.Problems.FirstOrDefault(problem => problem.Name == problemName);
                            if (p == null)
                            {
                                Problem problem = new Problem
                                {
                                    Id = Guid.NewGuid(),
                                    Name = problemName,
                                    Link = "https://codeforces.com/problemset/problem/" + contestId + "/" + index,
                                    Index = index,
                                    Rating = rating,
                                };
                                _context.Problems.Add(problem);
                                UserProblem userProblem = new UserProblem
                                {
                                    Problem = problem,
                                    User = user,
                                    ProblemId = problem.Id,
                                    UserId = user.Id
                                };
                                Console.WriteLine(problem.Name + ", " + user.Name +
                             ", " + problem.Id + ", " + user.Id);

                               _context.UserProblems.Add(userProblem);
                                foreach (string tag in submission.Problem.tags)
                                {
                                    Tag t = _context.Tags.FirstOrDefault(tg => tg.Title == tag);
                                    if (t != null)
                                    {
                                        _context.ProblemTags.Add(new ProblemTag { Tag = t, Problem = problem });
                                    }
                                }
                                if (_context.SaveChanges() > 0)
                                {
                                    Console.WriteLine("problem " + problemName + " added successfully with it's tags");
                                }
                                else
                                {
                                    throw new Exception("Error while adding problem " + problemName);
                                }

                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("user " + user.Handle +
                                        ", error while fetching submessions from: " + from + " count: " + count);
                }
                from += count;
                Console.WriteLine("------------------------------------------");
            }
            return true;
        }
        public async Task<ActionDto> CallApiAsync(string userHandle, int from = 1, int count = 50)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/user.status?handle=" + userHandle + "&from=" + from + "&count=" + count);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("successful attempt, processing users submission");
                    string Data = await response.Content.ReadAsStringAsync();
                    ActionDto actionDto = JsonConvert.DeserializeObject<ActionDto>(Data);
                    return actionDto;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new ActionDto { };
        }
    }
}
