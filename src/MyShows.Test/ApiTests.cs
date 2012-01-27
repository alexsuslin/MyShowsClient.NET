using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShows.Api;
using MyShows.Api.Constants;
using MyShows.Api.Objects;
using Newtonsoft.Json;

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
            MyShowsResponse<List<UserShow>> response;

            response = InvalidClient.ListOfShows();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.ListOfShows();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            foreach (UserShow show in response.Data)
            {
                Assert.IsNotNull(show);
            }
        }

        [TestMethod]
        public void ListOfWatchedEpisodes()
        {
            MyShowsResponse<List<WatchedEpisode>> response;
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
            MyShowsResponse<List<Episode>> response;
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
            MyShowsResponse<List<Episode>> response;
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

        [TestMethod]
        public void SetShowRating()
        {
            MyShowsResponse response;
            response = InvalidClient.SetShowRating(1, Rating.Fair);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.SetShowRating(-1, Rating.Fair);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.SetShowRating(1, Rating.Fair);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void SetEpisodeRating()
        {
            MyShowsResponse response;
            response = InvalidClient.SetEpisodeRating(291461, Rating.Fair);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.SetEpisodeRating(-1, Rating.Fair);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.SetEpisodeRating(291461, Rating.Fair);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void GetFavoriteEpisodes()
        {
            MyShowsResponse<List<EpisodeId>> response;
            response = InvalidClient.GetFavoriteEpisodes();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.GetFavoriteEpisodes();
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void AddEpisodeToFavorite()
        {
            MyShowsResponse response;
            response = InvalidClient.AddEpisodeToFavorite(291461);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.AddEpisodeToFavorite(-1);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.AddEpisodeToFavorite(291461);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void RemoveEpisodeFromFavorite()
        {
            MyShowsResponse response;
            response = InvalidClient.RemoveEpisodeFromFavorite(291461);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.RemoveEpisodeFromFavorite(-1);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.RemoveEpisodeFromFavorite(291461);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void GetIgnoredEpisodes()
        {
            MyShowsResponse<List<EpisodeId>> response;
            response = InvalidClient.GetIgnoredEpisodes();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.GetIgnoredEpisodes();
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void AddEpisodeToIgnored()
        {
            MyShowsResponse response;
            response = InvalidClient.AddEpisodeToIgnored(291461);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.AddEpisodeToIgnored(-1);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.AddEpisodeToIgnored(291461);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void RemoveEpisodeFromIgnored()
        {
            MyShowsResponse response;
            response = InvalidClient.RemoveEpisodeFromIgnored(291461);
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.RemoveEpisodeFromIgnored(-1);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.RemoveEpisodeFromIgnored(291461);
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void FriendsNews()
        {
            MyShowsResponse<Dictionary<DateTime,List<News>>> response;
            response = InvalidClient.GetFriendsNews();
            Assert.AreEqual(response.Status, Status.AuthenticationRequired);

            response = Client.GetFriendsNews();
            Assert.AreEqual(response.Status, Status.Success);
            foreach (KeyValuePair<DateTime, List<News>> pair in response.Data)
            {
                Assert.IsNotNull(pair);
                Assert.AreEqual(true, pair.Value.Count > 0);
            }
        }

        [TestMethod]
        public void Search()
        {
            MyShowsResponse<List<Show>> response;
            response = Client.Search(string.Empty);
            Assert.AreEqual(response.Status, Status.InvalidRequest);
            
            response = Client.Search("Theory");
            Assert.AreEqual(response.Status, Status.Success);
            foreach (Show show in response.Data)
            {
                Assert.IsNotNull(show);
            }
        }

        [TestMethod]
        public void SearchByFile()
        {
            MyShowsResponse<FileSearchResult> response;
            response = Client.SearchByFile(string.Empty);
            Assert.AreEqual(response.Status, Status.InvalidRequest);

            response = Client.SearchByFile("Star.Wars.The.Clone.Wars.s02e01e02.rus.LostFilm.TV.avi");
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data.Show.Episodes);
            Assert.AreEqual(true, response.Data.Show.Episodes.Count > 0);
        }

        [TestMethod]
        public void TopRatedShows()
        {
            MyShowsResponse<List<Show>> response;
            response = Client.TopRatedShows(RateFilter.None);
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.TopRatedShows(RateFilter.All);
            Assert.AreEqual(response.Status, Status.Success);
            Assert.AreEqual(true, response.Data.Count > 0);

            response = Client.TopRatedShows(RateFilter.Male);
            Assert.AreEqual(response.Status, Status.Success);
            Assert.AreEqual(true, response.Data.Count > 0);

            response = Client.TopRatedShows(RateFilter.Female);
            Assert.AreEqual(response.Status, Status.Success);
            Assert.AreEqual(true, response.Data.Count > 0);
        }

        [TestMethod]
        public void Profile()
        {
            MyShowsResponse<UserProfile> response;
            response = Client.GetProfile("djs435fnd");
            Assert.AreEqual(response.Status, Status.NotFound);

            response = Client.GetProfile("alexsuslin");
            Assert.AreEqual(response.Status, Status.Success);
        }
    }
}
