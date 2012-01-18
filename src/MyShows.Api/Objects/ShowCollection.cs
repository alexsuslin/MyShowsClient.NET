using System.Collections.Generic;

namespace MyShows.Api.Objects
{
    public class ShowCollection: List<Show>
    {
        private readonly IList<Show> list;

        internal ShowCollection(Dictionary<int, Show> dic)
        {
            list = Helper.ConvertToList(dic);
        }

        public IList<Show> Items
        {
            get { return list; }
        }
    }
}
