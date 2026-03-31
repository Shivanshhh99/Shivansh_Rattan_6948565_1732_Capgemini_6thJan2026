using ECommerceLoggingApi.Models;

namespace ECommerceLoggingApi.Services
{
    public class UserService
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 101, Email = "test@gmail.com", Password = "12345" },
            new User { Id = 102, Email = "admin@gmail.com", Password = "admin123" }
        };

        public User? ValidateUser(string email, string password)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool UserExists(string email)
        {
            return _users.Any(u => u.Email == email);
        }
    }
}