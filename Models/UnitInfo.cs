using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Aktan.Entities;

namespace Aktan.Models
{
    public class UnitInfo
    {
        public DateTime Start{get;set;}
        public DateTime End{get;set;}
        public Customer _Customer{get;set;}
        public double Sum{get;set;}
    }
}