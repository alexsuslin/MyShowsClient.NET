using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    public class EpisodesCollection : List<Episode>
    {
        #region Constructor

        internal EpisodesCollection(Dictionary<int, Episode> dic)
        {
            if (dic != null)
                AddRange(Helper.ConvertToList(dic));
        }

        #endregion
    }
}
