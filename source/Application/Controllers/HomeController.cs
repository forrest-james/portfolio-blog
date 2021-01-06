using Data.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly LogRepository _logRepository;

        public HomeController(LogRepository logRepository) => _logRepository = logRepository;

        public IActionResult Index()
        {
            ViewBag.Title = "My Thoughts";
            var model = _logRepository.GetAll();
            return View(model);
        }
    }
}