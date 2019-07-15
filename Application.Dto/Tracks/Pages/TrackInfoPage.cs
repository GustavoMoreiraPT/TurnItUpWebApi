using System.Collections.Generic;

namespace Application.Dto.Tracks.Pages
{
    public class TrackInfoPage
    {
        public int Page { get; set; }

        public int TotalItems { get; set; }

        public List<TrackInfo> TracksInfo { get; set; }
    }
}
