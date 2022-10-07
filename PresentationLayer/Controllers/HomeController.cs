using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly WalletAppContext _walletAppContext;
        LoginServices _loginServices;

        public HomeController(WalletAppContext walletAppContext)
        {
            _walletAppContext = walletAppContext;
            _loginServices = new LoginServices(_walletAppContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckRole(IFormCollection frm)
        {
            string emailId = frm["emailId"];
            string password = frm["password"];
            string userName = _loginServices.GetUserByUserName(emailId);
            try
            {
                bool status = _loginServices.VerifyUser(emailId, password);
                if (status)
                {
                    HttpContext.Session.SetString("userName", userName);
                    HttpContext.Session.SetString("userEmail", emailId);
                    return RedirectToAction("LoginHome", "Login");
                }
                else
                    View("Shared", "_ErrorLayout");
            }
            catch (Exception)
            {
                throw;
            }
            return View("Error");
        }

        public IActionResult SaveRegister(IFormCollection frm)
        {
            if (ModelState.IsValid)
            {
                string emailId = frm["emailId"];
                string password = frm["password"];
                string number = frm["number"];
                string userName = frm["userName"];

                bool returnValue = _loginServices.RegisterUser(userName, emailId, number, password);

                try
                {
                    if (returnValue)
                    {
                        HttpContext.Session.SetString("userName", userName);
                        HttpContext.Session.SetString("emailId", emailId);
                        return RedirectToAction("LoginHome", "Login");
                    }
                    else
                    {
                        View("Shared", "_ErrorLayout");
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
