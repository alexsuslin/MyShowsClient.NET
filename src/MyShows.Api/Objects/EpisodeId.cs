using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class EpisodeId
    {
        [DataMember(Name = Methods.Params.EpisodeId)]
        public int ID { get; set; }
    }
}
