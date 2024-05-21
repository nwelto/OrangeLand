namespace OrangeLand.Models
{
    public class Bikes
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal RentalFee { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<BikeRentals> BikeRentals { get; set; }
    }
}

