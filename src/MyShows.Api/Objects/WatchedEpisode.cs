using System;
using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class WatchedEpisode: EpisodeId
    {
        [DataMember(Name = Methods.Params.WatchDate)]
        protected internal string WatchDateParameter { get; set; }

        public DateTime? WatchDate
        {
            get { return Helper.ParseDate(WatchDateParameter); }
        }
    }
}
