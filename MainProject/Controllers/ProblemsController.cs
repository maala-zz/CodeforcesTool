using AutoMapper;
using CodeforcesTool.Entity;
using MainProject.Models.Helpers;
using MainProject.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Controllers
{
    [Route("api/problems")]
    public class ProblemsController : Controller
    {
        private readonly IRepository repo;
        private readonly IUrlHelper urlHelper;

        public ProblemsController(IRepository _repo,IUrlHelper _url)
        {
            repo = _repo;
            urlHelper = _url;
        }

        [HttpGet(Name ="GetProblems")]
        public IActionResult GetProblems(HomePageParameters homePageParameters) {

            var problemsFromRepo = repo.GetProblems(homePageParameters);

            var previousPageLink = problemsFromRepo.HasPrevious
                                    ? CreateProblemsResourceUri(homePageParameters,ResourceUriType.PreviousPage)
                                    : null;

            var nextPageLink = problemsFromRepo.HasNext
                                    ? CreateProblemsResourceUri(homePageParameters, ResourceUriType.NextPage)
                                    : null;

            var paginationMetaData = new
            {
                totalCount = problemsFromRepo.TotalCount,
                pageSize = problemsFromRepo.PageSize,
                currentPage = problemsFromRepo.CurrentPage,
                totalPages = problemsFromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetaData));

            var problems = repo.toDto(problemsFromRepo);
            
            if( ! string.IsNullOrEmpty ( homePageParameters.UserHandle))
            {
                var user = repo.GetUser(homePageParameters.UserHandle);
                if( user != null)
                {
                    foreach(var problem in problems)
                    {
                        problem.Solved = repo.IsSolved(user.Id, problem.Id);
                    }
                }
            }

            return Ok(problems);
        }

        private string CreateProblemsResourceUri(HomePageParameters homePageParameters,ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetProblems", new
                    {
                        solved = homePageParameters.Solved,
                        userName = homePageParameters.UserHandle,
                        tagName = homePageParameters.TagName,
                        pageNumber = homePageParameters.PageNumber-1,
                        pageSize = homePageParameters.PageSize
                    });

                case ResourceUriType.NextPage:
                    return Url.Link("GetProblems", new
                    {
                        solved = homePageParameters.Solved,
                        userName = homePageParameters.UserHandle,
                        tagName = homePageParameters.TagName,
                        pageNumber = homePageParameters.PageNumber + 1,
                        pageSize = homePageParameters.PageSize
                    });
                default:
                    return Url.Link("GetProblems", new
                    {
                        solved = homePageParameters.Solved,
                        userName = homePageParameters.UserHandle,
                        tagName = homePageParameters.TagName,
                        pageNumber = homePageParameters.PageNumber ,
                        pageSize = homePageParameters.PageSize
                    });

            }
        }


        [HttpGet("test")]
        public IActionResult GetUser()
        {
            var user = repo.GetUser("Daniar");
            return Ok(user);
        }
    }
}
