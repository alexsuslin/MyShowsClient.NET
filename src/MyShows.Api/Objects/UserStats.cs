using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class UserStats
    {
        /** @var float */

        [DataMember(Name = Methods.Params.WatchedHours)]
        public double WatchedHours { get; set; }

        /** @var float */

        [DataMember(Name = Methods.Params.RemainingHours)]
        public double RemainingHours { get; set; }

        /** @var int */

        [DataMember(Name = Methods.Params.WatchedEpisodes)]
        public int WatchedEpisodes { get; set; }

        /** @var int */

        [DataMember(Name = Methods.Params.RemainingEpisodes)]
        public int RemainingEpisodes { get; set; }

        /** @var float */

        [DataMember(Name = Methods.Params.WatchedDays)]
        public double WatchedDays { get; set; }

        /** @var float */

        [DataMember(Name = Methods.Params.RemainingDays)]
        public double RemainingDays { get; set; }
    }
}
