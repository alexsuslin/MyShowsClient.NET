using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Deserializers;

namespace MyShows.Api.Objects.JsonUtilities
{
    public class ApiJsonDeserializer : IDeserializer
    {
        public T Deserialize<T>(RestResponse response) where T : new()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
                                                  {
                                                      ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                                                      DefaultValueHandling = DefaultValueHandling.Ignore,
                                                      MissingMemberHandling = MissingMemberHandling.Ignore,
                                                      NullValueHandling = NullValueHandling.Ignore,
                                                      ObjectCreationHandling = ObjectCreationHandling.Auto,
                                                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                  };
   
            settings.Converters.Add(new ObjectsArrayConverter<UserShow>());
            settings.Converters.Add(new ObjectsArrayConverter<Show>());
            settings.Converters.Add(new ObjectsArrayConverter<WatchedEpisode>());
            settings.Converters.Add(new ObjectsArrayConverter<Episode>());
            settings.Converters.Add(new ObjectsArrayConverter<News>());

            return JsonConvert.DeserializeObject<T>(response.Content,settings);
        }

        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }
    }
}

