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
			return View((_context.Kategorie.Where(x => x.ParentKategorieId == null).ToList(), _context.Vyrobek.ToList()));
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
				var produkty = ZiskejProdukty(kategorie);
				return View((kategorie, produkty));
			}
			return View();
		}

		public List<Vyrobek> ZiskejProdukty(Kategorie kategorie)
		{
			var list = new List<Vyrobek>();
			list.AddRange(_context.Vyrobek.Where(x => x.KategorieId == kategorie.KategorieId).ToList());
			var updatedKategorie = _context.Kategorie.Where(x => x.KategorieId == kategorie.KategorieId).Include(x => x.ChildKategorie).ToList()[0];
			foreach (var childKategorie in updatedKategorie.ChildKategorie)
            {
				list.AddRange(ZiskejProdukty(childKategorie));
            }
            return list;
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}