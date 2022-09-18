using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            try
            {
                var userEmailId = _walletAppContext.User.Where(u => u.EmailId == emailId && u.Password == password).FirstOrDefault();

                if (userEmailId != null)
                    status = true;
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
            try
            {
                User user = new User();
                user.Name = name;
                user.EmailId = emailId;
                user.Password = password;
                user.MobileNumber = number;
                user.StatusId = 1;

                _walletAppContext.User.Add(user);
                _walletAppContext.SaveChanges();
                status = true;
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
