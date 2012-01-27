using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class UserStats
    {
        #region Properties

        [DataMember(Name = Methods.Params.WatchedHours)]
        public double WatchedHours { get; set; }

        [DataMember(Name = Methods.Params.RemainingHours)]
        public double RemainingHours { get; set; }

        [DataMember(Name = Methods.Params.WatchedEpisodes)]
        public int WatchedEpisodes { get; set; }

        [DataMember(Name = Methods.Params.RemainingEpisodes)]
        public int RemainingEpisodes { get; set; }

        [DataMember(Name = Methods.Params.WatchedDays)]
        public double WatchedDays { get; set; }

        [DataMember(Name = Methods.Params.RemainingDays)]
        public double RemainingDays { get; set; }

        #endregion

    }
}
