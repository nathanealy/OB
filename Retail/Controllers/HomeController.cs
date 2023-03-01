using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Retail.Areas.Identity.Data;
using Retail.Data;

namespace Retail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<RetailUser> _signInManager;
        private readonly UserManager<RetailUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, SignInManager<RetailUser> signInManager, UserManager<RetailUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

                }

                try
                {
                    var user = _userManager.Users.Where(u => u.Id == userId);
                    string ssn = user.First().SocialSecurityNumber;

                    return View();
                }
                catch
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
