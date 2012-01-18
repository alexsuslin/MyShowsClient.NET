namespace MyShows.Api.Objects
{
    public class Credentials
    {
        #region Properties

        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructors

        public Credentials(string username, string rawPassword)
        {
            Username = username;
            Password = Helper.GetMd5Hash(rawPassword);
        }

        #endregion
    }
}
