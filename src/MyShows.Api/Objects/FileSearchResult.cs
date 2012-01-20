using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class FileSearchResult
    {
        [DataMember(Name = Methods.Params.Match)]
        protected  internal int MatchParameter { get; set; }

        [DataMember(Name = Methods.Params.Filename)]
        public string Filename { get; set; }

        [DataMember(Name = Methods.Params.FileSize)]
        public int FileSize { get; set; }

        [DataMember(Name = Methods.Params.Show)]
        public Show Show { get; set; }

        public Match Match
        {
            get { return (Match) MatchParameter; }
        }
    }
}
