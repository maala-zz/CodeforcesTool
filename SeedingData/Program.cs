﻿using System;
using CodeforcesTool.Entity;
using SeedingData.SeedingEntity;
using MainProject.Models.Helpers;

namespace SeedingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Class Program Start..");
            //    UserSeeder userSeeder = new UserSeeder();
            //TagSeeder tagSeeder = new TagSeeder();
            //       ProblemSeeder problemSeeder = new ProblemSeeder();
            RecommendationRepository rec = new RecommendationRepository();
            var list = rec.GetUserProblemSug(new Guid("d71ec257-97da-40e0-b400-092651c2bbac"));

            Console.ReadLine();
        }
    }
}
