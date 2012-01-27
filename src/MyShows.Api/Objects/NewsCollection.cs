﻿using System;
using System.Collections.Generic;
﻿using System.Globalization;

namespace MyShows.Api.Objects
{
    public class NewsCollection : Dictionary<DateTime, List<News>>
    {
        #region Fields

        private static readonly string[] formats = {"dd.MM.yyyy", "MMM/yyyy", "yyyy", "MMM/dd/yyyy"};

        #endregion

        #region Constructor

        internal NewsCollection(Dictionary<string, List<News>> dic)
        {
            if (dic != null)
                foreach (KeyValuePair<string, List<News>> pair in dic)
                {
                    DateTime? date = ParseDate(pair.Key);
                    if (date != null && !ContainsKey(date.Value))
                        Add(date.Value, pair.Value);
                }
        }

        #endregion

        #region Helper Methods

        private static DateTime? ParseDate(string stringdate)
        {
            DateTime date;
            if (!DateTime.TryParseExact(stringdate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return null;
            return date;
        }

        #endregion

    }
}