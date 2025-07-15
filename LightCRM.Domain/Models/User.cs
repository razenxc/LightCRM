namespace LightCRM.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }

        public User(Guid id, string email, string role)
        {
            Id = id;
            Email = email;
            Role = role;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
