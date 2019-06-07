using MainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Services
{
    public interface IUserServices
    {
        Task<bool> Signup(SignUpModel model, out User user);
        Task<bool> ValidateCredentials(string handle, string password, out UserDto userDto);
    }
}
