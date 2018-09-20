using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Services
{
    public interface INetworkComissionService : IEntityService<NetworkComission>
    {
        NetworkIncomeModel GetNetworkIncomeByEmail(string email);
        int GetTotalRef(string email); //Get Sum of Referral                                                            
        int UpdateStatusAndUpdateTotalAmountIncome(int id, bool status, decimal amount, string email);
        int UpdateStatusReferralIncomeAndUpdateTotalAmountIncome(int id, bool status, decimal extraAmount, string email);
        int UpdateStatusReceiveProfit(int id, bool status, decimal extraAmount, string email);
    }

    public class NetworkComissionService : EntityService<NetworkComission>, INetworkComissionService
    {
        private IAccountService _accountService;
        private IHistoryNetworkIncomeService _historyNetworkIncomeService;
        private IHistoryReceiveIncomeService _historyReceiveIncomeService;
        private IHistoryGetIncomeBalanceService _historyGetIncomeBalanceService;
        private IIncomeService _incomeService;
        private IProfitService _profitService;
        private IProfitDailyService _profitDailyService;

        public NetworkComissionService(IDbContext context,
            IAccountService accountService,
            IHistoryNetworkIncomeService historyNetworkIncomeService,
            IHistoryReceiveIncomeService historyReceiveIncomeService,
            IHistoryGetIncomeBalanceService historyGetIncomeBalanceService,
            IIncomeService incomeService,
            IProfitService profitService,
            IProfitDailyService profitDailyService) : base(context)
        {
            _accountService = accountService;
            _historyNetworkIncomeService = historyNetworkIncomeService;
            _historyReceiveIncomeService = historyReceiveIncomeService;
            _historyGetIncomeBalanceService = historyGetIncomeBalanceService;
            _incomeService = incomeService;
            _profitService = profitService;
            _profitDailyService = profitDailyService;

        }

        public List<HistoryNetworkIncome> ListHistoryNetworkIncome(string email)
        {
            return _historyNetworkIncomeService.Find(x => x.UserId == email).ToList();
        }
        public List<HistoryReceiveIncome> ListHistoryReceiveIncome(string email)
        {
            return _historyReceiveIncomeService.Find(x => x.UserId == email).ToList();
        }
        public List<HistoryGetIncomeBalance> ListHistoryGetIncomeBalance(string email)
        {
            return _historyGetIncomeBalanceService.Find(x => x.UserId == email).ToList();
        }
        private decimal CalculatorTotalIncomeNetwork(string email)
        {
            decimal total = 0;
            var list = new List<HistoryNetworkIncome>();
            list = ListHistoryNetworkIncome(email);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Status == true)
                    {
                        total += item.Amount;
                    }
                }
            }
            return total;
        }
        private decimal CalculatorTotalIncomeReferral(string email)
        {
            decimal total = 0;
            var list = new List<HistoryReceiveIncome>();
            list = ListHistoryReceiveIncome(email);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Status == true)
                    {
                        total += item.Amount;
                    }
                }
            }
            return total;
        }
        public int GetTotalRef(string email)
        {
            int referral = 0;
            var list = _accountService.ListReferrals(email);
            if (list != null)
            {
                referral = list.Count();
            }
            return referral;
        }
        private decimal GetTotalAmountIncome(string email)
        {
            int totalAmount = 0;
            var record = _incomeService.Find(x => x.UserId == email).FirstOrDefault();
            if (record != null) { return record.TotalAmountIncome; }
            return totalAmount;
        }

        public NetworkIncomeModel GetNetworkIncomeByEmail(string email)
        {
            var model = new NetworkIncomeModel { };
            model.TotalIncomeNetwork = CalculatorTotalIncomeNetwork(email);
            model.TotalIncomeReferral = CalculatorTotalIncomeReferral(email);
            model.TotalNetwork = GetTotalAmountIncome(email);
            model.TotalReferral = GetTotalRef(email);
            model.HistoryNetworkIncomes = ListHistoryNetworkIncome(email);
            model.HistoryGetIncomeBalances = ListHistoryGetIncomeBalance(email);
            model.HistoryReceiveIncomes = ListHistoryReceiveIncome(email);
            model.Level = _accountService.GetByEmail(email).Level;
            return model;
        }

        public int UpdateStatusAndUpdateTotalAmountIncome(int id, bool status, decimal extraAmount, string email)
        {
            _historyNetworkIncomeService.UpdateStatusInHistoryNetworkIncome(id, status);
            if (_incomeService.CheckTableIncomeInDatabase(email)) _incomeService.UpdateTotalAmountIncome(email, extraAmount);
            return 1;
        }

        public int UpdateStatusReferralIncomeAndUpdateTotalAmountIncome(int id, bool status, decimal extraAmount, string email)
        {
            _historyReceiveIncomeService.UpdateStatusInHistoryReceiveIncomeReferral(id, status);
            if (_incomeService.CheckTableIncomeInDatabase(email)) _incomeService.UpdateTotalAmountIncome(email, extraAmount);
            return 1;
        }

        public int UpdateStatusReceiveProfit(int id, bool status, decimal extraAmount, string email)
        {
            _profitDailyService.UpdateStatusInProfitDaily(id, status);
            if (_profitService.CheckTableProfitInDatabase(email)) _profitService.UpdateTotalAmountProfit(email, extraAmount);
            return 1;
        }
    }
}
