using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    public class WatchedEpisodesCollection : List<WatchedEpisode>
    {
        #region Constructor

        internal WatchedEpisodesCollection(Dictionary<int, WatchedEpisode> dic)
        {
            if (dic != null)
                AddRange(Helper.ConvertToList(dic));
        }

        #endregion
    }
}
