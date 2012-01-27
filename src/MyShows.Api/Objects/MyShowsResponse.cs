using MyShows.Api.Constants;
using RestSharp;

namespace MyShows.Api.Objects
{
    public interface IMyShowsResponse
        {
            bool IsOk { get; }
            Status Status { get; set; }
        }

    public interface IMyShowsResponse<T> : IMyShowsResponse
        {
            T Data { get; set; }
        }

    public abstract class MyShowsResponseBase
        {
            #region Properties (Stats)
            
            protected internal IRestResponse Response { get; set; }

            public bool IsOk { get { return Status == Status.Success; } }

            public Status Status { get; set; }

            #endregion

            protected internal MyShowsResponseBase(IRestResponse response)
            {
                Response = response;
                Status = (Status)response.StatusCode;
            }

            protected MyShowsResponseBase()
            {
            }
        }

    public class MyShowsResponse<T> : MyShowsResponseBase, IMyShowsResponse<T>
        {

            protected internal MyShowsResponse(IRestResponse<T> response)
                : base(response)
            {
                Data = response.Data;
            }

            protected internal MyShowsResponse(IRestResponse response)
                : base(response)
            {
            }


            public static implicit operator MyShowsResponse<T>(RestResponse response)
            {
                return new MyShowsResponse<T>(response);
            }

            public static implicit operator MyShowsResponse<T>(RestResponse<T> response)
            {
                return new MyShowsResponse<T>(response);
            }

            #region Properties

            public T Data { get; set; }

            #endregion

        }

    public class MyShowsResponse : MyShowsResponseBase, IMyShowsResponse
    {
        protected internal MyShowsResponse(RestResponse response)
            : base(response)
        {
        }

        public static implicit operator MyShowsResponse(RestResponse response)
        {
            return new MyShowsResponse(response);
        }
    }
}

