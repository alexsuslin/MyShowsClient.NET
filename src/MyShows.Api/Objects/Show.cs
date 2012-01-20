using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class Show : UserShow
    {
        private EpisodesCollection episodes;

        [DataMember(Name = Methods.Params.Id)]
        protected internal int ID
        {
            get { return ShowId; }
            set { ShowId = value; }
        }

        [DataMember(Name = Methods.Params.Status)]
        protected internal string ShowStatus
        {
            get { return StatusParameter; }
            set { StatusParameter = value; }
        }

        [DataMember(Name = Methods.Params.Started)]
        protected internal string StartedParameter { get; set; }

        [DataMember(Name = Methods.Params.Ended)]
        protected internal string EndedParameter { get; set; }

        [DataMember(Name = Methods.Params.Genres)]
        protected internal int[] GenresParameter { get; set; }

        [DataMember(Name = Methods.Params.Country)]
        public string Country { get; set; }

        [DataMember(Name = Methods.Params.Year)]
        public int year { get; set; }

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
        public int place { get; set; }

        [DataMember(Name = Methods.Params.Episodes)]
        protected internal Dictionary<int, Episode> EpisodesParameter { get; set; }

        public DateTime? Started
        {
            get { return Helper.ParseDate(StartedParameter); }
        }

        public DateTime? Ended
        {
            get { return Helper.ParseDate(EndedParameter); }
        }

        public double AverageRating
        {
            get { return RatingParameter; }
        }

        public List<Genre> Genres
        {
            get
            {
                return (from index in GenresParameter
                        where GenresCollection.All.ContainsKey(index)
                        select GenresCollection.All[index])
                    .ToList();
            }
        }

        public EpisodesCollection Episodes
        {
            get { return episodes ?? (episodes = new EpisodesCollection(EpisodesParameter)); }
        }
    }
}
