using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyShows.Api.Objects.JsonUtilities
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        private static readonly string[] formats = { "dd.MM.yyyy", "MMM/yyyy", "yyyy", "MMM/dd/yyyy" };

        private string[] _dateTimeFormat;

        public new string[] DateTimeFormat
        {
            get { return _dateTimeFormat ?? formats; }
            set { _dateTimeFormat = value; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool nullable = Helper.IsNullableType(objectType);
            Type t = (nullable)
              ? Nullable.GetUnderlyingType(objectType)
              : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                    throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));

                return null;
            }

            if (reader.TokenType != JsonToken.String)
                throw new Exception("Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));

            string dateText = reader.Value.ToString();

            if (string.IsNullOrEmpty(dateText) && nullable)
                return null;

#if !PocketPC && !NET20
            if (t == typeof(DateTimeOffset))
            {
                if (DateTimeFormat != null)
                    return DateTimeOffset.ParseExact(dateText, DateTimeFormat, Culture, DateTimeStyles);
                return DateTimeOffset.Parse(dateText, Culture, DateTimeStyles);
            }
#endif


            if (DateTimeFormat != null)
                return DateTime.ParseExact(dateText, DateTimeFormat, Culture, DateTimeStyles);
            return DateTime.Parse(dateText, Culture, DateTimeStyles);
        }
    }
}
