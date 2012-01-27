using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MyShows.Api.Constants;
using MyShows.Api.Objects.JsonUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Show : UserShow
    {
        #region Properties

        [DataMember(Name = Methods.Params.Id)]
        public override int ID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }

        [DataMember(Name = Methods.Params.Country)]
        public string Country { get; set; }

        [DataMember(Name = Methods.Params.Year)]
        public int Year { get; set; }

        [DataMember(Name = Methods.Params.KinopoiskId)]
        public int? KinopoiskId { get; set; }

        [DataMember(Name = Methods.Params.TVRageID)]
        public int? TVRageID { get; set; }

        [DataMember(Name = Methods.Params.IMDBID)]
        public int? IMDBID { get; set; }

        [DataMember(Name = Methods.Params.Watching)]
        public int NumberOfWatching { get; set; }

        [DataMember(Name = Methods.Params.Voted)]
        public int NumberOfVotes { get; set; }

        [DataMember(Name = Methods.Params.Place)]
        public int Place { get; set; }

        [DataMember(Name = Methods.Params.Started), JsonConverter(typeof (CustomDateTimeConverter))]
        public DateTime? Started { get; set; }

        [DataMember(Name = Methods.Params.Ended), JsonConverter(typeof (CustomDateTimeConverter))]
        public DateTime? Ended { get; set; }

        [DataMember(Name = Methods.Params.Episodes), JsonConverter(typeof (ObjectsArrayConverter<Episode>))]
        public List<Episode> Episodes { get; set; }

        [DataMember(Name = Methods.Params.Status), JsonConverter(typeof (StringEnumConverter))]
        public ShowStatus ShowStatus { get; set; }

        [DataMember(Name = Methods.Params.Genres), JsonConverter(typeof (MyShowsGenresConverter))]
        public List<Genre> Genres { get; set; }

        #endregion
    }
}
