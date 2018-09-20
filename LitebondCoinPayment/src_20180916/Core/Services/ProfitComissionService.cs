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
    public interface IProfitComissionService : IEntityService<ProfitComission>
    {
        ProfitIncomeModel GetProfitIncomeByEmail(string email);
    }
    public class ProfitComissionService : EntityService<ProfitComission>, IProfitComissionService
    {
        private IAccountService _accountService;
        private IProfitService _profitService;
        private IHistoryDepositService _historyDepositService;
        private IHistoryReceiveProfitService _historyReceiveProfitService;
        private IHistoryGetProfitBalanceService _historyGetProfitBalnceService;
        private IProfitDailyService _profitDailyService;
        private ICoditionService _coditionService;
        public ProfitComissionService(IDbContext context,
            IAccountService accountService,
            IProfitService profitService,
            IHistoryDepositService historyDepositService,
            IHistoryReceiveProfitService historyReceiveProfitService,
            IHistoryGetProfitBalanceService historyGetProfitBalnceService,
            IProfitDailyService profitDailyService,
            ICoditionService coditionService) : base(context)
        {
            _accountService = accountService;
            _profitService = profitService;
            _historyDepositService = historyDepositService;
            _historyReceiveProfitService = historyReceiveProfitService;
            _historyGetProfitBalnceService = historyGetProfitBalnceService;
            _profitDailyService = profitDailyService;
            _coditionService = coditionService;
        }

        public List<HistoryDepositModel> ListTotalDeposit(string email)
        {
            var model = new List<HistoryDepositModel>();
            var list = _historyDepositService.Find(x => x.UserId == email).OrderByDescending(x => x.CreatedAt).ToList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    var listProfitDaily = _profitDailyService.ListProfitDaily(email, item.Id);
                    decimal totalProfitAmountDaily = 0;
                    //Cal TotalProfitAmountDaily
                    foreach (var i in listProfitDaily)
                    {
                        if (i.Status == true)
                        {
                            totalProfitAmountDaily += i.AmountDaily;
                        }
                    }
                    if (listProfitDaily != null)
                    {
                        //cal percentProcess
                        var numberProfitDaily = listProfitDaily.Count;
                        var totalDay = item.TotalDay;
                        var percentProcess = ((float)numberProfitDaily / (float)totalDay) * 100;
                        //cal TotaProfitDailyAmountReceived
                        var totaProfitDailyAmountReceived = totalProfitAmountDaily;
                        // add to List<HistoryDepositModel>
                        var record = new HistoryDepositModel();
                        record.UserId = item.UserId;
                        record.DateCreate = item.DateCreate;
                        record.DateEnd = item.DateEnd;
                        record.Amount = item.Amount;
                        record.TotalDay = item.TotalDay;
                        record.Status = item.Status;
                        record.PercentProcessing = percentProcess;
                        record.TotaProfitDailyAmountReceived = totaProfitDailyAmountReceived;
                        record.PercentProfitDaily = item.PercentProfitDaily;
                        model.Add(record);
                    }
                }
            }
            return model;
        }
        public List<ProfitDailyModel> ListTotalReceiveProfit(string email)
        {
            // return _profitDailyService.Find(x => x.UserId == email).OrderBy(x => x.DateReceive.Date.CompareTo(x.CreatedAt)).ToList();
            var listProfitDaily = _profitDailyService.Find(x => x.UserId == email).OrderBy(x => x.DateReceive.Date.CompareTo(x.CreatedAt)).ToList();
            var model = new List<ProfitDailyModel>();
            if (listProfitDaily != null)
            {
                foreach (var item in listProfitDaily)
                {
                    var record = new ProfitDailyModel();
                    record.Id = item.Id;
                    record.UserId = item.UserId;
                    record.AmountDaily = item.AmountDaily;
                    record.DepositId = item.DepositId;
                    record.DateReceive = item.DateReceive;
                    record.Status = item.Status;
                    record.ProfitOfDeposit = _historyDepositService.Find(x => x.Id == item.DepositId).SingleOrDefault().Amount;
                    model.Add(record);
                }
            }
            return model;
        }
        public List<HistoryGetProfitBalance> ListGetProfitBalance(string email)
        {
            return _historyGetProfitBalnceService.Find(x => x.UserId == email).OrderByDescending(x => x.CreatedAt).ToList();
        }
        private decimal CalculatorTotalDeposit(string email)
        {
            decimal total = 0;
            var list = new List<HistoryDepositModel>();
            list = ListTotalDeposit(email);
            if (list != null)
            {
                foreach (var item in list)
                {
                    total += item.Amount;
                }
            }
            return total;
        }
        private decimal CalculatorTotalProfit(string email)
        {
            decimal total = 0;
            var list = new List<ProfitDailyModel>();
            list = ListTotalReceiveProfit(email);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Status == true)
                    {
                        total += item.AmountDaily;
                    }
                }
            }
            return total;
        }
        private decimal GetTotalAmountProfit(string email)
        {
            int totalAmount = 0;
            var record = _profitService.Find(x => x.UserId == email).SingleOrDefault();
            if (record != null) { return record.TotalAmountProfit; }
            return totalAmount;
        }
        public ProfitIncomeModel GetProfitIncomeByEmail(string email)
        {
            var model = new ProfitIncomeModel { };
            model.TotalDeposit = CalculatorTotalDeposit(email);
            model.TotalProfit = CalculatorTotalProfit(email);
            model.CurrentProfit = GetTotalAmountProfit(email);
            model.HistoryDepositModels = ListTotalDeposit(email);
            model.HistoryReceiveProfits = ListTotalReceiveProfit(email);
            model.HistoryGetProfitBalances = ListGetProfitBalance(email);
            model.Level = _accountService.GetByEmail(email).Level;
            return model;
        }












    }
}
