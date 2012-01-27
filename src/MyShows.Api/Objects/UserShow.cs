using System;
using System.Runtime.Serialization;
using MyShows.Api.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class UserShow
    {
        #region Properties

        [DataMember(Name = Methods.Params.ShowId)]
        public virtual int ID { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.RuTitle)]
        public string RuTitle { get; set; }

        [DataMember(Name = Methods.Params.Runtime)]
        public int Runtime { get; set; }

        [DataMember(Name = Methods.Params.WatchedEpisodes)]
        public int WatchedEpisodes { get; set; }

        [DataMember(Name = Methods.Params.TotalEpisodes)]
        public int TotalEpisodes { get; set; }

        [DataMember(Name = Methods.Params.Image)]
        public string ImageUrl { get; set; }

        [DataMember(Name = Methods.Params.ShowStatus), JsonConverter(typeof(StringEnumConverter))]
        public ShowStatus Status { get; set; }

        [DataMember(Name = Methods.Params.WatchStatus), JsonConverter(typeof(StringEnumConverter))]
        public WatchStatus WatchStatus { get; set; }
        
        [DataMember(Name = Methods.Params.Rating)]
        public double AverageRating { get; set; }

        public virtual Rating Rating
        {
            get { return (Rating) Convert.ToInt32(Math.Ceiling(AverageRating)); }
            set { AverageRating = (int) value; }
        }

        #endregion
    }
}
