using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aktan.Models;

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
            _context.Customers.Add(new Entities.Customer{CustomerName = "Sanzhar", DiscountForCustomer=0});
            var list = _context.Units.ToList();
            System.Console.WriteLine(list.Count);
            _context.SaveChanges();
            return View();
        }
        public IActionResult Lobby()
        {
            return View();
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
