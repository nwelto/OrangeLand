namespace OrangeLand.Models
{
    public class Guests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RVType { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; } 

        public ICollection<Reservations> Reservations { get; set; }
    }
}



