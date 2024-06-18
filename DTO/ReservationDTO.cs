using OrangeLand.Models;

namespace OrangeLand.DTO
{
    public class ReservationDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? SiteId { get; set; }
        public int? GuestId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? NumberOfGuests { get; set; }
        public int? NumberOfDogs { get; set; }
        public ReservationStatus? Status { get; set; }

        public List<BikeRentalDTO> BikeRentals { get; set; } 
    }
}


