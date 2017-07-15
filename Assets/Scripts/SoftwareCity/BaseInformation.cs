using DataModel;
using DataModel.Metrics;
using DataModel.ProjectTree.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInformation : MonoBehaviour {

    //[SerializeField]
    //private string sqObjectType;

    private string id;

    private string key;

    private string componentName;

    private SqQualifier qualifier;

    private List<TreeMetric> metrics;

    [SerializeField]
    private List<GameObject> childs;

    public void UpdateValues(TreeComponent treeComponent)
    {
        this.id = treeComponent.ID;
        this.key = treeComponent.Key;
        this.componentName = treeComponent.Name;
        this.qualifier = treeComponent.Qualifier;
        this.metrics = treeComponent.Metrics;
    }

    public SqQualifier GetQualifier()
    {
        return qualifier;
    }

    public void SetChilds(List<GameObject> childs)
    {
        this.childs = childs;
    }

    public List<GameObject> GetChilds()
    {
        return childs;
    }

    public override string ToString()
    {
        string metricString = "";
        foreach(TreeMetric metric in metrics)
        {
            metricString += "   " + metric.Key + ": " + metric.Value + "\n";
        }

        return "<b>Type:</b> <size=40pt>" + qualifier + "</size>\n<b>Id:</b> <size=40pt>" + this.id + "</size>\n<b>Key:</b> <size=40pt>" + this.key + "</size>\n<b>Name:</b> <size=40pt>" + componentName + "</size>\n<b>Metrics:</b> \n" + metricString;
    }
}
