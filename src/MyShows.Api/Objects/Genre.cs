using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Genre
    {
        [DataMember(Name = Methods.Params.Id)]
        public int ID { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.RuTitle)]
        public string RuTitle { get; set; }
    }
}
