using DataAccessLayer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace DataAccessLayer.Services
{
    public class WalletServices
    {
        private readonly WalletAppContext _walletAppContext;
        public WalletServices(WalletAppContext walletAppContext)
        {
            _walletAppContext = walletAppContext;   
        }

        public decimal ViewBalance(string emailId)
        {
            decimal amount = -1;
            try
            {
                var addedMoney = (from u in _walletAppContext.UserTransaction
                                  where u.EmailId == emailId && u.PaymentTypeId == 3
                                  select u.Amount).Sum();
                var sendMoneyToWallet = (from u in _walletAppContext.UserTransaction
                                  where u.EmailId == emailId && u.PaymentTypeId == 2
                                  select u.Amount).Sum();
                var sendMoneyToBank = (from u in _walletAppContext.UserTransaction
                                       where u.EmailId == emailId && u.PaymentTypeId == 4
                                       select u.Amount).Sum();
                amount = Math.Abs(addedMoney - (sendMoneyToWallet + sendMoneyToBank));
                amount = Math.Round(amount, 2);
            }
            catch (Exception)
            {
                amount = -1;
                throw;
            }
            return amount;
        }

        public ArrayList AddMoneyUsingCard(string cardNumber, string emailId, int cvv, DateTime expiryDate, decimal amount, ref bool status)
        {
            status = false;
            string message = null;
            var arrayList = new ArrayList();
            try
            {
                if (cardNumber.ToString().Length == 16)
                {
                    if (Math.Floor(Math.Log10(cvv) + 1) == 3)
                    {
                        if (expiryDate > DateTime.Now)
                        {
                            if (amount > 0)
                            {
                                using (var walletAppContext = _walletAppContext.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        UserCard userCard = new UserCard();
                                        userCard.EmailId = emailId;
                                        userCard.CardNumber = cardNumber;
                                        userCard.BankId = 1;
                                        userCard.ExpiryDate = expiryDate;
                                        userCard.StatusId = 1;
                                        var result = (from u in _walletAppContext.UserCard
                                                      where u.EmailId == emailId
                                                      select u.UserCardMappingId);
                                        if (result == null)
                                            _walletAppContext.UserCard.Add(userCard);
                                        else
                                        {
                                            _walletAppContext.UserCard.Update(userCard);
                                            _walletAppContext.SaveChanges();
                                            walletAppContext.Commit();
                                            status = true;
                                            message = "Success";
                                            arrayList.Add(status);
                                            arrayList.Add(message);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        walletAppContext.Rollback();
                                        status = false;
                                        arrayList.Add(status);
                                        arrayList.Add(message);
                                        throw;
                                    }
                                }
                                using (var walletAppContext = _walletAppContext.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        UserTransaction userTransaction = new UserTransaction();
                                        userTransaction.EmailId = emailId;
                                        userTransaction.Amount = amount;
                                        userTransaction.PaymentTypeId = 3;
                                        userTransaction.Info = "Money Added to Wallet using Card.";
                                        userTransaction.StatusId = 1;
                                        userTransaction.IsRedeemed = false;
                                        var result = (from u in _walletAppContext.UserTransaction
                                                      where u.EmailId == emailId
                                                      select u.UserTransactionId);
                                        if (result == null)
                                            _walletAppContext.UserTransaction.Add(userTransaction);
                                        else
                                        {
                                            _walletAppContext.UserTransaction.Update(userTransaction);
                                            _walletAppContext.SaveChanges();
                                            walletAppContext.Commit();
                                            status = true;
                                            message = "Success.";
                                            arrayList.Add(status);
                                            arrayList.Add(message);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        walletAppContext.Rollback();
                                        status = false;
                                        arrayList.Add(status);
                                        arrayList.Add(message);
                                        throw;
                                    }
                                }
                            }
                            else
                            {
                                status = false;
                                message = "Amount should be greater than 0.";
                                arrayList.Add(status);
                                arrayList.Add(message);
                            }

                        }
                        else
                        {
                            status = false;
                            message = "Incorrect Expiry Date.";
                            arrayList.Add(status);
                            arrayList.Add(message);
                        }
                    }
                    else
                    {
                        status = false;
                        message = "Incorrect CVV.";
                        arrayList.Add(status);
                        arrayList.Add(message);
                    }
                }
                else
                {
                    status = false;
                    message = "Incorrect Card Number.";
                    arrayList.Add(status);
                    arrayList.Add(message);
                }
            }
            catch (Exception)
            {
                status = false;
                message = "Invalid Credentials.";
                arrayList.Add(status);
                arrayList.Add(message);
                throw;
            }
            return arrayList;
        }

        public ArrayList AddMoneyUsingBank(string emailId, string password, decimal amount, ref bool status)
        {
            status = false;
            string message = null;
            var arrayList = new ArrayList();
            try
            {
                var userEmailId = _walletAppContext.User.Where(u => u.EmailId == emailId && u.Password == password).FirstOrDefault();
                if (userEmailId != null)
                {
                    if(amount > 0)
                    {
                        using (var walletAppContext = _walletAppContext.Database.BeginTransaction())
                        {
                            try
                            {
                                UserTransaction userTransaction = new UserTransaction();
                                userTransaction.EmailId = emailId;
                                userTransaction.Amount = amount;
                                userTransaction.PaymentTypeId = 3;
                                userTransaction.Info = "Money Added to Wallet using NetBank.";
                                userTransaction.StatusId = 1;
                                userTransaction.IsRedeemed = false;
                                var result = (from u in _walletAppContext.UserTransaction
                                              where u.EmailId == emailId
                                              select u.UserTransactionId);
                                if (result == null)
                                    _walletAppContext.UserTransaction.Add(userTransaction);
                                else
                                {
                                    _walletAppContext.UserTransaction.Update(userTransaction);
                                    _walletAppContext.SaveChanges();
                                    walletAppContext.Commit();
                                    status = true;
                                    message = "Success";
                                    arrayList.Add(status);
                                    arrayList.Add(message);
                                }
                            }
                            catch (Exception)
                            {
                                walletAppContext.Rollback();
                                status = false;
                                arrayList.Add(status);
                                arrayList.Add(message);
                                throw;
                            }
                        }
                    }
                    else
                    {
                        status = false;
                        message = "Amount should be greater than 0.";
                        arrayList.Add(status);
                        arrayList.Add(message);
                    }
                }
                else
                {
                    status = false;
                    message = "Invalid Credentials";
                    arrayList.Add(status);
                    arrayList.Add(message);
                }

            }
            catch (Exception)
            {
                status = false;
                message = "Invalid Credentials.";
                arrayList.Add(status);
                arrayList.Add(message);
                throw;
            }

            return arrayList;
        }


        public ArrayList TransferToWallet(string upi, decimal amount, string remarks, string emailId)
        {
            bool status = false;
            string message = null;
            var arrayList = new ArrayList();
            string upiRegex = @"^[\w.-]+@[\w.-]+$";

            try
            {
                if(!string.IsNullOrEmpty(upi) && Regex.IsMatch(upi, upiRegex))
                {
                    if(amount > 0)
                    {
                        if(amount < ViewBalance(emailId))
                        {
                            using (var walletAppContext = _walletAppContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    UserTransaction userTransaction = new UserTransaction();
                                    userTransaction.EmailId = emailId;
                                    userTransaction.Amount = amount;
                                    userTransaction.PaymentTypeId = 2;
                                    userTransaction.Remarks = remarks;
                                    userTransaction.Info = "Money transferred to " + upi;
                                    userTransaction.StatusId = 1;
                                    userTransaction.IsRedeemed = false;
                                    var result = (from u in _walletAppContext.UserTransaction
                                                  where u.EmailId == emailId
                                                  select u.UserTransactionId);
                                    if (result == null)
                                        _walletAppContext.UserTransaction.Add(userTransaction);
                                    else
                                    {
                                        _walletAppContext.UserTransaction.Update(userTransaction);
                                        _walletAppContext.SaveChanges();
                                        walletAppContext.Commit();
                                        status = true;
                                        message = "Success";
                                        arrayList.Add(status);
                                        arrayList.Add(message);
                                    }
                                }
                                catch (Exception)
                                {
                                    walletAppContext.Rollback();
                                    status = false;
                                    arrayList.Add(status);
                                    arrayList.Add(message);
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            status = false;
                            message = "Insufficient Wallet Money.";
                            arrayList.Add(status);
                            arrayList.Add(message);
                        }
                    }
                    else
                    {
                        status = false;
                        message = "Amount should be greater than 0.";
                        arrayList.Add(status);
                        arrayList.Add(message);
                    }
                }
                else
                {
                    status = false;
                    message = "Invalid Credentials.";
                    arrayList.Add(status);
                    arrayList.Add(message);
                }
            }
            catch (Exception)
            {
                status = false;
                message = "Invalid Credentials.";
                arrayList.Add(status);
                arrayList.Add(message);
                throw;
            }

            return arrayList;
        }


        public ArrayList TransferToBank(string accountNo, string accountName, string ifsc, decimal amount, string emailId)
        {
            bool status = false;
            string message = null;
            var arrayList = new ArrayList();
            string accountNoRegex = @"^\d{9,18}$";
            string ifscRegex = @"^[A-Za-z]{4}[a-zA-Z0-9]{7}$";

            try
            {
                if (!string.IsNullOrEmpty(accountNo) && Regex.IsMatch(accountNo, accountNoRegex))
                {
                    if(!string.IsNullOrEmpty(accountName))
                    {
                        if(!string.IsNullOrEmpty(ifsc) && Regex.IsMatch(ifsc, ifscRegex))
                        {
                            if (amount > 0)
                            {
                                if(amount < ViewBalance(emailId)) 
                                {
                                    using (var walletAppContext = _walletAppContext.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            UserTransaction userTransaction = new UserTransaction();
                                            userTransaction.EmailId = emailId;
                                            userTransaction.Amount = amount;
                                            userTransaction.PaymentTypeId = 4;
                                            userTransaction.Remarks = "A/C No: " + accountNo + " IFSC: " + ifsc;
                                            userTransaction.Info = "Money transferred to " + accountName;
                                            userTransaction.StatusId = 1;
                                            userTransaction.IsRedeemed = false;
                                            var result = (from u in _walletAppContext.UserTransaction
                                                          where u.EmailId == emailId
                                                          select u.UserTransactionId);
                                            if (result == null)
                                                _walletAppContext.UserTransaction.Add(userTransaction);
                                            else
                                            {
                                                _walletAppContext.UserTransaction.Update(userTransaction);
                                                _walletAppContext.SaveChanges();
                                                walletAppContext.Commit();
                                                status = true;
                                                message = "Success";
                                                arrayList.Add(status);
                                                arrayList.Add(message);
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            walletAppContext.Rollback();
                                            status = false;
                                            arrayList.Add(status);
                                            arrayList.Add(message);
                                            throw;
                                        }
                                    }
                                }
                                else
                                {
                                    status = false;
                                    message = "Insufficient Wallet Money.";
                                    arrayList.Add(status);
                                    arrayList.Add(message);
                                }
                            }
                            else
                            {
                                status = false;
                                message = "Amount should be greater than 0.";
                                arrayList.Add(status);
                                arrayList.Add(message);
                            }
                        }
                        else
                        {
                            status = false;
                            message = "Invalid IFSC Code.";
                            arrayList.Add(status);
                            arrayList.Add(message);
                        }
                    }
                    else
                    {
                        status = false;
                        message = "Incorrect Account Name.";
                        arrayList.Add(status);
                        arrayList.Add(message);
                    }
                }
                else
                {
                    status = false;
                    message = "Incorrect Account Number.";
                    arrayList.Add(status);
                    arrayList.Add(message);
                }
            }
            catch (Exception)
            {
                status = false;
                message = "Invalid Credentials";
                arrayList.Add(status);
                arrayList.Add(message);
                throw;
            }

            return arrayList;
        }
    }
}
