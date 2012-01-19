using System;
using System.Collections.Generic;
using System.Linq;
using MyShows.Api.Constants;
using MyShows.Api.Objects;
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

        #region Methods


        #endregion

        #region Helper Methods


        private MyShowsResponse Execute(RestRequest request)
        {
            RestClient client = new RestClient(ApiUrl);
            request = RequestSetup(request);
            return new MyShowsResponse(client.Execute(request));
        }

        private MyShowsResponse<T> Execute<T>(RestRequest request, Credentials cred = null) where T : new()
        {
            RestClient client = new RestClient(ApiUrl);
            request = RequestSetup(request);
            return new MyShowsResponse<T>(client.Execute<T>(request));
        }

        private RestRequest RequestSetup(RestRequest request)
        {
            //request.Resource = !request.Resource.EndsWith(".php") ? string.Format("{0}.php", request.Resource) : request.Resource;
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            if(!string.IsNullOrEmpty(phpSessId))
                request.AddCookie(Methods.Params.PhpSessionId, phpSessId);
            return request;
        }

        #endregion

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

        public MyShowsResponse<ShowsCollection>  ListOfShows()
        {
            RestRequest request = new RestRequest(Methods.ListOfShows);
            MyShowsResponse<Dictionary<int, Show>> myShowsResponse = Execute<Dictionary<int, Show>>(request);
            MyShowsResponse<ShowsCollection> response = new MyShowsResponse<ShowsCollection>(myShowsResponse.Response);
            response.Data = new ShowsCollection(myShowsResponse.Data);
            return response;
        }

        public MyShowsResponse<WatchedEpisodesCollection> ListOfWatchedEpisodes(int showId)
        {
            RestRequest request = new RestRequest(Methods.ListOfWatchedEpisodes);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            MyShowsResponse<Dictionary<int, WatchedEpisode>> myShowsResponse = Execute<Dictionary<int, WatchedEpisode>>(request);
            MyShowsResponse<WatchedEpisodesCollection> response = new MyShowsResponse<WatchedEpisodesCollection>(myShowsResponse.Response);
            response.Data = new WatchedEpisodesCollection(myShowsResponse.Data);
            return response;
        }

        public MyShowsResponse<EpisodesCollection> ListOfPastEpisodes()
        {
            RestRequest request = new RestRequest(Methods.ListOfPastEpisodes);
            MyShowsResponse<Dictionary<int, Episode>> myShowsResponse = Execute<Dictionary<int, Episode>>(request);
            MyShowsResponse<EpisodesCollection> response = new MyShowsResponse<EpisodesCollection>(myShowsResponse.Response);
            response.Data = new EpisodesCollection(myShowsResponse.Data);
            return response;
        }

        public MyShowsResponse<EpisodesCollection> ListOfNextEpisodes()
        {
            RestRequest request = new RestRequest(Methods.ListOfNextEpisodes);
            MyShowsResponse<Dictionary<int, Episode>> myShowsResponse = Execute<Dictionary<int, Episode>>(request);
            MyShowsResponse<EpisodesCollection> response = new MyShowsResponse<EpisodesCollection>(myShowsResponse.Response);
            response.Data = new EpisodesCollection(myShowsResponse.Data);
            return response;
        }

        public MyShowsResponse MarkAsWatched(int episodeId, Rating rating = Rating.None)
        {
            RestRequest request = new RestRequest(Methods.MarkAsWatched);
            request.AddParameter(Methods.Params.EpisodeId, episodeId,ParameterType.UrlSegment);
            if (rating != Rating.None)
                request.AddParameter(Methods.Params.Rating, (int) rating);
            return Execute(request);
        }

        public MyShowsResponse MarkAsWatched(int showId, params int[] episodes)
        {
            RestRequest request = new RestRequest(Methods.SyncMarkAsWatched);
            request.AddParameter(Methods.Params.ShowId, showId, ParameterType.UrlSegment);
            request.AddParameter(Methods.Params.Episodes, Helper.ToCommaString(episodes));
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
            request.AddParameter(Methods.Params.ShowStatus, Show.WatchStatusToString(watchStatus), ParameterType.UrlSegment);
            return Execute(request);
        }
    }
}
