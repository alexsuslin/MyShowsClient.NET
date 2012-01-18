using System;
using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class WatchedEpisode
    {
        [DataMember(Name = Methods.Params.Id)]
        public int Id { get; set; }

        [DataMember(Name = Methods.Params.WatchDate)]
        protected internal string WatchDateParameter { get; set; }

        public DateTime? WatchDate
        {
            get { return Helper.ParseDate(WatchDateParameter); }
        }
    }
}
