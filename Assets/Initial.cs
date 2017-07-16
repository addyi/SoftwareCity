using DataModel;
using DataModel.ProjectTree;
using DataModel.ProjectTree.Components;
using SoftwareCity.Rendering;
using UnityEngine;
using Webservice;
using Webservice.Response.ComponentTree;
using Webservice.UriBuilding;

public class Initial : MonoBehaviour {

    private readonly string baseUri = "http://sonarqube.eosn.de/";
    private readonly string username = "user";
    private readonly string pw = "123456";
    private readonly string metricKeys = "ncloc,bugs,vulnerabilities,code_smells,violations,functions,coverage,test_success_density,comment_lines_density";
    private readonly string projectKey = "geo-quiz-app";

    public ProjectComponent rootProjectComponent;

    // Use this for initialization
    void Start () {
        StartCoroutine(WebInterface.WebRequest<ComponentTree>(
            new SqComponentTreeUriBuilder(baseUri, projectKey, metricKeys)
                 .UserCredentials(username, pw).GetSqUri(),
            (System.Action<ComponentTree, long>)((res, err) =>
            {
                switch (err)
                {
                    case 200:
                        Debug.Log(res.baseComponent.ToString());
                        Debug.Log(res.paging.ToString());
                        IProjectTree ProjectTree = new Model();

                        rootProjectComponent = ProjectTree.BuildProjectTree(res.baseComponent, res.components);

                        break;
                    default:
                        Debug.Log("Addyi ResponseCode: " + err);
                        break;

                }
            }))
            );
    }
}
