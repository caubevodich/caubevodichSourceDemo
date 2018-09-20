﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class HistoryReceiveProfit : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public DateTime DateReceive { get; set; }
        public decimal AmountProfit { get; set; }
        public bool Status { get; set; }
    }
}