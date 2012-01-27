using System.Runtime.Serialization;

namespace MyShows.Api.Constants
{
    public enum Match
    {
        [EnumMember(Value = "100")]
        Full=100,
        [EnumMember(Value = "85")]
        Partial=85
    }
}
