using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Followers.Requests
{
    public class AddFollowerRequest
    {
        public Guid CustomerToFollow { get; set; }

        public Guid CustomerToBeFollowed { get; set; }
    }
}
