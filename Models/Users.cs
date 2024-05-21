namespace OrangeLand.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; } 

        public ICollection<Reservations> Reservations { get; set; }
    }
}




