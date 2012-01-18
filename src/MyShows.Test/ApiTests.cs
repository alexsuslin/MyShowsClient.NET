using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShows.Api;
using MyShows.Api.Constants;
using MyShows.Api.Objects;

namespace MyShows.Test
{

    [TestClass]
    public class ApiTests
    {
        private readonly Credentials credentials;
        private readonly MyShowsClient Client;

        public ApiTests()
        {
            credentials = new Credentials(Config.Username, Config.Password);
            Client = new MyShowsClient(credentials);
        }

        [TestInitialize]
        public void StartUp()
        {
            Client.Auth(credentials);
        }

        [TestMethod]
        public void Auth()
        {
            MyShowsResponse response;
            
            response = Client.Auth(credentials);
            Assert.AreEqual(response.Status, Status.Success);

            response = Client.Auth(new Credentials("wrongusername", "wrongpassword"));
            Assert.AreEqual(response.Status, Status.InvalidCredentials);

            response = Client.Auth(new Credentials("", ""));
            Assert.AreEqual(response.Status, Status.NotFound);
        }

        [TestMethod]
        public void ListOfShows()
        {

            MyShowsResponse<ShowCollection> response;

            response = new MyShowsClient(new Credentials("wrongusername", "wrongpassword")).ListOfShows();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.ListOfShows();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            foreach (Show show in response.Data)
            {
                Assert.IsNotNull(show);
            }

        }
    }
}
