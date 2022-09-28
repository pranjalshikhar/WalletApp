﻿using DataAccessLayer.Models;
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

        public IActionResult Register()
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
                    View("Home", "Error");
            }
            catch (Exception)
            {
                throw;
            }
            return View("LoginHome");
        }
    }
}
