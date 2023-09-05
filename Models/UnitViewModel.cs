using System;

namespace Aktan.Models
{
    public class UnitViewModel
    {
        public int UnitId{get;set;}
        public int UnitNumber { get; set; }
        public bool isActive { get; set; }
        public string URL{get;set;}
        public DateTime EndTime { get; set; }
    }
}