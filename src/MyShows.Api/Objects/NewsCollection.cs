using System;
using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    public class NewsCollection: Dictionary<DateTime, List<News>>
    {
         #region Constructor

        internal NewsCollection(Dictionary<string, List<News>> dic)
        {
            if (dic != null)
                foreach (KeyValuePair<string, List<News>> pair in dic)
                {
                    DateTime? date = Helper.ParseDate(pair.Key);
                    if (date != null && !ContainsKey(date.Value))
                        Add(date.Value, pair.Value);
                }
        }

        #endregion
    }
}
