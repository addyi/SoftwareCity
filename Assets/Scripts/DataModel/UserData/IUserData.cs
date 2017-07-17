namespace DataModel.UserData
{
    interface IUserData
    {
        string GetBaseUrl();
        string GetUsername();
        string GetPassword();
        void SetCredentials(string BaseUrl, string Username, string Password);
    }
}
