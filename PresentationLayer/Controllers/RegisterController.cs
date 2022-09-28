using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly WalletAppContext _walletAppContext;
        LoginServices _loginServices;

        public RegisterController(WalletAppContext walletAppContext)
        {
            _walletAppContext = walletAppContext;
            _loginServices = new LoginServices(_walletAppContext);
        }

        public IActionResult SaveRegister(IFormCollection frm)
        {
            if(ModelState.IsValid)
            {
                string emailId = frm["emailId"];
                string password = frm["password"];
                string number = frm["number"];
                string userName = frm["userName"];

                bool returnValue = _loginServices.RegisterUser(userName, emailId, number, password);

                try
                {
                    if(returnValue)
                    {
                        HttpContext.Session.SetString("userName", userName);
                        HttpContext.Session.SetString("emailId", emailId);
                        return RedirectToAction("LoginHome", "Login");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View("LoginHome");
        }
    }
}
