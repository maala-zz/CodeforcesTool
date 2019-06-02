using System;
using System.Collections.Generic;
using System.Text;
using CodeforcesTool.Entity;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SeedingData.EntityDto;
using System.Threading.Tasks;

namespace SeedingData.SeedingEntity
{
    class UserSeeder
    {
        private String Handles = "";
        private string HANDLES_FILE = "handles.txt";
        static HttpClient client = new HttpClient();
        private CodeforcesContext _context = new CodeforcesContext();
        public UserSeeder()
        {
            _context.Users.RemoveRange(_context.Users);
            this.seed();
            Console.WriteLine("UserSeeder Seeding Done!");
        }
        public void seed()
        {
            #region configue base url
            client.BaseAddress = new Uri("http://codeforces.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion
            ReadHandleFileAsync();
            ConfigureSeedingAsync();
        }
        public async void ConfigureSeedingAsync()
        {
            UserDto response = await CallApiAsync();
            foreach (UserJson user in response.result)
            {
                #region debug userDto
                /*
                Console.WriteLine("name: "+user.firstName+" "+user.lastName);
                Console.WriteLine("handle: " + user.Handle);
                Console.WriteLine("maxRank: " + user.maxRank);
                Console.WriteLine("Rank: " + user.Rank);
                Console.WriteLine("MaxRating: " + user.maxRating);
                Console.WriteLine("Rating: " + user.Rating);
                Console.WriteLine("###############");
                */
                #endregion
                User u = new User
                {
                    Id = Guid.NewGuid(),
                    Contribution = user.contribution,
                    Name = (user.firstName != null && user.lastName != null) ? user.firstName + " " + user.lastName : "",
                    Handle = user.Handle,
                    MaxRank = user.maxRank,
                    Rank = user.Rank,
                    MaxRating = user.maxRating,
                    Rating = user.Rating
                };
                _context.Users.Add(u);
                if (_context.SaveChanges() > 0)
                {
                    Console.WriteLine("user " + user.Handle + " added successfuly");
                }
                else
                {
                    Console.WriteLine("an error occured while adding user " + user.Handle);
                }
            }
        }
        public async Task<UserDto> CallApiAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/user.info?handles=" + Handles);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("successful attempt, processing users data");
                    string Data = await response.Content.ReadAsStringAsync();
                    UserDto userDto = JsonConvert.DeserializeObject<UserDto>(Data);
                    return userDto;
                }
                else
                {
                    Console.WriteLine("unsuccessful attempt");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new UserDto { };
        }
        public async void ReadHandleFileAsync()
        {
            try
            {
                FileStream fileStream = new FileStream(HANDLES_FILE, FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    Console.WriteLine("Reading Handles from the file " + HANDLES_FILE);
                    while (true)
                    {
                        string Name = await reader.ReadLineAsync();
                        if (Name == null) break;
                        if (Handles.Equals(""))
                            Handles += Name;
                        else
                            Handles += ";" + Name;
                    }
                    Console.WriteLine("Files readed successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
