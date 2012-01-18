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

        public MyShowsResponse<ShowCollection>  ListOfShows()
        {
            RestRequest request = new RestRequest(Methods.ListOfShows);
            MyShowsResponse<Dictionary<int, Show>> myShowsResponse = Execute<Dictionary<int, Show>>(request);
            MyShowsResponse<ShowCollection> response = new MyShowsResponse<ShowCollection>(myShowsResponse.Response);
            response.Data = new ShowCollection(myShowsResponse.Data);
            return response;
        }
    }
}
