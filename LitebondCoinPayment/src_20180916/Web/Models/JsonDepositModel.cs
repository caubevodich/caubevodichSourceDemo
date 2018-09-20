using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class JsonDepositModel
    {
        public string txid { get; set; }
        public bool from_green_address { get; set; }
        public Int64 time { get; set; }
        public Int64 confirmations { get; set; }
        public ICollection<AmountReceivedModel> amounts_received { get; set; }
        public ICollection<string> senders { get; set; }
    }
       
    public class AmountReceivedModel
    {
        public string recipient { get; set; }
        public decimal amount { get; set; }       
    }

    public class NetworkModel
    {
        public string network { get; set; }
    }
}