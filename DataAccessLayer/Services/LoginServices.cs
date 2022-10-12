using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataAccessLayer.Services
{
    public class LoginServices
    {
        private readonly WalletAppContext walletAppContext;
        public LoginServices(WalletAppContext _walletAppContext)
        {
            walletAppContext = _walletAppContext;
        }

        public bool VerifyUser(string emailId, string password)
        {
            bool status = false;
            string emailRegex = @"[a-z0-9]+@[a-z]+\.[a-z]{2,3}";
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            try
            {
                if (!string.IsNullOrEmpty(emailId) && Regex.IsMatch(emailId, emailRegex) && !string.IsNullOrEmpty(password) && Regex.IsMatch(password, passwordRegex))
                {
                    var userEmailId = walletAppContext.User.Where(u => u.EmailId == emailId && u.Password == password).FirstOrDefault();

                    if (userEmailId != null)
                        status = true;
                    else
                        status = false;
                }
                else
                    status = false;
            }
            catch (Exception)
            {
                status = false;
                throw;
            }
            return status;
        }

        public bool RegisterUser(string name, string emailId, string number, string password)
        {
            bool status = false;
            string emailRegex = @"[a-z0-9]+@[a-z]+\.[a-z]{2,3}";
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            string nameRegex = @"^[_A-z0-9]*((-|\s)*[_A-z0-9])*$";
            string numberRegex = @"[1-9]{1}\d{9}";
            try
            {
                if (Regex.IsMatch(emailId, emailRegex) && Regex.IsMatch(password, passwordRegex) && Regex.IsMatch(name, nameRegex) && Regex.IsMatch(number, numberRegex))
                {
                    User user = new User
                    {
                        Name = name,
                        EmailId = emailId,
                        Password = password,
                        MobileNumber = number,
                        StatusId = 1
                    };

                    walletAppContext.User.Add(user);
                    walletAppContext.SaveChanges();
                    status = true;
                }
                else
                    status = false;
            }
            catch (Exception)
            {
                status = false;
                throw;
            }
            return status;
        } 

        public string GetUserByUserName(string emailId)
        {
            try
            {
                var userName = (from u in walletAppContext.User
                                where u.EmailId == emailId
                                select u.Name).FirstOrDefault();
                return userName;
            }
            catch (Exception)
            {
                return "Exception Caught.";
                throw;
            }
        }

        //public string GetUserEmail(string emailId)
        //{

        //}
    }
}
