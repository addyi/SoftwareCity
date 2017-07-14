using UnityEngine;
using Webservice;
using Webservice.Response.Authentication;
using Webservice.UriBuilding;
using Webservice.Response.Project;
using Webservice.Response.ComponentTree;
using System.Collections.Generic;
using DataModel;
using DataModel.ProjectTree;
using DiskIO.AvailableMetrics;
using DiskIO.ProjectTreeSaveLoader;


//calls to generate a test enviroment
public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject enviroment;

    private bool enviromentExist;

    private void Start()
    {
        enviromentExist = false;

        const string baseUri = "http://sonarqube.eosn.de/";
        const string username = "user";
        const string pw = "123456";
        const string metricKeys = "ncloc,bugs,vulnerabilities,code_smells,violations,functions,coverage,test_success_density,comment_lines_density";
        const string projectKey = "geo-quiz-app";

        // TODO ADDYI REMOVE DEBUG LOGS
        StartCoroutine(WebInterface.WebRequest<Auth>(
            new SqAuthValidationUriBuilder(baseUri, username, pw).GetSqUri(),
            (res, err) =>
            {
                switch (err)
                {
                    case 200:
                        Debug.Log("Addyi Auth:" + res.valid.ToString());
                        break;
                    default:
                        Debug.Log("Addyi ResponseCode: " + err);
                        break;

                }
            }));

        StartCoroutine(WebInterface.WebRequest<List<SQProject>>(
           new SqProjectUriBuilder(baseUri).UserCredentials(username, pw).GetSqUri(),
           (res, err) =>
           {
               switch (err)
               {
                   case 200:
                       res.ForEach((projekt) =>
                       {
                           Debug.Log("Addyi Projekt: " + projekt.ToString());
                       });
                       break;
                   default:
                       Debug.Log("Addyi ResponseCode: " + err);
                       break;

               }
           }));

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

                       // List<Webservice.Response.ComponentTree.Component> components = res.components;
                       //components.Sort();


                       ProjectTree.BuildProjectTree(res.baseComponent, res.components);
                       Debug.Log(ProjectTree.GetTree().ToString());

                       break;
                   default:
                       Debug.Log("Addyi ResponseCode: " + err);
                       break;

               }

           })));

        List<Metric> metrics = AvailableMetricConfigReader.ReadConfigFile();
        foreach (Metric m in metrics)
        {
            Debug.Log(string.Format("Metric {0}, {1}, {2}, {3}", m.name, m.key, m.defaultvalue, m.datatype));
        }

		//ComponentTreeStream.SerializeObject<Metric>(metrics[0]);

		Metric metric = ComponentTreeStream.DeserializeObject<Metric>();
		Debug.Log(string.Format("Metric for IO debG {0}", metric.name));




	}

    public void CreateEnvironment()
    {
        if (!enviromentExist)
        {
            Instantiate(enviroment, transform.localPosition, transform.localRotation);
            enviromentExist = true;
        }

    }

}