using System.Runtime.Serialization;

namespace MyShows.Api.Constants
{
    public enum Rating
    {
        [EnumMember(Value = "0")]
        None = 0,
        [EnumMember(Value = "1")]
        Poor = 1,
        [EnumMember(Value = "2")]
        Fair = 2,
        [EnumMember(Value = "3")]
        Average = 3,
        [EnumMember(Value = "4")]
        Good = 4,
        [EnumMember(Value = "5")]
        Excellent = 5
    }
}
