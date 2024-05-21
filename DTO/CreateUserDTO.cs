namespace OrangeLand.DTO
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public bool IsValidRole()
        {
            return Role == "Manager" || Role == "Employee";
        }
    }
}

