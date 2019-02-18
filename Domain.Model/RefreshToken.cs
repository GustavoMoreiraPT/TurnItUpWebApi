using System;

namespace Domain.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; private set; }

        public DateTime Expires { get; private set; }

        public string UserEmail { get; private set; }

        public bool Active => DateTime.UtcNow <= Expires;

        public string RemoteIpAddress { get; private set; }

        public RefreshToken(string token, DateTime expires, string userEmail, string remoteIpAddress)
        {
            this.Token = token;
            this.Expires = expires;
            this.UserEmail = userEmail;
            this.RemoteIpAddress = remoteIpAddress;
        }

        public RefreshToken()
        {
        }
    }
}
