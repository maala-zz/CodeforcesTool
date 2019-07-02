using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProject.Models;

namespace MainProject.Services
{
    public class UserServices : IUserServices
    {
        AuthContext _context;
        public UserServices(AuthContext ctx)
        {
            this._context = ctx;
        }

        public Task<bool> Signup(SignUpModel model, out User user)
        {
            user = null;
            User newUser = new User { Id = Guid.NewGuid(), Handle = model.Handle, Password = model.Password };
            User tempUser = _context.Users.FirstOrDefault(u => u.Handle == model.Handle);
            if (tempUser == null)
            {
                _context.Users.Add(newUser);
                if (_context.SaveChanges() > 0)
                {
                    user = newUser;
                    return Task.FromResult(true);
                }
                else
                    Task.FromResult(false);
            }
            return Task.FromResult(false);

        }

        public Task<bool> ValidateCredentials(string handle, string password, out UserDto userDto)
        {
            userDto = null;
            User user = _context.Users.FirstOrDefault(u => u.Handle == handle && u.Password == password);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            userDto = new UserDto { Handle = handle, Password = password };
            return Task.FromResult(true);
        }
    }
}
