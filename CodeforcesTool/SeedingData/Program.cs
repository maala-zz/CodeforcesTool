using System;
using CodeforcesTool.Entity;
using SeedingData;
using SeedingData.SeedingEntity;

namespace SeedingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Class Program Start..");
            UserSeeder userSeeder = new UserSeeder();
            TagSeeder tagSeeder = new TagSeeder();
            ProblemSeeder problemSeeder = new ProblemSeeder();
            Console.ReadLine();
        }
    }
}
