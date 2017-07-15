using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour {

    [SerializeField]
    private string sqObjectType;

    [SerializeField]
    private List<GameObject> childs;

    public void SetSQObjectType(string sqObjectType)
    {
        this.sqObjectType = sqObjectType;
    }

    public string GetSQObjectType()
    {
        return sqObjectType;
    }

    public void SetChilds(List<GameObject> childs)
    {
        this.childs = childs;
    }

    public List<GameObject> GetChilds()
    {
        return childs;
    }
}
