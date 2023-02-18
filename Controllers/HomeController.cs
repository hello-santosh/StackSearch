using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackSearch.Models;

namespace StackSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StackOverflowDbContext _context;


        public HomeController(ILogger<HomeController> logger, StackOverflowDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> showSearchResult(String searchPhrase)
        {
            return View("showSearchResult", await _context.Posts.Where ( j => j.Title.Contains(searchPhrase)).Take(10).ToListAsync());
        }

		public async Task<IActionResult> ViewUser(String postOwnerId)
		{
			return View("ViewUser", await _context.Users.Where(j => j.Id.Equals(int.Parse(postOwnerId))).ToListAsync());
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}