using EshopMVC.Data;
using EshopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EshopMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View(_context.Kategorie.Where(x => x.ParentKategorieId == null).ToList());
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Kategorie([FromRoute] string id)
		{
			var kategorie = _context.Kategorie.Include(x => x.ChildKategorie).Include(x => x.ParentKategorie).Where(x => x.KategorieId == new Guid(id)).ToList()[0];
			if (kategorie != null)
			{
				return View(kategorie);
			}
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}