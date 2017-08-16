using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginModel model);

        Task Register(RegisterModel model);

        Task ForgotPassword(ForgotPasswordModel model);




    }
}
