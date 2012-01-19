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

        [TestMethod]
        public void ListOfPastEpisodes()
        {
            MyShowsResponse<EpisodesCollection> response;
            response = InvalidClient.ListOfPastEpisodes();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.ListOfPastEpisodes();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            foreach (Episode episode in response.Data)
                Assert.IsNotNull(episode);
        }

        [TestMethod]
        public void ListOfNextEpisodes()
        {
            MyShowsResponse<EpisodesCollection> response;
            response = InvalidClient.ListOfNextEpisodes();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.ListOfNextEpisodes();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            foreach (Episode episode in response.Data)
                Assert.IsNotNull(episode);
        }

        [TestMethod]
        public void MarkAsWatched()
        {
            MyShowsResponse response;
            response = InvalidClient.MarkAsWatched(291461);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.MarkAsWatched(291461);
            Assert.AreEqual(response.Status, Status.Success);

            response = Client.MarkAsWatched(291461, Rating.Excellent);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void SyncMarkAsWatched()
        {
            MyShowsResponse response;
            response = InvalidClient.MarkAsWatched(142, 16388, 16387, 16386);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.MarkAsWatched(-1, 16388, 16387, 16386);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.MarkAsWatched(142, 16388, 16387, 16386);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void UnmarkAsWatched()
        {
            MyShowsResponse response;
            response = InvalidClient.UnmarkAsWatched(291461);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.UnmarkAsWatched(291461);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void SyncEpisodes()
        {
            MyShowsResponse response;
            response = InvalidClient.Sync(142, new[] {16388, 16387}, new[] {16386});
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.Sync(-1, new[] { 16388, 16387 }, new[] { 16386 });
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.Sync(142, new[] { 16388, 16387 }, new[] { 16386 });
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void SetShowWatchedStatus()
        {
            MyShowsResponse response;
            response = InvalidClient.SetShowStatus(1, WatchStatus.Later);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.SetShowStatus(-1, WatchStatus.Later);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.SetShowStatus(1, WatchStatus.Later);
            Assert.AreEqual(response.Status, Status.Success);
        }
    }
}
