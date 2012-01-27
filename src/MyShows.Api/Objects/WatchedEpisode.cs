using System;
using System.Runtime.Serialization;
using MyShows.Api.Constants;
using MyShows.Api.Objects.JsonUtilities;
using Newtonsoft.Json;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class WatchedEpisode: EpisodeId
    {
        #region Properties

        [DataMember(Name = Methods.Params.WatchDate), JsonConverter(typeof (CustomDateTimeConverter))]
        public DateTime? WatchDate { get; set; }

        #endregion

    }
}
