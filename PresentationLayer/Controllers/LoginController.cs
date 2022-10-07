using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;

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
            ViewBag.userName = _loginServices.GetUserByUserName(HttpContext.Session.GetString("userName"));
            return View();
        }

        public IActionResult AddCardMoney(IFormCollection frm)
        {
            if(ModelState.IsValid)
            {
                string cardNumber = frm["cardNumber"];
                DateTime expiry = Convert.ToDateTime(frm["expiry"]);
                int cvv = Convert.ToInt32(frm["cvv"]);
                decimal amount = Convert.ToInt64(frm["amount"]);
                string emailId = HttpContext.Session.GetString("userEmail");
                bool status = false;
                ArrayList message = _walletServices.AddMoneyUsingCard(cardNumber, emailId, cvv, expiry, amount, ref status);
                try
                {
                    if(status)
                    {
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

        public IActionResult AddBankMoney(IFormCollection frm)
        {
            if (ModelState.IsValid)
            {
                string emailId = HttpContext.Session.GetString("userEmail");
                string password = HttpContext.Session.GetString("userPassword");
                decimal amount = Convert.ToInt64(frm["net-bank-amount"]);
                bool status = false;
                ArrayList message = _walletServices.AddMoneyUsingBank(emailId, password, amount, ref status);
                try
                {
                    if (status)
                    {
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
