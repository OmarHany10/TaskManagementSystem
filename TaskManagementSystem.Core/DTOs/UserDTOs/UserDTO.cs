namespace TaskManagementSystem.Core.DTOs.UserDTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Experation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
        public string Message { get; set; }
    }
}
