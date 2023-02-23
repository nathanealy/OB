using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Retail.Areas.Identity.Data;
using Retail.Models;
using System.Diagnostics;

namespace Retail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<RetailUser> _signInManager;
        private readonly UserManager<RetailUser> _userManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<RetailUser> signInManager, UserManager<RetailUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager; 
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user =  _userManager.Users.FirstOrDefault();
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }


                

                return View();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}