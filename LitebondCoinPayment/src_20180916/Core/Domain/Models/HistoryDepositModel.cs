using System;
using System.Collections.Generic;
using Core.Domain.Entities;

namespace Core.Domain.Models
{
    public class HistoryDepositModel
    {
        public string UserId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Amount { get; set; }
        public int TotalDay { get; set; }
        public bool Status { get; set; }
        public double PercentProfitDaily { get; set; }
        public float PercentProcessing { get; set; }
        public decimal TotaProfitDailyAmountReceived { get; set; }
    }
}