using Application.Common.Services;
using Application.ViewModels;
using Data.Common.Enums;
using Data.Persistence.Identity;
using Data.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(LogRepository logRepository,
            SignInManager<ApplicationUser> signInManager)
        {
            _logRepository = logRepository;
            _signInManager = signInManager;
        }

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

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            ViewBag.PageTitle = "Log In";
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

            // TODO: Add Failure Message
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index");
        }
    }
}