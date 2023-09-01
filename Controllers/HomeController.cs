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
            return View();

        }
        public ActionResult GetMessage()
        {
        
            return PartialView("_GetMessage");
        }

        public IActionResult Calculate(int input)
        {
            return PartialView("Calc", input);
        }
        

        [HttpPost]
        public IActionResult MakeOrder(int unit, int Sum, int customer)
        {
            System.Console.WriteLine($"Unit = {unit}, Sum = {Sum}, Cus = {customer}");

            var unitController = new UnitController(unit, customer);
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
            return View("Lobby",list);
            
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
                    URL = u.isActive ? "~/icons/Play.png" : "~/icons/Ready.png"
                }).ToList();
            return View(list);
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
