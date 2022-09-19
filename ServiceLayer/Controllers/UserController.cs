using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly WalletAppContext _walletAppContext;
        LoginServices _loginServices;
        WalletServices _walletServices;
        public UserController()
        {
            _walletAppContext = new WalletAppContext();
            _loginServices = new LoginServices(_walletAppContext);
            _walletServices = new WalletServices(_walletAppContext);
        }

        [HttpGet]
        public JsonResult VerifyUser(string emailId, string password)
        {
            bool status = false;
            string message = null;
            try
            {
                status = _loginServices.VerifyUser(emailId, password);
                if (status)
                    message = "Valid Credentials.";
                else
                    message = "Invalid Credentials.";
            }
            catch (Exception)
            {
                message = "Exception Caught";
                throw;
            }
            return Json(message);
        }

        [HttpPost]
        public JsonResult RegisterUser(string name, string emailId, string number, string password)
        {
            bool status = false;
            string message = null;
            try
            {
                status = _loginServices.RegisterUser(name, emailId, number, password);
                if (status)
                    message = "Registration Successful.";
                else
                    message = "Registration Failed. Try Again.";
            }
            catch (Exception)
            {
                message = "Exception Caught.";
                throw;
            }
            return Json(message);
        }

        [HttpGet]
        public JsonResult ViewBalance(string emailId)
        {
            decimal amount = -1;
            string message = null;
            try
            {
                amount = _walletServices.ViewBalance(emailId);
                if(amount > 0)
                    message = "Balance in " + emailId +" is " + amount;
            }
            catch (Exception)
            {
                message = "Exception Caught.";
                throw;
            }
            return Json(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddMoneyUsingCard(string cardNumber, string emailId, int cvv, DateTime expiryDate, decimal amount)
        {
            bool status = false;
            string message = null;
            try
            {
                status = _walletServices.AddMoneyUsingCard(cardNumber, emailId, cvv, expiryDate, amount);
                if (status)
                    message = "Money added to Wallet using Card.";
                else
                    message = "Money is not added to Wallet.";
            }
            catch (Exception)
            {
                message = "Exception Caught.";
                throw;
            }
            return Json(message);
        }
    }
}
