﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aktan.Models;
using Aktan.Entities;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Lobby()
        {
            List<UnitViewModel> list = 
            _context.Units.Select(u => 
                new UnitViewModel 
                    { 
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
