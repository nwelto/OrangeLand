namespace OrangeLand.Models
{
    public class BikeRentals
    {
        public int ReservationId { get; set; }
        public int BikeId { get; set; }
        public Reservations Reservation { get; set; }
        public Bikes Bike { get; set; }
    }
}





