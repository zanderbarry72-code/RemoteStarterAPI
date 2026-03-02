namespace RemoteStarterAPI.Models
{
    public class User
    {
        public int Id { get; set; }           // Primary key
        public string Name { get; set; } = "";  // Default empty string avoids null issues
        public string Email { get; set; } = ""; // Default empty string
    }
}