using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour {

    [SerializeField]
    private string sqObjectType;

    public void SetSQObjectType(string sqObjectType)
    {
        this.sqObjectType = sqObjectType;
    }

    public string GetSQObjectType()
    {
        return sqObjectType;
    }
}
