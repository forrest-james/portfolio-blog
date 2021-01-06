using Application.Common.Services;
using Data.Common.Enums;
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

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "My Thoughts";
            var model = _logRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        [Route("/{logType}")]
        public IActionResult List(LogType logType)
        {
            ViewBag.PageTitle = logType.ToName();
            return View(_logRepository.GetByType(logType));
        }
    }
}