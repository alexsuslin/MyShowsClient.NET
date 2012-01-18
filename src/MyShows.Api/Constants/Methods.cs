namespace MyShows.Api.Constants
{
    internal struct Methods
    {
        public static string ListOfShows = "/profile/shows/";
        public const string Auth = "profile/login";

        public struct Params
        {
            public const string Image = "image";
            public const string Rating = "rating";
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
        }
    }
}
