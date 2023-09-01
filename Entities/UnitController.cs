using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktan.Models;

namespace Aktan.Entities
{
    public class UnitController
    {
        public Unit _Unit { get; set; }
        public Customer _Customer { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Sum { get; set; }

        public UnitController(Unit unit, Customer customer)
        {
            _Unit = unit;
            _Customer = customer;
        }
        public void SetTime(double Minutes)
        {
            using (var db = new AppDbContext())
            {
                var unit = db.Units.FirstOrDefault(u => u.UnitId == _Unit.UnitId);
                unit.isActive = true;

                var log = new Log();
                log.CustomerId = _Customer.CustomerId;
                log.UnitId = _Unit.UnitId;
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
            Sum -= Sum * (_Customer.DiscountForCustomer / 100);
        }
        public void SetBudget(double Budget)
        {
            using (var db = new AppDbContext())
            {
                var unit = db.Units.FirstOrDefault(u => u.UnitId == _Unit.UnitId);
                unit.isActive = true;

                var log = new Log();
                log.CustomerId = _Customer.CustomerId;
                log.UnitId = _Unit.UnitId;
                log.Start = Start;
                log.End = End;
                log.Sum = Sum;
                db.Logs.Add(log);
                db.SaveChanges();
            }
            Budget += Budget * _Customer.DiscountForCustomer / 100;
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
                    var un = db.Units.FirstOrDefault(u => u.UnitId == _Unit.UnitId);
                    un.isActive = false;
                    db.SaveChanges();
                }
            }
        }
        public UnitInfo GetInfo()
        {
            return new UnitInfo
            {
                _Customer = _Customer,
                Start = Start,
                End = End,
                Sum = Sum
            };
        }

    }
}