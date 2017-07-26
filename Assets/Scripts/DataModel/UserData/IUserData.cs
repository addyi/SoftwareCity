namespace DataModel.UserData
{
    /// <summary>
    /// Methods the Model needs to provide for UserData handling
    /// </summary>
    interface IUserData
    {
        /// <summary>
        /// Returns the base url of the sonarqube server
        /// </summary>
        /// <returns>base url of the sonarqube server</returns>
        string GetBaseUrl();
        /// <summary>
        /// Returns the username for the sonarqube server
        /// </summary>
        /// <returns>username for the sonarqube server</returns>
        string GetUsername();
        /// <summary>
        /// Returns the password of the user for the sonarqube server
        /// </summary>
        /// <returns>password of the user for the sonarqube server</returns>
        string GetPassword();
        /// <summary>
        /// Sets the UserCredentials and BaseUrl for the SonarQube server
        /// </summary>
        /// <param name="BaseUrl">BaseUrl for the SonarQube server</param>
        /// <param name="Username">Username for the SonarQube server</param>
        /// <param name="Password">Password for the SonarQube server</param>
        void SetCredentials(string BaseUrl, string Username, string Password);
    }
}
