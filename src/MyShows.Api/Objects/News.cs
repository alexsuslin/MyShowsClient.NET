using System.Runtime.Serialization;
using MyShows.Api.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class News
    {
        #region Properties

        [DataMember(Name = Methods.Params.EpisodeId)]
        public int? EpisodeId { get; set; }

        [DataMember(Name = Methods.Params.ShowId)]
        public int ShowId { get; set; }

        [DataMember(Name = Methods.Params.Show)]
        public string ShowTitle { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string EpisodeTitle { get; set; }

        [DataMember(Name = Methods.Params.Login)]
        public string Username { get; set; }

        [DataMember(Name = Methods.Params.Episodes)]
        public int TotalCheckedEpisodes { get; set; }

        //todo I can add Series N and Episode N instead
        [DataMember(Name = Methods.Params.Episode)]
        public string Episode { get; set; }
        
        //todo have a look if that can be different than 'watch'
        [DataMember(Name = Methods.Params.Action)]
        public string Action { get; set; }

        [DataMember(Name = Methods.Params.Gender), JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        #endregion
    }
}
