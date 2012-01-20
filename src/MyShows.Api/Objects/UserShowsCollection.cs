using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    public class UserShowsCollection: List<UserShow>
    {
        #region Constructor

        internal UserShowsCollection(Dictionary<int, UserShow> dic)
        {
            if (dic != null)
                AddRange(Helper.ConvertToList(dic));
        }

        #endregion
    }
}
