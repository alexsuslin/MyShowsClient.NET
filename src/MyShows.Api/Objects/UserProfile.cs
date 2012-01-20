using System.Collections.Generic;
using System.Runtime.Serialization;
using MyShows.Api.Constants;

namespace MyShows.Api.Objects
{
    [DataContract]
    public class UserProfile
    {
        [DataMember(Name = Methods.Params.Login)]
        public string Login { get; set; }

        [DataMember(Name = Methods.Params.Avatar)]
        public string AvatarUrl { get; set; }

        [DataMember(Name = Methods.Params.WastedTime)]
        public int WastedHours { get; set; }

        [DataMember(Name = Methods.Params.Gender)]
        protected internal char GenderParameter { get; set; }

        [DataMember(Name = Methods.Params.Friends)]
        public List<UserProfile> Friends { get; set; }

        [DataMember(Name = Methods.Params.Followers)]
        public List<UserProfile> Followers { get; set; }
        
        [DataMember(Name = Methods.Params.Stats)]
        public UserStats stats { get; set; }

        public Gender Gender
        {
            get
            {
                switch (GenderParameter)
                {
                    case Methods.Params.Male:
                        return Gender.Male;
                    case Methods.Params.Female:
                        return Gender.Female;
                    default:
                        return Gender.None;
                }
            }
        }
    }
}
