namespace LightCRM.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }

        public User(Guid id, string username, string role)
        {
            Id = id;
            Username = username;
            Role = role;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
