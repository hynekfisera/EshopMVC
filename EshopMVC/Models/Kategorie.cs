
using System.ComponentModel.DataAnnotations.Schema;

namespace EshopMVC.Models
{
	public class Kategorie
	{
		public Guid KategorieId { get; set; }
		public string? Nazev { get; set; }
		public Guid? ParentKategorieId { get; set; }
		public Kategorie? ParentKategorie { get; set; }
		public IEnumerable<Kategorie>? ChildKategorie { get; set; }
	}
}
