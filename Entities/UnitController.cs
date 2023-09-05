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

        public UnitController(int unitId, int customerId, int sum)
        {
            _unitId = unitId;
            _customerId = customerId;
            Sum = sum;
            using (var db = new AppDbContext())
            {
                var c = db.Customers.FirstOrDefault(c => c.CustomerId == _customerId);
                DiscountForCustomer = c.DiscountForCustomer;
            }
        }
        // without realization
        public double Calculate(double Budget)
        {
            Budget += Budget * DiscountForCustomer / 100;
            double Minutes = Budget / 2.5;
            return Minutes;
        }

        public static double __Calculate(double input, int userId)
        {
            System.Console.WriteLine($"{input} === {userId}");
            double result;

            using (var db = new AppDbContext())
            {
                var _customer = db.Customers.FirstOrDefault(u => u.CustomerId == userId);
                var DiscountForCustomer = _customer.DiscountForCustomer;
                input = input + (input * DiscountForCustomer / 100);
                result = input * 0.6;
            }
            return result;
        }


        public void Activate()
        {
            Sum += Sum * DiscountForCustomer / 100;
            double Minutes = Sum * 0.6;
            Start = DateTime.Now;
            End = Start.AddMinutes(Minutes);
            using (var db = new AppDbContext())
            {
                var unit = db.Units.FirstOrDefault(u => u.UnitId == _unitId);
                unit.isActive = true;
                unit.CurrentEndTime = End;
                var log = new Log();
                log.CustomerId = _customerId;
                log.UnitId = _unitId;
                log.Start = Start;
                log.End = End;
                log.Sum = Sum;
                db.Logs.Add(log);
                db.SaveChanges();
            }



        }
        public void Check()
        {
            if (End == DateTime.Now || End < DateTime.Now)
            {
                using (var db = new AppDbContext())
                {
                    var un = db.Units.FirstOrDefault(u => u.UnitId == _unitId);
                    un.isActive = false;
                    // set push up
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