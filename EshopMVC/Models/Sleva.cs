namespace EshopMVC.Models
{
	public class Sleva
	{
		public Guid SlevaId { get; set; }
		public double Hodnota { get; set; }
		public bool JeProcentualni { get; set; }
		public int MinimalniPocet { get; set; }
		public DateTime DatumZacatku { get; set; }
		public DateTime DatumKonce { get; set; }
		public IEnumerable<Vyrobek>? Vyrobky { get; set; }
	}
}
