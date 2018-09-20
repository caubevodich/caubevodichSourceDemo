using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Domain.Entities;
using Core.Domain.Models;
using Core.Helper;

namespace Core.Services
{
    public class AccountService : EntityService<Account>, IAccountService
    {
        private readonly IBalanceService _balanceService;
        private readonly ITransactionService _transactionService;
        private readonly IWalletService _walletService;
        private readonly ICoditionService _coditionService;
        private readonly IHistoryDepositService _historyDepositService;

        public AccountService(IDbContext context, ITransactionService transactionService,
            IWalletService walletService,
            ICoditionService coditionService,
            IHistoryDepositService historyDepositService,
            IBalanceService balanceService
            ) : base(context)
        {
            _balanceService = balanceService;
            _transactionService = transactionService;
            _walletService = walletService;
            _coditionService = coditionService;
            _historyDepositService = historyDepositService;
        }

        public Account GetByEmail(string email)
        {
            var account = Table.FirstOrDefault(x => x.Email == email);

            return account;
        }

        public Wallet GetActiveWallet(string userId)
        {
            var wallet = _walletService.Find(x => x.UserId == userId).FirstOrDefault();
            if (wallet == null) wallet = new Wallet();
            return wallet;
        }

        public ResponseResult CreateNewWallet(string userId, Wallet entity)
        {
            var result = new ResponseResult
            {
                Code = 200
            };
            var wallet = _walletService.Find(x => x.UserId == userId).FirstOrDefault();
            if (wallet == null)
            {
                entity.IsDefault = true;
                entity.Name = userId;
                entity.UserId = userId;
                entity.CreatedAt = DateTime.UtcNow;
                entity.ModifiedAt = DateTime.UtcNow;
                _walletService.Insert(entity);
            }
            else
            {
                _walletService.Update(wallet);
                result.Code = 400;
                result.Errors.Add("Your wallet existed");
            }

            return result;
        }

        public ResponseResult GetWallet(string userId)
        {
            var model = new WalletModel { };
            var result = new ResponseResult
            {
                Data = model,
                Code = 200
            };
            var wallet = _walletService.Find(x => x.UserId == userId && x.IsDefault).FirstOrDefault();

            if (wallet != null)
            {
                model.Address = wallet.Address;
                // model.AvailableBalance = wallet.AvailableBalance ?? 0;
                model.AvailableBalance = _balanceService.GetByEmail(userId).TotalBalance;
                model.Symbol = wallet.Symbol;
                var transactions = _transactionService.Table.Where(x => x.UserId == userId).ToList();
                model.Transactions = transactions;

                result.Data = model;
            }
            else
            {
                result.Code = 400;
            }

            return result;
        }

        public ResponseResult SyncWallet(string userId, Wallet activeWallet)
        {
            var result = new ResponseResult
            {
                Code = 200
            };

            activeWallet.ModifiedAt = DateTime.UtcNow;
            _walletService.Update(activeWallet);

            return result;
        }

        public ResponseResult Withdraw(Wallet activeWallet, string to, decimal amount, Transaction transaction)
        {
            var result = new ResponseResult
            {
                Code = 200
            };
            //
            //activeWallet.AvailableBalance -= amount;
            //activeWallet.ModifiedAt = DateTime.UtcNow;
            //_walletService.Update(activeWallet);

            //Updaate Balance Amount 
            

            //Insert Transaction
            transaction.Type = false;
            transaction.Status = "Pending";
            transaction.UserId = activeWallet.UserId;
            transaction.From = activeWallet.Address;
            transaction.To = to;
            transaction.Amount = amount;
            transaction.Network = activeWallet.Network;
            _transactionService.Insert(transaction);

            return result;
        }

        public List<Account> ListReferrals(string email)
        {
            return this.Find(x => x.ParentId == email).ToList();
        }

        public int CheckAndUpdateLevel(string email, int currentLevel)
        {
            var list = ListReferrals(email);
            if (ListReferrals(email) != null)
            {
                int stt = 0;
                foreach (var item in list)
                {
                    if (item.Level == _coditionService.Codition_Level_L10().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L10().Condition) return _coditionService.Codition_Level_L10().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L9().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L9().Condition) return _coditionService.Codition_Level_L9().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L8().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L8().Condition) return _coditionService.Codition_Level_L8().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L7().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L7().Condition) return _coditionService.Codition_Level_L7().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L6().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L6().Condition) return _coditionService.Codition_Level_L6().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L5().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L5().Condition) return _coditionService.Codition_Level_L5().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L4().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L4().Condition) return _coditionService.Codition_Level_L4().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L3().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L3().Condition) return _coditionService.Codition_Level_L3().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L2().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L2().Condition) return _coditionService.Codition_Level_L2().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L1().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L1().Condition) return _coditionService.Codition_Level_L1().Level;
                    }
                    else if (item.Level == _coditionService.Codition_Level_L().Level && _historyDepositService.ListHistoryDeposit(item.Email).Count > 0)
                    {
                        stt++;
                        if (stt == _coditionService.Codition_Level_L().Condition) return _coditionService.Codition_Level_L().Level;
                    }
                }
            }
            return currentLevel;
        }

        public int UpdateLevel(int Level, string email)
        {
            var acc = this.Find(x => x.Email == email).FirstOrDefault();
            acc.Level = Level;
            this.Update(acc);
            return 1;
        }

        public List<Account> RecursiveOfUser(string email)
        {
            var listDownline = this.All();
            var lookup = listDownline.ToLookup(x => x.ParentId);
            var res = lookup[email].SelectRecursive(x => lookup[x.Email]).ToList();
            return res;
        }

        public decimal totalDepOfTotalNetworkNoUseGetNetWorkComission(string email)
        {
            decimal total = 0;
            var listTotalNetwork = RecursiveOfUser(email);
            foreach (var item in listTotalNetwork)
            {
                total += _historyDepositService.calTotalDeposit(item.Email);
            }
            return total;
        }


        public decimal totalDepOfTotalNetwork(string email, DateTime dateGetNetworkComssion)
        {
            decimal total = 0;
            var listTotalNetwork = RecursiveOfUser(email);
            foreach (var item in listTotalNetwork)
            {
                total += _historyDepositService.calTotalDepositWithDate(item.Email, dateGetNetworkComssion);
            }
            return total;
        }








        public ResponseResult Login(string email, string password)
        {
            string hashPassword = password.EncryptSHA256();
            var account = this.Find(x => x.Email == email && x.PasswordHash == hashPassword).FirstOrDefault();
            string message = "";
            if (account == null) message = "Login failure";
            return new ResponseResult
            {
                Data = account,
                Message = message,
                Code = account == null ? -1 : 200
            };
        }

        public ResponseResult Register(Account model)
        {
            var result = new ResponseResult
            {
                Code = -1,
                Message = "Account not found"
            };
            var account = GetByEmail(model.Email);
            if (account != null)
            {
                result.Message = "Account has existed";
            }
            else
            {
                string hashPassword = model.PasswordHash.EncryptSHA256();
                model.PasswordHash = hashPassword;

                this.Insert(model);

                result.Code = 200;
            }
            return result;
        }

        public ResponseResult ChangePassword(string email, string oldPassword, string newPassword)
        {
            var result = new ResponseResult
            {
                Code = -1,
                Message = "Account not found"
            };
            var profile = GetByEmail(email);
            if (profile != null)
            {
                string hashPassword = newPassword.EncryptSHA256();
                profile.PasswordHash = hashPassword;

                Update(profile);
                result.Message = "Password changed";
                result.Code = 200;
            }

            return result;
        }

        public ResponseResult ForgotPassword(string email, string actionCode)
        {
            throw new NotImplementedException();
        }

        public Account CreateActionCode(string email)
        {
            var profile = GetByEmail(email);
            if (profile != null)
            {
                string dateTimeString = DateTime.UtcNow.ToString();
                var code = CryptographyHelper.EncryptMD5Enhance(email + dateTimeString);

                profile.VerificationCode = code;
                profile.VerificationTimeToLive = DateTime.Now.AddDays(2);
                Update(profile);

                return profile;
            }
            return null;
        }

        public Account GetByVerificationCode(string code)
        {
            var account = Table.FirstOrDefault(x => x.VerificationCode.Equals(code) && x.VerificationTimeToLive > DateTime.Now);

            return account;
        }

        public ResponseResult ResetPassword(string code, string email, string newPassword)
        {
            string msg = "";
            var result = new ResponseResult
            {
                Code = -1,
                Message = msg
            };
            var account = GetByEmail(email);

            if (account == null)
                msg = "User not found";
            if (account != null)
            {
                code = code.ToLower();
                if (!account.VerificationCode.ToLower().Equals(code))
                    msg = "Acction code error";
                else
                {
                    string passwordNew = newPassword.EncryptSHA256();

                    account.PasswordHash = passwordNew;
                    account.VerificationCode = null;
                    account.VerificationTimeToLive = null;
                    this.Update(account);
                    result.Code = 200;
                }
                return result;
            }

            return result;
        }

        public ResponseResult ChangeAddressLitecoin(string email, string address)
        {
            var result = new ResponseResult
            {
                Code = -1,
                Message = "Account not found"
            };
            var profile = GetByEmail(email);
            if (profile != null)
            {             
                profile.WalletAddress = address;

                Update(profile);
                result.Message = "Address LTC changed";
                result.Code = 200;
            }

            return result;
        }
    }
}