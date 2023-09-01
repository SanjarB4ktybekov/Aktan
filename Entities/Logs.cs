using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aktan.Entities
{
    public class Log
    {
        public int LogId{get;set;}
        public int UnitId{get;set;}
        public int CustomerId{get;set;}
        public DateTime Start{get;set;}
        public DateTime End{get;set;}
        public Double Sum{get;set;}
        //===========================
        public Unit Unit{get;set;}
        public Customer Customer{get;set;}
    }
}