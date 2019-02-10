using System;
using System.Collections.Generic;
using System.Text;

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
            Token = token;
            Expires = expires;
            UserEmail = userEmail;
            RemoteIpAddress = remoteIpAddress;
        }

        public RefreshToken()
        {
        }
    }
}
