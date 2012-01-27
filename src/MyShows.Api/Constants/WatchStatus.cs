using System.Runtime.Serialization;

namespace MyShows.Api.Constants
{
    public enum WatchStatus
    {
        [EnumMember(Value = Methods.Params.Watching)]
        Watching,
        [EnumMember(Value = Methods.Params.Later)]
        Later,
        [EnumMember(Value = Methods.Params.Cancelled)]
        Cancelled,
        [EnumMember(Value = Methods.Params.Finished)]
        Finished,
        [EnumMember(Value = null)]
        None
    }
}