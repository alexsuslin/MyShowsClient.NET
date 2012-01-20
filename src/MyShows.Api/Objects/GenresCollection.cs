using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    using System;

    public sealed class GenresCollection
    {
        private static readonly object syncRoot = new Object();
        private static volatile Dictionary<int, Genre> all;

        private GenresCollection()
        {
            Initialize();
        }

        private static void Initialize()
        {
            all = new MyShowsClient().GetGenres().Data ?? new Dictionary<int, Genre>();
        }

        public static Dictionary<int, Genre> All
        {
            get
            {
                if (all == null)
                    lock (syncRoot)
                    {
                        if (all == null)
                            Initialize();
                    }
                return all;
            }
        }
    }
}
