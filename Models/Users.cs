namespace OrangeLand.Models
{
    public class Users
    {
        
        public int Id { get; set; }
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; } 

        public ICollection<Reservations> Reservations { get; set; }
    }
}




