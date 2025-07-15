namespace LightCRM.Domain.Models
{
    public class JwtRefreshToken
    {
        public Guid Id { get; private set; }
        public string RefreshToken { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime ExpireAt { get; private set; }
        public bool IsRevoked { get; private set; }

        public JwtRefreshToken(Guid id, string refreshToken, Guid userId, DateTime expireAt)
        {
            Id = id;
            RefreshToken = refreshToken;
            UserId = userId;
            ExpireAt = expireAt;
        }

        public void SetRevoked(bool isRevoked)
        {
            IsRevoked = isRevoked;
        }
    }
}
