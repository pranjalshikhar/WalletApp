﻿using DataAccessLayer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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
                amount = (from u in _walletAppContext.UserTransaction
                          where u.EmailId == emailId
                          select u.Amount).Sum();
                amount = Math.Round(amount, 2);
            }
            catch (Exception)
            {
                amount = -1;
                throw;
            }
            return amount;
        }

        public ArrayList AddMoneyUsingCard(string cardNumber, string emailId, int cvv, DateTime expiryDate, decimal amount)
        {
            bool status = false;
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
                                        userTransaction.PaymentTypeId = 1;
                                        userTransaction.Info = "Money Added to Wallet";
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
                                message = "Amount should be greater than 0";
                                arrayList.Add(status);
                                arrayList.Add(message);
                            }

                        }
                        else
                        {
                            status = false;
                            message = "Incorrect Expiry Date";
                            arrayList.Add(status);
                            arrayList.Add(message);
                        }
                    }
                    else
                    {
                        status = false;
                        message = "Incorrect CVV";
                        arrayList.Add(status);
                        arrayList.Add(message);
                    }
                }
                else
                {
                    status = false;
                    message = "Incorrect Card Number";
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
