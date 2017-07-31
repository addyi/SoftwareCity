using NUnit.Framework;
using Webservice.UriBuilding;
using System;

class SQUriBuilderTest
{
    [Test]
    public void BaseUriNotEmpty()
    {
        Assert.Throws<UriFormatException>(() => new SqUriBuilder(""));
    }

    [Test]
    public void BaseUriNotNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SqUriBuilder(null));
    }

    [Test]
    public void UriIsSetCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";

        SqUriBuilder uri = new SqUriBuilder(baseUri);

        Assert.AreEqual(baseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "api/some/path/";
        string alsoToAppend = null;

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        Assert.AreEqual(baseUri + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "api/some/path/";
        string alsoToAppend = "";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        Assert.AreEqual(baseUri + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsPathAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "api/some/path/";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToPath(toAppend);

        Assert.AreEqual(baseUri + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultiplePathsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "api/some/path/";
        string alsoToAppend = "other/path/";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        Assert.AreEqual(baseUri + toAppend + alsoToAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsQueryAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToQuery(query);

        Assert.AreEqual(baseUri + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultipleQuerysAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";
        string alsoToAppend = "anotherKey=anotherValue";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        Assert.AreEqual(baseUri + "?" + query + "&" + alsoToAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";
        string alsoToAppend = null;

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        Assert.AreEqual(baseUri + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";
        string alsoToAppend = "";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        Assert.AreEqual(baseUri + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "";
        string password = "password";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = null;
        string password = "password";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = null;

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void CredentialsInsertedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";

        SqUriBuilder uri = new SqUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual("http://" + username + ":" + password + "@" + "sonarqube.test.de/", uri.GetSqUri().ToString());
    }
}

