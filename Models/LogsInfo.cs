using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aktan.Models
{
    public class LogsInfo
    {
        public int UnitName { get; set; }
        public string CustomerName { get; set; }
        public double DiscountForCustomer { get; set; }
        public string Start { get; set; }

        public string End { get; set; }
        public double Sum { get; set; }
        public double Minutes{get;set;}
    }
}