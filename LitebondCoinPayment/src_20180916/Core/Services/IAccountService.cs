using System;
using System.Collections.Generic;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Services
{
    public interface IAccountService : IEntityService<Account>
    {
        Account GetByEmail(string email);

        Wallet GetActiveWallet(string userId);

        ResponseResult GetWallet(string userId);

        ResponseResult SyncWallet(string userId, Wallet activeWallet);

        ResponseResult CreateNewWallet(string userId, Wallet entity);

        ResponseResult Withdraw(Wallet activeWallet, string to, decimal amount, Transaction transaction);

        List<Account> ListReferrals(string email);

        int CheckAndUpdateLevel(string email, int currentLevel);

        int UpdateLevel(int Level, string email);

        List<Account> RecursiveOfUser(string email);

        decimal totalDepOfTotalNetworkNoUseGetNetWorkComission(string email);

        decimal totalDepOfTotalNetwork(string email, DateTime dateGetNetworkComssion);

        ResponseResult Login(string email, string password);

        ResponseResult Register(Account model);

        ResponseResult ChangePassword(string email, string oldPassword, string newPassword);

        ResponseResult ForgotPassword(string email, string actionCode);

        Account CreateActionCode(string email);

        Account GetByVerificationCode(string code);

        ResponseResult ResetPassword(string code, string email, string newPassword);

        ResponseResult ChangeAddressLitecoin(string email, string address);
    }
}