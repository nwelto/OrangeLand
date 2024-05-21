namespace OrangeLand.Models
{
    public class RVSites
    {
        public int Id { get; set; }
        public string SiteNumber { get; set; }
        public bool HasGrassyArea { get; set; }
        public bool IsPullThrough { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
    }
}


