using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Aktan.Entities
{
    public class Unit
    {
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public bool isActive { get; set; }
        [AllowNull]
        public DateTime CurrentEndTime { get; set; }
        public double LastSum{get;set;}
    }
}