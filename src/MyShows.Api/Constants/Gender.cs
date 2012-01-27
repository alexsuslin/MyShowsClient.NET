using System.Runtime.Serialization;

namespace MyShows.Api.Constants
{
    public enum Gender
    {
        [EnumMember(Value = "m")]
        Male,
        [EnumMember(Value = "f")]
        Female,
        [EnumMember(Value = null)]
        None
    }
}
