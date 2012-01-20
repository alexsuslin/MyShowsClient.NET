namespace MyShows.Api.Constants
{
    public struct RateFilterParams
    {
        public const string All = "all";
        public const string Male = "male";
        public const string Female = "female";

        public static string ToString(RateFilter filter)
        {
            switch (filter)
            {
                case RateFilter.Male:
                    return Male;
                case RateFilter.Female:
                    return Female;
                case RateFilter.All:
                    return All;
                default:
                    return string.Empty;
            }
        }
    }

    public enum RateFilter
    {
        All,
        Male,
        Female,
        None
    }
}
