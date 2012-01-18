using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Episode
    {
        [DataMember(Name = Methods.Params.EpisodeId)]
        public int EpisodeId { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.ShowId)]
        public int ShowId { get; set; }

        [DataMember(Name = Methods.Params.SeasonNumber)]
        public int SeasonNumber { get; set; }

        [DataMember(Name = Methods.Params.EpisodeNumber)]
        public int EpisodeNumber { get; set; }

        //todo
        [DataMember(Name = Methods.Params.AirDate)]
        public object AirDate { get; set; }

        [DataMember(Name = Methods.Params.ProductionNumber)]
        public int ProductionNumber { get; set; }

        [DataMember(Name = Methods.Params.SequenceNumber)]
        public int SequenceNumber { get; set; }

        [DataMember(Name = Methods.Params.Id)]
        public int Id { get; set; }

        [DataMember(Name = Methods.Params.TVRageLink)]
        public string TVRageLink { get; set; }

        [DataMember(Name = Methods.Params.Image)]
        public string ImageLink { get; set; }

        [DataMember(Name = Methods.Params.ShortName)]
        public string ShortName { get; set; }
    }
}
