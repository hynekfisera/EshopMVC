using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EshopMVC.Models;

namespace EshopMVC.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Kategorie> Kategorie { get; set; }
		public DbSet<Vyrobek> Vyrobek { get; set; }
		public DbSet<EshopMVC.Models.Sleva> Sleva { get; set; } = default!;
	}
}