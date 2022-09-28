using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly WalletAppContext _walletAppContext;
        WalletServices _walletServices;
        LoginServices _loginServices;

        public LoginController(WalletAppContext walletAppContext)
        {
            _walletAppContext = walletAppContext;
            _walletServices = new WalletServices(_walletAppContext);
            _loginServices = new LoginServices(_walletAppContext);
        }

        public IActionResult LoginHome()
        {
            ViewBag.amount = _walletServices.ViewBalance(HttpContext.Session.GetString("userEmail"));
            return View();
        }

        //public IActionResult CheckRole(IFormCollection frm)
        //{
        //    string emailId = frm["emailId"];
        //    string password = frm["password"];
        //    string userName = _loginServices.GetUserByUserName(emailId);
        //    try
        //    {
        //        bool status = _loginServices.VerifyUser(emailId, password);
        //        if (status)
        //        {
        //            HttpContext.Session.SetString("userName", userName);
        //            HttpContext.Session.SetString("userEmail", emailId);
        //            return RedirectToAction("LoginHome", "Login");
        //        }
        //        else
        //            View("Error");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return View("LoginHome");
        //}
    }
}
