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
        private readonly Credentials wrongCredentials;
        private readonly MyShowsClient Client;
        private readonly MyShowsClient InvalidClient;

        public ApiTests()
        {
            credentials = new Credentials(Config.Username, Config.Password);
            wrongCredentials = new Credentials("wrongusername", "wrongpassword");
            Client = new MyShowsClient(credentials);
            InvalidClient = new MyShowsClient(wrongCredentials);
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

            response = Client.Auth(wrongCredentials);
            Assert.AreEqual(response.Status, Status.InvalidCredentials);

            response = Client.Auth(new Credentials(string.Empty, string.Empty));
            Assert.AreEqual(response.Status, Status.NotFound);
        }

        [TestMethod]
        public void ListOfShows()
        {
            MyShowsResponse<ShowsCollection> response;

            response = InvalidClient.ListOfShows();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.ListOfShows();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            foreach (Show show in response.Data)
            {
                Assert.IsNotNull(show);
            }
        }

        [TestMethod]
        public void ListOfWatchedEpisodes()
        {
            MyShowsResponse<WatchedEpisodesCollection> response;
            response = InvalidClient.ListOfWatchedEpisodes(1);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.ListOfWatchedEpisodes(1);
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            foreach (WatchedEpisode episode in response.Data)
            {
                Assert.IsNotNull(episode);
                Assert.IsNotNull(episode.WatchDate);
            }
        }
    }
}
