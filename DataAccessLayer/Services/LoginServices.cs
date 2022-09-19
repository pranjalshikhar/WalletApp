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
        private readonly WalletAppContext _walletAppContext;
        public LoginServices(WalletAppContext walletAppContext)
        {
            _walletAppContext = walletAppContext;
        }

        public bool VerifyUser(string emailId, string password)
        {
            bool status = false;
            string emailRegex = @"[a-z0-9]+@[a-z]+\.[a-z]{2,3}";
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            try
            {
                if (Regex.IsMatch(emailId, emailRegex, RegexOptions.IgnoreCase) && Regex.IsMatch(password, passwordRegex))
                {
                    var userEmailId = _walletAppContext.User.Where(u => u.EmailId == emailId && u.Password == password).FirstOrDefault();

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

                    _walletAppContext.User.Add(user);
                    _walletAppContext.SaveChanges();
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
    }
}
