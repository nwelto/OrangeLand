using System.Collections.Generic;

namespace OrangeLand.Models
{
    public class Reservations
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SiteId { get; set; }
        public int GuestId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfDogs { get; set; }
        public string Status { get; set; }

        public Users User { get; set; }
        public RVSites Site { get; set; }
        public Guests Guest { get; set; }
        public ICollection<BikeRentals> BikeRentals { get; set; }
    }
}




