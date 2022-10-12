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
            ViewBag.email = HttpContext.Session.GetString("userEmail");
            ViewBag.Transactions = _walletServices.ViewTransactions(HttpContext.Session.GetString("userEmail"));

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
                status = Convert.ToBoolean(message[0]);
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
                decimal amount = Convert.ToInt64(frm["amount"]);
                bool status = false;
                ArrayList message = _walletServices.AddMoneyUsingBank(emailId, amount, ref status);
                status = Convert.ToBoolean(message[0]);
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

        public IActionResult TransferToWallet(IFormCollection frm)
        {
            if(ModelState.IsValid)
            {
                string upi = frm["upi"];
                decimal amount = Convert.ToInt64(frm["amount"]);
                string remarks = frm["remarks"];
                string emailId = HttpContext.Session.GetString("userEmail");
                ArrayList result = _walletServices.TransferToWallet(upi, amount, remarks, emailId);
                bool status = Convert.ToBoolean(result[0]);
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
                    View("Shared", "_ErrorLayout");
                    throw;
                }
            }
            return View("LoginHome");
        }

        public IActionResult TransferToBank(IFormCollection frm)
        {
            if (ModelState.IsValid)
            {
                string accountNo = frm["account-no"];
                string accountName = frm["account-holder-name"];
                string ifsc = frm["ifsc-code"];
                decimal amount = Convert.ToInt64(frm["amount"]);
                string remarks = frm["remarks"];
                string emailId = HttpContext.Session.GetString("userEmail");
                ArrayList result = _walletServices.TransferToBank(accountNo, accountName, ifsc, amount, emailId);
                bool status = Convert.ToBoolean(result[0]);
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
                    View("Shared", "_ErrorLayout");
                    throw;
                }
            }
            return View("LoginHome");
        }


        public IActionResult ViewTransactions(IFormCollection frm)
        {
            if(ModelState.IsValid)
            {
                string emailId = HttpContext.Session.GetString("userEmail");
                var arrayList = _walletServices.ViewTransactions(emailId);
            }
            return View("LoginHome");
        }


        public IActionResult PayBills(IFormCollection frm)
        {
            if(ModelState.IsValid)
            {
                string services = frm["services"];
                decimal amount = Convert.ToInt64(frm["amount"]);
                string emailId = HttpContext.Session.GetString("userEmail");
                ArrayList result = _walletServices.PayBills(services, amount, emailId);
                bool status = Convert.ToBoolean(result[0]);
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
                    View("Shared", "_ErrorLayout");
                    throw;
                }
            }
            return View("LoginHome");
        }


        public IActionResult ChangePassword(IFormCollection frm)
        {
            if(ModelState.IsValid)
            {
                string oldPassword = frm["old-password"];
                string newPassword = frm["new-password"];
                string emailId = HttpContext.Session.GetString("userEmail");
                ArrayList result = _walletServices.ChangePassword(oldPassword, newPassword, emailId);
                bool status = Convert.ToBoolean(result[0]);
                try
                {
                    if (status)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        View("Shared", "_ErrorLayout");
                    }
                }
                catch (Exception)
                {
                    View("Shared", "_ErrorLayout");
                    throw;
                }
            }
            return View("ChangePassword");
        }
    }
}
