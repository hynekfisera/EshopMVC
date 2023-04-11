namespace EshopMVC.Models
{
    public class Vyrobek
    {
        public Guid VyrobekId { get; set; }
        public string? Nazev { get; set; }
        public string? Popis { get; set; }
        public string? Obrazek { get; set; }
        public double Cena { get; set; }
        public Guid? KategorieId { get; set; }
        public Kategorie? Kategorie { get; set; }
    }
}
