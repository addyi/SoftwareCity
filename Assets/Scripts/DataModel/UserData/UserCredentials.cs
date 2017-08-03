namespace DataModel.UserData
{
    /// <summary>
    /// POCO to store the UserCredentials and the base uri
    /// </summary>
    class UserCredentials
    {
        /// <summary>
        /// base url of the sonarqube server
        /// </summary>
        public string baseUri = "";
        /// <summary>
        /// username for the sonarqube server
        /// </summary>
        public string username = "";
        /// <summary>
        /// password of the user for the sonarqube server
        /// </summary>
        public string password = "";
    }
}
