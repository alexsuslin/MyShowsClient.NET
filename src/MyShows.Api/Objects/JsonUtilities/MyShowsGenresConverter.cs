using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace MyShows.Api.Objects.JsonUtilities
{
    public class MyShowsGenresConverter: JsonConverter
    {
        #region Overrides of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<Genre> list = value as List<Genre>;
            if(list == null)
                writer.WriteNull();
            else
                serializer.Serialize(writer, list.Select(genre => genre.ID).ToArray());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (!Helper.IsNullableType(objectType))
                    throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));

                return null;
            }

            List<Genre> list = null;
            if (reader.TokenType == JsonToken.StartArray)
            {
                int[] array = serializer.Deserialize<int[]>(reader);
                list =
                    (from value in array
                     where GenresCollection.All.ContainsKey(value)
                     select GenresCollection.All[value]).ToList();
            }
            return list;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (List<Genre>);
        }

        #endregion
    }
}
