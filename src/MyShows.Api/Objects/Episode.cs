using System;
using System.Runtime.Serialization;
using MyShows.Api.Constants;
using MyShows.Api.Objects.JsonUtilities;
using Newtonsoft.Json;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Episode : EpisodeId
    {
        #region Properties

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.ShowId)]
        public int ShowId { get; set; }

        [DataMember(Name = Methods.Params.SeasonNumber)]
        public int SeasonNumber { get; set; }

        [DataMember(Name = Methods.Params.EpisodeNumber)]
        public int EpisodeNumber { get; set; }

        [DataMember(Name = Methods.Params.ProductionNumber)]
        public int ProductionNumber { get; set; }

        [DataMember(Name = Methods.Params.SequenceNumber)]
        public int SequenceNumber { get; set; }

        [DataMember(Name = Methods.Params.TVRageLink)]
        public string TVRageLink { get; set; }

        [DataMember(Name = Methods.Params.Image)]
        public string ImageLink { get; set; }

        [DataMember(Name = Methods.Params.ShortName)]
        public string ShortName { get; set; }

        [DataMember(Name = Methods.Params.AirDate), JsonConverter(typeof (CustomDateTimeConverter))]
        public DateTime? AirDate { get; set; }

        #endregion

    }
}

