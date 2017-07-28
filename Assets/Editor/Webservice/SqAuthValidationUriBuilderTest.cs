using NUnit.Framework;
using Webservice.UriBuilding;
using System;

class SqAuthValidationUriBuilderTest
{
    [Test]
    public void UriIsSetCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void BaseUriNotEmpty()
    {
        string username = "username";
        string password = "password";
        Assert.Throws<UriFormatException>(() => new SqAuthValidationUriBuilder("", username, password));
    }

    [Test]
    public void BaseUriNotNull()
    {
        string username = "username";
        string password = "password";
        Assert.Throws<ArgumentNullException>(() => new SqAuthValidationUriBuilder(null, username, password));
    }

    [Test]
    public void EmptyUsernameInConstructor()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "";
        string password = "password";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        string expectedBaseUri = "http://sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPasswordInConstructor()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        string expectedBaseUri = "http://sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullUsernameInConstructor()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = null;
        string password = "password";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        string expectedBaseUri = "http://sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPasswordInConstructor()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = null;

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        string expectedBaseUri = "http://sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string toAppend = "some/path/";
        string alsoToAppend = null;

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate/";
        Assert.AreEqual(expectedBaseUri + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string toAppend = "some/path/";
        string alsoToAppend = "";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate/";
        Assert.AreEqual(expectedBaseUri + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsPathAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string toAppend = "some/path/";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToPath(toAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate/";
        Assert.AreEqual(expectedBaseUri + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultiplePathsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string toAppend = "some/path/";
        string alsoToAppend = "other/path/";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate/";
        Assert.AreEqual(expectedBaseUri + toAppend + alsoToAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsQueryAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string query = "key=value";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToQuery(query);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultipleQuerysAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string query = "key=value";
        string alsoToAppend = "anotherKey=anotherValue";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri + "?" + query + "&" + alsoToAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string query = "key=value";
        string alsoToAppend = null;

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        string query = "key=value";
        string alsoToAppend = "";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        uri.UserCredentials("", password);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        uri.UserCredentials(username, "");

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        uri.UserCredentials(null, password);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";
        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, username, password);

        uri.UserCredentials(username, null);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void CredentialsInsertedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, null, null);
        uri.UserCredentials(username, password);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void CredentialsOverriddenCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";

        SqAuthValidationUriBuilder uri = new SqAuthValidationUriBuilder(baseUri, "otherUsername", "otherPassword");
        uri.UserCredentials(username, password);

        string expectedBaseUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/api/authentication/validate";
        Assert.AreEqual(expectedBaseUri, uri.GetSqUri().ToString());
    }
}

