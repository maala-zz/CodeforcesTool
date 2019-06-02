using CodeforcesTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SeedingData.SeedingEntity
{
    class TagSeeder
    {
        private string TAGS_FILE = "tags.txt";
        private CodeforcesContext _context = new CodeforcesContext();
        public TagSeeder()
        {
            _context.Tags.RemoveRange(_context.Tags);
            this.seed();
            Console.WriteLine("UserSeeder Seeding Done!");
        }
        public async void seed()
        {
            try
            {
                FileStream fileStream = new FileStream(TAGS_FILE, FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    Console.WriteLine("Reading Tags from the file " + TAGS_FILE);
                    while (true)
                    {
                        string Tag = await reader.ReadLineAsync();
                        if (Tag == null) break;
                        _context.Tags.Add(new Tag { Id = Guid.NewGuid() , Title = Tag });
                        if (_context.SaveChanges() > 0)
                        {
                            Console.WriteLine("tag " + Tag + " addedd successfuly");
                        }
                        else
                        {
                            Console.WriteLine("an error occured while adding tag " + Tag);
                        }
                    }
                    Console.WriteLine("File readed successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
