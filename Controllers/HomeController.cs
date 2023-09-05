using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aktan.Models;
using Aktan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Threading;


namespace Aktan.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        public HomeController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IActionResult Index()
        {
            using (var db = new AppDbContext())
            {
                if (db.Customers.Count() == 0)
                {
                    var c = new Customer
                    {
                        CustomerName = "Гость",
                        DiscountForCustomer = 0
                    };
                    var c1 = new Customer
                    {
                        CustomerName = "Vip",
                        DiscountForCustomer = 20
                    }; var c2 = new Customer
                    {
                        CustomerName = "Vip Plus",
                        DiscountForCustomer = 40
                    };
                    var c3 = new Customer
                    {
                        CustomerName = "Super VIP",
                        DiscountForCustomer = 60
                    };
                    db.Customers.Add(c);
                    db.Customers.Add(c1);
                    db.Customers.Add(c2);
                    db.Customers.Add(c3);


                }
                if (db.Units.Count() == 0)
                {
                    var unit1 = new Unit
                    {
                        UnitNumber = 1,
                        isActive = false,
                        CurrentEndTime = DateTime.Now
                    };
                    var unit2 = new Unit
                    {
                        UnitNumber = 2,
                        isActive = false,
                        CurrentEndTime = DateTime.Now
                    };
                    var unit3 = new Unit
                    {
                        UnitNumber = 3,
                        isActive = false,
                        CurrentEndTime = DateTime.Now
                    };
                    var unit4 = new Unit
                    {
                        UnitNumber = 4,
                        isActive = false,
                        CurrentEndTime = DateTime.Now
                    };
                    var unit5 = new Unit
                    {
                        UnitNumber = 5,
                        isActive = false,
                        CurrentEndTime = DateTime.Now
                    };
                    var unit6 = new Unit
                    {
                        UnitNumber = 6,
                        isActive = false,
                        CurrentEndTime = DateTime.Now
                    };

                    db.Units.Add(unit1);
                    db.Units.Add(unit2);
                    db.Units.Add(unit3);
                    db.Units.Add(unit4);
                    db.Units.Add(unit5);
                    db.Units.Add(unit6);
                    db.SaveChanges();
                }
            }
            return View();
        }
        public IActionResult Calculate(double input, int role)
        {
            var res = UnitController.__Calculate(input, role);
            string result = res.ToString("N2");
            return PartialView("Calc", result);
        }


        [HttpPost]
        public IActionResult MakeOrder(int unit, int Sum, int customer)
        {
            var unitController = new UnitController(unit, customer, Sum);
            unitController.Activate();


            #region 
            ViewData["Customers"] = _context.Customers.ToList();
            List<UnitViewModel> list =
            _context.Units.Select(u =>
                new UnitViewModel
                {
                    UnitId = u.UnitId,
                    UnitNumber = u.UnitNumber,
                    isActive = u.isActive,
                    URL = u.isActive ? "~/icons/Play.png" : "~/icons/Ready.png"
                }).ToList();
            return RedirectToAction("Lobby", list);
            #endregion
        }
        [HttpPost]
        public IActionResult Stop(int unitId)
        {
            using (var db = new AppDbContext())
            {
                var unit = db.Units.FirstOrDefault(u => u.UnitId == unitId);
                unit.isActive = false;
                db.Units.Update(unit);
                db.SaveChanges();
                ViewData["EndTime"] = unit.CurrentEndTime;
                ViewData["LastSum"] = unit.LastSum;
            }
            var r = new Report();
            System.Console.WriteLine("I am here");
            return View(r);
        }

        public IActionResult Lobby()
        {
            ViewData["Customers"] = _context.Customers.ToList();
            List<UnitViewModel> list =
            _context.Units.Select(u =>
                new UnitViewModel
                {
                    UnitId = u.UnitId,
                    UnitNumber = u.UnitNumber,
                    isActive = u.isActive,
                    URL = u.isActive ? "~/icons/Play.png" : "~/icons/Ready.png",
                    EndTime = u.CurrentEndTime,
                }).ToList();
            return View(list);
        }

        public IActionResult Log()
        {
            List<LogsInfo> logs;
            using (var db = new AppDbContext())
            {
                logs = db.Logs.Select(l => new LogsInfo
                {
                    UnitName = l.Unit.UnitNumber,
                    CustomerName = l.Customer.CustomerName,
                    DiscountForCustomer = l.Customer.DiscountForCustomer,
                    Start = l.Start.ToShortDateString(),
                    End = l.End.ToShortTimeString(),
                    Sum = l.Sum,
                    Minutes = l.Minutes
                }).ToList();

            }
            return View(logs);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}