using RestSharp;

namespace MyShows.Api.Constants
{
    internal struct Methods
    {
        public const string ListOfShows = "profile/shows/";
        public static readonly string ListOfWatchedEpisodes = string.Format("profile/shows/{{{0}}}/", Params.ShowId);
        public static readonly string MarkAsWatched = string.Format("profile/episodes/check/{{{0}}}", Params.EpisodeId);
        public static readonly string SyncMarkAsWatched = string.Format("profile/shows/{{{0}}}/sync", Params.ShowId);
        public static readonly string UnmarkAsWatched = string.Format("profile/episodes/uncheck/{{{0}}}", Params.EpisodeId);
        public static readonly string Sync = string.Format("profile/shows/{{{0}}}/episodes", Params.ShowId);
        public static readonly string SetShowStatus = string.Format("profile/shows/{{{0}}}/{{{1}}}", Params.ShowId, Params.ShowStatus);
        public static readonly string SetShowRating = string.Format("profile/shows/{{{0}}}/rate/{{{1}}}", Params.ShowId, Params.Rate);
        public static readonly string SetEpisodeRating = string.Format("profile/episodes/rate/{{{0}}}/{{{1}}}", Params.Rate, Params.EpisodeId);
        public const string GetFavoriteEpisodes = "profile/episodes/favorites/list/";
        public const string ListOfPastEpisodes = "profile/episodes/unwatched/";
        public const string ListOfNextEpisodes = "profile/episodes/next/";
        public const string Auth = "profile/login";

        public struct Params
        {
            public const string Check = "check";
            public const string Uncheck = "uncheck";
            public const string Episodes = "episodes";
            public const string WatchDate = "watchDate";
            public const string ShortName = "shortName";
            public const string TVRageLink = "tvrageLink";
            public const string Id = "id";
            public const string SequenceNumber = "sequenceNumber";
            public const string ProductionNumber = "productionNumber";
            public const string AirDate = "airDate";
            public const string EpisodeNumber = "episodeNumber";
            public const string SeasonNumber = "seasonNumber";
            public const string EpisodeId = "episodeId";
            public const string Image = "image";
            public const string Rating = "rating";
            public const string Rate = "rate";
            public const string TotalEpisodes = "totalEpisodes";
            public const string WatchedEpisodes = "watchedEpisodes";
            public const string WatchStatus = "watchStatus";
            public const string ShowStatus = "showStatus";
            public const string Runtime = "runtime";
            public const string RuTitle = "ruTitle";
            public const string Title = "title";
            public const string PhpSessionId = "PHPSESSID";
            public const string ShowId = "showId";
            public const string Username = "login";
            public const string Password = "password";

            public const string Watching = "watching";
            public const string Later = "later";
            public const string Cancelled = "cancelled";
            public const string None = "remove";
            public const string Finished = "finished";

            public const string CanceledOrEnded = "Canceled/Ended";
            public const string ReturiningSeries = "Returning Series";
            public const string FinalSeason = "Final Season";
            public const string NewSeries = "New Series";
            public const string TBD = "TBD/On The Bubble";
        }
    }
}
