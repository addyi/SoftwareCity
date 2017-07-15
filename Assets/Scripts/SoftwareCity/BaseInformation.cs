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

    /*
    public void SetSQObjectType(string sqObjectType)
    {
        this.sqObjectType = sqObjectType;
    }

    public string GetSQObjectType()
    {
        return sqObjectType;
    }
    */
    public void SetChilds(List<GameObject> childs)
    {
        this.childs = childs;
    }

    public List<GameObject> GetChilds()
    {
        return childs;
    }
}
