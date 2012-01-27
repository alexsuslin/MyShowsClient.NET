using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using MyShows.Api.Constants;
using MyShows.Api.Objects;
using MyShows.Api.Objects.JsonUtilities;
using Newtonsoft.Json;
using RestSharp;

namespace MyShows.Api
{
    public class MyShowsClient
    {
        #region Fields & Consts

        public const string BaseUrl = "http://api.myshows.ru/";

        private readonly string ApiUrl;

        private string phpSessId;

        #endregion

        #region Properties

        public Credentials Credentials { get; set; }

        #endregion

        #region Constructors

        public MyShowsClient(Credentials credentials = null, string baseUrl = BaseUrl)
        {
            ApiUrl = baseUrl;
            Credentials = credentials;
        }

        #endregion

        #region Helper Methods

        private MyShowsResponse Execute(RestRequest request)
        {
            RestClient client = new RestClient(ApiUrl);
            request = RequestSetup(client, request);
            return client.Execute(request);
        }

        private MyShowsResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            RestClient client = new RestClient(ApiUrl);
            request = RequestSetup(client, request);
            return client.Execute<T>(request);
        }

        private RestRequest RequestSetup(RestClient client, RestRequest request)
        {
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;

            client.AddHandler("application/json", new ApiJsonDeserializer());
            client.AddHandler("text/json", new ApiJsonDeserializer());
            client.AddHandler("text/x-json", new ApiJsonDeserializer());
            //why Myshows sends text/html when it should be json?
            client.AddHandler("text/html", new ApiJsonDeserializer());
            
            if(!string.IsNullOrEmpty(phpSessId))
                request.AddCookie(Methods.Params.PhpSessionId, phpSessId);
            return request;
        }

        #endregion

        #region Api Methods

        public MyShowsResponse Auth(Credentials cred = null)
        {
            RestRequest request = new RestRequest(Methods.Auth);

            cred = cred ?? Credentials;

            if (cred != null)
            {
                request.AddParameter(Methods.Params.Username, cred.Username, ParameterType.GetOrPost);
                request.AddParameter(Methods.Params.Password, cred.Password, ParameterType.GetOrPost);
            }

            MyShowsResponse myShowsResponse = Execute(request);
            phpSessId = myShowsResponse.Response.Cookies.GetValueByName(Methods.Params.PhpSessionId);
            return Execute(request);
        }

        [JsonObject,JsonConverter(typeof(ObjectsArrayConverter<UserShow>))]
        public class Test<T>:List<T>
        {
            
        }

        public MyShowsResponse<List<UserShow>> ListOfShows()
        {
            RestRequest request = new RestRequest(Methods.ListOfShows);
            return Execute<List<UserShow>>(request);
        }

        public MyShowsResponse<List<WatchedEpisode>> ListOfWatchedEpisodes(int showId)
        {
            RestRequest request = new RestRequest(Methods.ListOfWatchedEpisodes);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            return Execute<List<WatchedEpisode>>(request);
        }

        public MyShowsResponse<List<Episode>> ListOfPastEpisodes()
        {
            RestRequest request = new RestRequest(Methods.ListOfPastEpisodes);
            return Execute<List<Episode>>(request);
        }

        public MyShowsResponse<List<Episode>> ListOfNextEpisodes()
        {
            RestRequest request = new RestRequest(Methods.ListOfNextEpisodes);
            return Execute<List<Episode>>(request);
        }

        public MyShowsResponse MarkAsWatched(int episodeId, Rating rating = Rating.None)
        {
            RestRequest request = new RestRequest(Methods.MarkAsWatched);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            if (rating != Rating.None)
                request.AddParameter(Methods.Params.Rating, (int) rating);
            return Execute(request);
        }

        public MyShowsResponse MarkAsWatched(int showId, params int[] episodeIds)
        {
            RestRequest request = new RestRequest(Methods.SyncMarkAsWatched);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            request.AddParameter(Methods.Params.Episodes, Helper.ToCommaString(episodeIds));
            return Execute(request);
        }

        public MyShowsResponse UnmarkAsWatched(int episodeId)
        {
            RestRequest request = new RestRequest(Methods.UnmarkAsWatched);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse Sync(int showId, int[] watchedEpisodes, int[] unwatchedEpisodes)
        {
            RestRequest request = new RestRequest(Methods.Sync);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            request.AddParameter(Methods.Params.Check, Helper.ToCommaString(watchedEpisodes));
            request.AddParameter(Methods.Params.Uncheck, Helper.ToCommaString(unwatchedEpisodes));
            return Execute(request);
        }

        public MyShowsResponse SetShowStatus(int showId, WatchStatus watchStatus)
        {
            RestRequest request = new RestRequest(Methods.SetShowStatus);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            string statusString = watchStatus.GetAttributeValue<EnumMemberAttribute, string>(x => x.Value);
            request.AddParameter(Methods.Params.ShowStatus, statusString, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse SetShowRating(int showId, Rating rating)
        {
            RestRequest request = new RestRequest(Methods.SetShowRating);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            request.AddParameter(Methods.Params.Rate, (int) rating, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse SetEpisodeRating(int episodeId, Rating rating)
        {
            RestRequest request = new RestRequest(Methods.SetEpisodeRating);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            request.AddParameter(Methods.Params.Rate, (int) rating, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse<List<EpisodeId>> GetFavoriteEpisodes()
        {
            RestRequest request = new RestRequest(Methods.GetFavoriteEpisodes);
            return Execute<List<EpisodeId>>(request);
        }

        public MyShowsResponse AddEpisodeToFavorite(int episodeId)
        {
            RestRequest request = new RestRequest(Methods.AddEpisodeToFavorite);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse RemoveEpisodeFromFavorite(int episodeId)
        {
            RestRequest request = new RestRequest(Methods.RemoveEpisodeFromFavorite);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse<List<EpisodeId>> GetIgnoredEpisodes()
        {
            RestRequest request = new RestRequest(Methods.GetIgnoredEpisodes);
            return Execute<List<EpisodeId>>(request);
        }

        public MyShowsResponse AddEpisodeToIgnored(int episodeId)
        {
            RestRequest request = new RestRequest(Methods.AddEpisodeToIgnored);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse RemoveEpisodeFromIgnored(int episodeId)
        {
            RestRequest request = new RestRequest(Methods.RemoveEpisodeFromIgnored);
            request.AddParameter(Methods.Params.EpisodeId, episodeId, ParameterType.UrlSegment);
            return Execute(request);
        }

        public MyShowsResponse<Dictionary<DateTime,List<News>>> GetFriendsNews()
        {
            //todo check for the easier implementation
            RestRequest request = new RestRequest(Methods.FriendsNews);
            MyShowsResponse<Dictionary<string, List<News>>> myShowsResponse = Execute<Dictionary<string, List<News>>>(request);
            return new MyShowsResponse<Dictionary<DateTime, List<News>>>(myShowsResponse.Response) { Data = new NewsCollection(myShowsResponse.Data) };
        }

        public MyShowsResponse<List<Show>> Search(string q)
        {
            RestRequest request = new RestRequest(Methods.Search);
            request.AddParameter(Methods.Params.Query.ToString(CultureInfo.InvariantCulture), q);
            return Execute<List<Show>>(request);

        }

        protected internal MyShowsResponse<Dictionary<int, Genre>> GetGenres()
        {
            RestRequest request = new RestRequest(Methods.GetGenres);
            return Execute<Dictionary<int, Genre>>(request);
        }

        public MyShowsResponse<FileSearchResult> SearchByFile(string q)
        {
            RestRequest request = new RestRequest(Methods.SearchByFile);
            request.AddParameter(Methods.Params.Query.ToString(CultureInfo.InvariantCulture), q);
            return Execute<FileSearchResult>(request);
            ;
        }

        public MyShowsResponse<List<Show>> TopRatedShows(RateFilter filter)
        {
            RestRequest request = new RestRequest(Methods.TopRatedShows);
            request.AddParameter(Methods.Params.Gender, RateFilterParams.ToString(filter), ParameterType.UrlSegment);
            return Execute<List<Show>>(request);
        }

        public MyShowsResponse<UserProfile> GetProfile(string username)
        {
            RestRequest request = new RestRequest(Methods.UserProfile);
            request.AddParameter(Methods.Params.Login, username, ParameterType.UrlSegment);
            return Execute<UserProfile>(request);
        }

        #endregion
    }
}
