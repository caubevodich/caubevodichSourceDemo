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
    public interface ICoditionService : IEntityService<Codition>
    {
        Codition Codition_Level_L();
        Codition Codition_Level_L1();
        Codition Codition_Level_L2();
        Codition Codition_Level_L3();
        Codition Codition_Level_L4();
        Codition Codition_Level_L5();
        Codition Codition_Level_L6();
        Codition Codition_Level_L7();
        Codition Codition_Level_L8();
        Codition Codition_Level_L9();
        Codition Codition_Level_L10();
        bool CheckCoditionDeposit(int level, decimal amount);
        double ReturnPercentProfitDaily(int level);
        int ReturnTotaDayProfitDaily(int level);
        int GetPercentComission(string email, decimal totalDepOfNetwork);
        decimal A(string email);
        decimal B(string email);
        decimal C(string email);
        decimal D(string email);
        decimal E(string email);
        decimal F(string email);
        decimal G(string email);

    }
    public class CoditionService : EntityService<Codition>, ICoditionService
    {
        private readonly IHistoryDepositService _historyDepositService;
        private readonly IHistoryNetworkIncomeService _historyNetworkIncomeService;
        private readonly IReceiveNetworkComissionService _receiveNetworkComissionService;
        public CoditionService(IDbContext context,
            IHistoryDepositService historyDepositService,
            IHistoryNetworkIncomeService historyNetworkIncomeService,
            IReceiveNetworkComissionService receiveNetworkComissionService) : base(context)
        {
            _historyDepositService = historyDepositService;
            _historyNetworkIncomeService = historyNetworkIncomeService;
            _receiveNetworkComissionService = receiveNetworkComissionService;
        }

        public Codition Codition_Level_L()
        {
            var model = this.Find(x => x.Id == 1).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L1()
        {
            var model = this.Find(x => x.Id == 2).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L2()
        {
            var model = this.Find(x => x.Id == 3).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L3()
        {
            var model = this.Find(x => x.Id == 4).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L4()
        {
            var model = this.Find(x => x.Id == 5).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L5()
        {
            var model = this.Find(x => x.Id == 6).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L6()
        {
            var model = this.Find(x => x.Id == 7).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L7()
        {
            var model = this.Find(x => x.Id == 8).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L8()
        {
            var model = this.Find(x => x.Id == 9).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L9()
        {
            var model = this.Find(x => x.Id == 10).SingleOrDefault();
            return model;
        }
        public Codition Codition_Level_L10()
        {
            var model = this.Find(x => x.Id == 11).SingleOrDefault();
            return model;
        }
        public bool CheckCoditionDeposit(int level, decimal amount)
        {
            switch (level)
            {
                case 0:
                    if (Codition_Level_L().MinDeposit <= amount && amount <= Codition_Level_L().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (Codition_Level_L1().MinDeposit <= amount && amount <= Codition_Level_L1().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (Codition_Level_L2().MinDeposit <= amount && amount <= Codition_Level_L2().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (Codition_Level_L3().MinDeposit <= amount && amount <= Codition_Level_L3().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 4:
                    if (Codition_Level_L4().MinDeposit <= amount && amount <= Codition_Level_L4().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 5:
                    if (Codition_Level_L5().MinDeposit <= amount && amount <= Codition_Level_L5().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 6:
                    if (Codition_Level_L6().MinDeposit <= amount && amount <= Codition_Level_L6().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 7:
                    if (Codition_Level_L7().MinDeposit <= amount && amount <= Codition_Level_L7().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 8:
                    if (Codition_Level_L8().MinDeposit <= amount && amount <= Codition_Level_L8().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 9:
                    if (Codition_Level_L9().MinDeposit <= amount && amount <= Codition_Level_L9().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                case 10:
                    if (Codition_Level_L10().MinDeposit <= amount && amount <= Codition_Level_L10().MaxDeposit)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }
        public double ReturnPercentProfitDaily(int level)
        {
            switch (level)
            {
                case 0: return Codition_Level_L().PercentProfitDaily;
                case 1: return Codition_Level_L1().PercentProfitDaily;
                case 2: return Codition_Level_L2().PercentProfitDaily;
                case 3: return Codition_Level_L3().PercentProfitDaily;
                case 4: return Codition_Level_L4().PercentProfitDaily;
                case 5: return Codition_Level_L5().PercentProfitDaily;
                case 6: return Codition_Level_L6().PercentProfitDaily;
                case 7: return Codition_Level_L7().PercentProfitDaily;
                case 8: return Codition_Level_L8().PercentProfitDaily;
                case 9: return Codition_Level_L9().PercentProfitDaily;
                case 10: return Codition_Level_L10().PercentProfitDaily;
                default: return 0;
            }
        }
        public int ReturnTotaDayProfitDaily(int level)
        {
            switch (level)
            {
                case 0: return Codition_Level_L().Day;
                case 1: return Codition_Level_L1().Day;
                case 2: return Codition_Level_L2().Day;
                case 3: return Codition_Level_L3().Day;
                case 4: return Codition_Level_L4().Day;
                case 5: return Codition_Level_L5().Day;
                case 6: return Codition_Level_L6().Day;
                case 7: return Codition_Level_L7().Day;
                case 8: return Codition_Level_L8().Day;
                case 9: return Codition_Level_L9().Day;
                case 10: return Codition_Level_L10().Day;
                default: return 0;
            }
        }


        public decimal A(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 5;
        }
        public decimal B(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 10;
        }
        public decimal C(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 20;
        }
        public decimal D(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 30;
        }
        public decimal E(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 50;
        }
        public decimal F(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 70;
        }
        public decimal G(string email)
        {
            var totalDep = _historyDepositService.calTotalDeposit(email);
            return totalDep * 100;
        }
        public int GetPercentComission(string email, decimal totalDepOfNetwork)
        {
            if (totalDepOfNetwork >= G(email) && totalDepOfNetwork>0)
            {
                return 10;
            }
            else if (F(email) <= totalDepOfNetwork && totalDepOfNetwork < G(email) && totalDepOfNetwork > 0)
            {
                return 9;
            }
            else if (E(email) <= totalDepOfNetwork && totalDepOfNetwork < F(email) && totalDepOfNetwork > 0)
            {
                return 8;
            }
            else if (D(email) <= totalDepOfNetwork && totalDepOfNetwork < E(email) && totalDepOfNetwork > 0)
            {
                return 7;
            }
            else if (C(email) <= totalDepOfNetwork && totalDepOfNetwork < D(email) && totalDepOfNetwork > 0)
            {
                return 6;
            }
            else if (B(email) <= totalDepOfNetwork && totalDepOfNetwork < C(email) && totalDepOfNetwork > 0)
            {
                return 5;
            }
            else if (A(email) <= totalDepOfNetwork && totalDepOfNetwork < B(email) && totalDepOfNetwork > 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }


    }
}
