using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Show
    {
        [DataMember(Name=Methods.Params.ShowId)]
        public int ShowId { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.RuTitle)]
        public string RuTitle { get; set; }

        [DataMember(Name = Methods.Params.Runtime)]
        public int Runtime { get; set; }

        //todo
        [DataMember(Name = Methods.Params.ShowStatus)]
        public string ShowStatus { get; set; }

        //todo
        [DataMember(Name = Methods.Params.WatchStatus)]
        public string WatchStatus { get; set; }

        [DataMember(Name = Methods.Params.WatchedEpisodes)]
        public int WatchedEpisodes { get; set; }

        [DataMember(Name = Methods.Params.TotalEpisodes)]
        public int TotalEpisodes { get; set; }

        [DataMember(Name = Methods.Params.Rating)]
        public int Rating { get; set; }

        [DataMember(Name = Methods.Params.Image)]
        public string ImageUrl { get; set; }
    }
}
