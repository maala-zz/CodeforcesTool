using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MainProject.Models;
using MainProject.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MainProject.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        IUserServices _userServices;
        public AuthController(IUserServices userServices)
        {
            this._userServices = userServices;
        }
        [Route("signin")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto;
                if (await _userServices.ValidateCredentials(model.Handle, model.Password, out userDto))
                {
                    await SignInUser(userDto.Handle);
                    if (returnUrl != null)
                        return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(string handle)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, handle),
                new Claim("handle", handle)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "handle", null);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignUpModel model, string returnUrl = null)
        {
            User user;
            //if (ModelState.IsValid)
            //{
            //    if( await _userServices.Signup(model,out user))
            //    {
            //        await SignInUser(model.Handle);
            //        Console.WriteLine("user.Id "+ user.Id);
            //        Console.WriteLine("user.Handle " + user.Handle);
            //        Console.WriteLine("user.Password " + user.Password);
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}
            await _userServices.Signup(model, out user);
            Console.WriteLine("user.Id " + user.Id);
            Console.WriteLine("user.Handle " + user.Handle);
            Console.WriteLine("user.Password " + user.Password);
            Console.WriteLine("handle " + model.Handle);
            Console.WriteLine("password " + model.Password);
            Console.WriteLine("confirm password " + model.ConfirmPassword);
            return RedirectToAction("Index", "Home");
        }
    }
}