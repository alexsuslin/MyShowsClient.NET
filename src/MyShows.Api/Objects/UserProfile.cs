using System.Collections.Generic;
using System.Runtime.Serialization;
using MyShows.Api.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class UserProfile
    {
        #region Properties

        [DataMember(Name = Methods.Params.Login)]
        public string Login { get; set; }

        [DataMember(Name = Methods.Params.Avatar)]
        public string AvatarUrl { get; set; }

        [DataMember(Name = Methods.Params.WastedTime)]
        public int WastedHours { get; set; }

        [DataMember(Name = Methods.Params.Friends)]
        public List<UserProfile> Friends { get; set; }

        [DataMember(Name = Methods.Params.Followers)]
        public List<UserProfile> Followers { get; set; }

        [DataMember(Name = Methods.Params.Stats)]
        public UserStats stats { get; set; }

        [DataMember(Name = Methods.Params.Gender), JsonConverter(typeof (StringEnumConverter))]
        public Gender Gender { get; set; }

        #endregion

    }
}
