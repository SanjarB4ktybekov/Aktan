using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktan.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Aktan.Entities
{
    public class UnitController
    {
        public int _unitId { get; set; }
        public int _customerId { get; set; }
        public float DiscountForCustomer { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Sum { get; set; }

        public UnitController(int unitId, int customerId)
        {
            _unitId = unitId;
            _customerId = customerId;
            using (var db = new AppDbContext())
            {
                var c = db.Customers.FirstOrDefault(c => c.CustomerId == _customerId);
                DiscountForCustomer = c.DiscountForCustomer;
            }
        }
        // without realization
        public void SetTime(double Minutes)
        {
            using (var db = new AppDbContext())
            {
                var unit = db.Units.FirstOrDefault(u => u.UnitId == _unitId);
                unit.isActive = true;

                var log = new Log();
                log.CustomerId = _customerId;
                log.UnitId = _unitId;
                log.Start = Start;
                log.End = End;
                log.Sum = Sum;
                db.Logs.Add(log);
                db.SaveChanges();
            }
            Start = DateTime.Now;
            End = Start.AddMinutes(Minutes);
            // make dynamic
            Sum = Minutes * 2.5;
            Sum -= Sum * (DiscountForCustomer / 100);
        }
        

        public double Calculate(double Budget)
        {
            Budget += Budget * DiscountForCustomer / 100;
            double Minutes = Budget / 2.5;
            return Minutes;
        }

        public void SetBudget(double Budget)
        {
            using (var db = new AppDbContext())
            {
                var unit = db.Units.FirstOrDefault(u => u.UnitId == _unitId);
                unit.isActive = true;
                var log = new Log();
                log.CustomerId = _customerId;
                log.UnitId = _unitId;
                log.Start = Start;
                log.End = End;
                log.Sum = Sum;
                db.Logs.Add(log);
                db.SaveChanges();
            }
            Budget += Budget * DiscountForCustomer / 100;
            double Minutes = Budget / 2.5;
            Start = DateTime.Now;
            End = Start.AddMinutes(Minutes);
        }
        public void Check()
        {
            if (End == DateTime.Now || End < DateTime.Now)
            {
                using (var db = new AppDbContext())
                {
                    var un = db.Units.FirstOrDefault(u => u.UnitId == _unitId);
                    un.isActive = false;
                    db.SaveChanges();
                }
            }
        }
        public UnitInfo GetInfo()
        {
            return new UnitInfo
            {
                CustomerId = _customerId,
                Start = Start,
                End = End,
                Sum = Sum
            };
        }

    }
}