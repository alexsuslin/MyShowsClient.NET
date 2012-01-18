using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    public class ShowsCollection: List<Show>
    {
        #region Constructor

        internal ShowsCollection(Dictionary<int, Show> dic)
        {
            if (dic != null)
                AddRange(Helper.ConvertToList(dic));
        }

        #endregion
    }
}
