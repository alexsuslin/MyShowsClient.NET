using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Show
    {
        #region Properties

        [DataMember(Name = Methods.Params.ShowStatus)]
        protected internal string StatusParameter { get; set; }

        [DataMember(Name = Methods.Params.Rating)]
        protected internal int RatingParameter { get; set; }

        [DataMember(Name = Methods.Params.WatchStatus)]
        public string WatchStatusParameter { get; set; }

        [DataMember(Name = Methods.Params.ShowId)]
        public int ShowId { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.RuTitle)]
        public string RuTitle { get; set; }

        [DataMember(Name = Methods.Params.Runtime)]
        public int Runtime { get; set; }

        [DataMember(Name = Methods.Params.WatchedEpisodes)]
        public int WatchedEpisodes { get; set; }

        [DataMember(Name = Methods.Params.TotalEpisodes)]
        public int TotalEpisodes { get; set; }

        [DataMember(Name = Methods.Params.Image)]
        public string ImageUrl { get; set; }

        public ShowStatus Status
        {
            get
            {
                switch (StatusParameter)
                {
                    case Methods.Params.CanceledOrEnded:
                        return ShowStatus.CanceledOrEnded;
                    case Methods.Params.FinalSeason:
                        return ShowStatus.FinalSeason;
                    case Methods.Params.NewSeries:
                        return ShowStatus.NewSeries;
                    case Methods.Params.ReturiningSeries:
                        return ShowStatus.ReturiningSeries;
                    case Methods.Params.TBD:
                        return ShowStatus.TBD;
                    default:
                        return ShowStatus.Unidentified;

                }
            }
        }

        public WatchStatus WatchStatus
        {
            get
            {
                switch (WatchStatusParameter)
                {
                    case Methods.Params.Watching:
                        return WatchStatus.Watching;
                    case Methods.Params.Later:
                        return WatchStatus.Later;
                    case Methods.Params.Cancelled:
                        return WatchStatus.Cancelled;
                    case Methods.Params.Finished:
                        return WatchStatus.Finished;
                    default:
                        return WatchStatus.None;

                }
            }
        }

        public Rating Rating
        {
            get { return (Rating) RatingParameter; }
        }

        #endregion

        #region Helper Methods

        static internal protected string WatchStatusToString(WatchStatus status)
        {
            switch (status)
            {
                case WatchStatus.Watching:
                    return Methods.Params.Watching;
                case WatchStatus.Later:
                    return Methods.Params.Later;
                case WatchStatus.Cancelled:
                    return Methods.Params.Cancelled;
                case WatchStatus.Finished:
                    return Methods.Params.Finished;
                default:
                    return string.Empty;

            }
        }

        #endregion
    }
}
