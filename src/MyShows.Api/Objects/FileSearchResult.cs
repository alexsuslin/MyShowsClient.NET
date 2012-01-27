using System.Runtime.Serialization;
using MyShows.Api.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class FileSearchResult
    {
        #region Properties

        [DataMember(Name = Methods.Params.Filename)]
        public string Filename { get; set; }

        [DataMember(Name = Methods.Params.FileSize)]
        public int FileSize { get; set; }

        [DataMember(Name = Methods.Params.Show)]
        public Show Show { get; set; }

        [DataMember(Name = Methods.Params.Match), JsonConverter(typeof (StringEnumConverter))]
        public Match Match { get; set; }

        #endregion
    }
}
