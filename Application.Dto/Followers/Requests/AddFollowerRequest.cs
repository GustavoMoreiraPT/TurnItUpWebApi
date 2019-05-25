using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Followers.Requests
{
    public class AddFollowerRequest
    {
        public Guid Followed { get; set; }

        public Guid Follower { get; set; }
    }
}
