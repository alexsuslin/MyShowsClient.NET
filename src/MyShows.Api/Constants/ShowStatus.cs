using System.Runtime.Serialization;

namespace MyShows.Api.Constants
{
    public enum ShowStatus
    {
        [EnumMember(Value = Methods.Params.CanceledOrEnded)]
        CanceledOrEnded,
        [EnumMember(Value = Methods.Params.ReturiningSeries)]
        ReturiningSeries,
        [EnumMember(Value = Methods.Params.FinalSeason)]
        FinalSeason,
        [EnumMember(Value = Methods.Params.NewSeries)]
        NewSeries,
        [EnumMember(Value = Methods.Params.TBD)]
        TBD,
        [EnumMember(Value = Methods.Params.OnHiatus)]
        OnHiatus,
        [EnumMember(Value = Methods.Params.NeverAired)]
        NeverAired,
        [EnumMember(Value = null)]
        Unidentified
    }
}
