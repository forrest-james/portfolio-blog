using Application.Common.Services;
using Application.ViewModels;
using Data.Common.Enums;
using Data.Models;
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
        private readonly CurrentUserService _currentUserService;

        public HomeController(LogRepository logRepository,
            SignInManager<ApplicationUser> signInManager,
            CurrentUserService currentUserService)
        {
            _logRepository = logRepository;
            _signInManager = signInManager;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Route("/create")]
        public IActionResult Create()
        {
            ViewBag.PageTitle = "Create Post";
            return View(new Log());
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create(Log newLog)
        {
            newLog.CreatedBy = _currentUserService.UserId;
            _logRepository.Upsert(newLog);
            if(await _logRepository.SaveChangesAsync())
            {
                // TODO: Add Success Message
                return RedirectToAction("index");
            }
            else
            {
                // TODO: Add Failure Message
                return View(newLog);
            }
        }

        [HttpGet]
        [Route("/edit/{encodedTitle}")]
        public IActionResult Edit(string encodedTitle)
        {
            Log log = _logRepository.GetByTitle(encodedTitle);
            ViewBag.PageTitle = "Edit Post";
            return View(log);
        }

        [HttpPost]
        [Route("/edit")]
        [Route("/edit/{encodedTitle}")]
        public async Task<IActionResult> Edit(Log log)
        {
            log.ModifiedBy = _currentUserService.UserId;
            _logRepository.Upsert(log);
            if(await _logRepository.SaveChangesAsync())
            {
                // TODO: Add Success Message
                return RedirectToAction("index");
            }
            else
            {
                // TODO: Add Failure Message
                return View(log);
            }
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

        [HttpGet]
        [Route("/article/{encodedTitle}")]
        public IActionResult Post(string encodedTitle)
        {
            Log log = _logRepository.GetByTitle(encodedTitle);
            ViewBag.PageTitle = log.Type.ToName();
            return View(log);
        }
    }
}