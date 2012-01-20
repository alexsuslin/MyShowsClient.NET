using System.Collections.Generic;
using MyShows.Api.Constants;
using Newtonsoft.Json;
using RestSharp;

namespace MyShows.Api.Objects
{
    public class MyShowsResponse
    {
        #region Fields

        public readonly Status Status;
        public readonly bool isOk;

        #endregion

        #region Properties (Stats)

        protected internal IRestResponse Response { get; private set; }

        #endregion

        #region Constructors

        internal MyShowsResponse(IRestResponse response)
        {
            Response = response;
            Status = (Status) response.StatusCode;
            isOk = (Status == Status.Success);
        }

        #endregion
    }

    public class MyShowsResponse<T> : MyShowsResponse
    {
        #region Properties

        public T Data { get; protected internal set; }

        #endregion

        #region Constructors

        internal MyShowsResponse(IRestResponse<T> response)
            : base(response)
        {
            // There is a bug in RestSharp
            //UserStatsWrapper wrapper = restResponse.Data;

            if (isOk)
                Data = JsonConvert.DeserializeObject<T>(response.Content);
        }

        protected internal MyShowsResponse(IRestResponse response, T data = default(T))
            : base(response)
        {
            Data = data;
        }

        #endregion

        public IEnumerator<KeyValuePair<int, Genre>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}

