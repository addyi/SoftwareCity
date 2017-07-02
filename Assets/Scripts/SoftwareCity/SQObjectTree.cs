using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SoftwareCity.Envelope.Dimension;

public class SQObjectTree : MonoBehaviour {

    private int packageCounterGlobal = 0;
    private int documentCounterGlobal = 0;

    private readonly Vector3 packageBorder = new Vector3(0.2f, 0.0f, 0.2f);

    private readonly Vector3 localScaleOfDocument = new Vector3(0.2f, 0.3f, 0.2f);

    private System.Random rnd;

    private readonly Color[] colors = new Color[] { Color.green, Color.yellow };

    private List<GameObject> helperGameObjects = new List<GameObject>();

    private float levelHeight = 0.1f;

    private int packageLevel;

    private float shiftingFactorYDirection = -0.01f;

    // Use this for initialization
    void Start () {
        rnd = new System.Random();

        packageLevel = 1;

        SQPackage root = new SQPackage();
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());
        root.AddChild(new SQDocument());

        SQPackage p2 = new SQPackage();
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());
        p2.AddChild(new SQDocument());


        SQPackage p3 = new SQPackage();
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());
        p3.AddChild(new SQDocument());

        SQPackage p4 = new SQPackage();
        p4.AddChild(new SQDocument());
        p4.AddChild(new SQDocument());
        p4.AddChild(new SQDocument());
        p4.AddChild(new SQDocument());

        root.AddChild(p2);
        root.AddChild(p3);
        root.AddChild(p4);
        


        GameObject rootGameObject = TraverseTree(root, packageLevel);

        DeleteHelperGameObjects(helperGameObjects);

        rootGameObject.transform.SetParent(this.gameObject.transform);
        rootGameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x /10f, this.gameObject.transform.localScale.y/ 10f, this.gameObject.transform.localScale.z/ 10f);

       //this.gameObject.transform.parent.GetComponent<EnvelopeDimension>().UpdateDimensionPoints();
    }

    private GameObject TraverseTree(ISQObject treeObject, int packageLevel)
    {
        if (treeObject is SQPackage)
        {
            List<GameObject> childs = new List<GameObject>();

            foreach(ISQObject child in ((SQPackage)treeObject).GetChilds())
            {
                childs.Add(TraverseTree(child, packageLevel + 1));
            }

            /*
            for(int i = 0; i < childs.Count; i++)
            {
                print(childs[i].name);
            }
            print("-------------------------------------");
            */
            List<GameObject> childDocuments = FilterDocuments(childs);
            List<GameObject> childPackages = FilterPackages(childs);
            if(childs.Count > 0)
            {
                if(childPackages.Count > 0 && childDocuments.Count > 0)
                {
                    CalculateChildPositions(childDocuments);

                    GameObject helperGameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    helperGameobject.AddComponent<Information>();
                    helperGameobject.GetComponent<Information>().SetSQObjectType("package");
                    helperGameObjects.Add(helperGameobject);

                    helperGameobject.transform.localScale = CalculatePackageSize(childDocuments, helperGameobject);
                    SetPackageGameObjectAsParent(helperGameobject, childDocuments);

                    childPackages.Add(helperGameobject);
                    CalculateChildPositions(childPackages);
                } else
                {
                    if(childs.Count > 0)
                        CalculateChildPositions(childs);  //--> WICHTIG !!!!!!!!!!!!!!!!!!!!
                }
            }

            GameObject packageGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            packageGameObject.AddComponent<Information>();
            packageGameObject.GetComponent<Information>().SetSQObjectType("package");
            packageGameObject.GetComponent<Collider>().enabled = false;
            packageGameObject.GetComponent<Renderer>().enabled = false;
            packageGameObject.name = "Package " + packageCounterGlobal++;
            //print("[" + packageGameObject.name + "]" + packageLevel);

            packageGameObject.GetComponent<Renderer>().material.color = colors[packageLevel%2];

            if(childPackages.Count > 0 && childDocuments.Count > 0)
            {
                packageGameObject.transform.localScale = CalculatePackageSize(childPackages, packageGameObject);
                SetPackageGameObjectAsParent(packageGameObject, childPackages);
            } else
            {
                packageGameObject.transform.localScale = CalculatePackageSize(childs, packageGameObject);
                SetPackageGameObjectAsParent(packageGameObject, childs);
            }

            return packageGameObject;
        } else
        {
            GameObject documentGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            documentGameObject.AddComponent<Information>();
            documentGameObject.GetComponent<Information>().SetSQObjectType("document");
            documentGameObject.name = "Document " + documentCounterGlobal++;
            documentGameObject.GetComponent<Renderer>().material.color = Color.red;
            documentGameObject.GetComponent<Collider>().enabled = false;
            documentGameObject.GetComponent<Renderer>().enabled = false;
            documentGameObject.transform.localScale = localScaleOfDocument;
            documentGameObject.transform.position = new Vector3(documentGameObject.transform.position.x, documentGameObject.GetComponent<Renderer>().bounds.size.y / 2, documentGameObject.transform.position.z);
           // print(documentGameObject.GetComponent<Renderer>().bounds.size.y / 2);

            return documentGameObject;
        }
    }

    private void SetPackageGameObjectAsParent(GameObject packageGameObject, List<GameObject> childs)
    {
        foreach(GameObject child in childs)
        {
            child.transform.SetParent(packageGameObject.transform);
        }
    }

    private Vector3 CalculatePackageSize(List<GameObject> childs, GameObject packageGameObject)
    {
        Vector3 minPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 maxPosition = new Vector3(0.0f, 0.0f, 0.0f);

        foreach(GameObject child in childs)
        {
            Bounds childBounds = child.GetComponent<Renderer>().bounds;

            if (minPosition.x > childBounds.min.x)
                minPosition.x = childBounds.min.x;

            if (minPosition.z > childBounds.min.z)
                minPosition.z = childBounds.min.z;

            if (maxPosition.x < childBounds.max.x)
                maxPosition.x = childBounds.max.x;

            if (maxPosition.z < childBounds.max.z)
                maxPosition.z = childBounds.max.z;
        }

        /*
        GameObject min = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        min.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        min.transform.position = minPosition;


        GameObject max = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        max.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        max.transform.position = maxPosition;
        */

        float horizontalDistance = Vector3.Distance(new Vector3(minPosition.x, 0.0f, 0.0f), new Vector3(maxPosition.x, 0.0f, 0.0f));
        //print("minPosition: " + minPosition);
        float verticalDistance = Vector3.Distance(new Vector3(0.0f, 0.0f, minPosition.z), new Vector3(0.0f, 0.0f, maxPosition.z));
        //print("maxPosition: " + maxPosition);

        //print((minPosition + maxPosition) / 2f + " = (" + minPosition + " + " + maxPosition + ") / 2");

        packageGameObject.transform.position = (minPosition + maxPosition) / 2f;
        packageGameObject.transform.position = new Vector3(packageGameObject.transform.position.x, shiftingFactorYDirection * packageLevel, packageGameObject.transform.position.z);
        //print(shiftingFactorYDirection * packageLevel);
        //print(packageGameObject.name + ", position: " + packageGameObject.transform.position);

        return new Vector3(horizontalDistance, levelHeight, verticalDistance) + packageBorder;
    }

    private void CalculateChildPositions(List<GameObject> childs)
    {
        int sign = 1;
        int sequenceNumber = 0;

        float displacementFactorWidth = FindOutDisplacementFactorWidth(childs);
        float displacementFactorDepth = FindOutDisplacementFactorDepth(childs);

        //print("displacementFactor: " + displacementFactor);

        GameObject prevGameObject = childs[0];
        //prevGameObject.transform.position = new Vector3(0.0f, prevGameObject.transform.position.y, 0.0f);

        if(prevGameObject.GetComponent<Information>().GetSQObjectType().Equals("document"))
            prevGameObject.transform.position = new Vector3(0.0f, prevGameObject.transform.position.y, 0.0f);
        else
            prevGameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        int childIndex = 1;
        for (int listIndex = 0; childIndex < childs.Count; listIndex++)
        {
            if((listIndex + 1) % 2 == 0)
            {
                sign = -sign;
            } else
            {
                sequenceNumber++;
            }

            for(int loopIndexSeqNumber = 0; loopIndexSeqNumber < sequenceNumber; loopIndexSeqNumber++)
            {
                if(childIndex < childs.Count)
                {
                    if (listIndex % 2 == 0)
                    {
                        childs[childIndex].transform.position = prevGameObject.transform.position + new Vector3(sign * displacementFactorWidth, 0.0f, 0.0f);
                        
                        //childs[childIndex].transform.position = prevGameObject.transform.position + new Vector3(sign, 0.0f, 0.0f);
                        //print(childs[childIndex].transform.position + " = " + prevGameObject.transform.position + " + " + new Vector3(sign, 0.0f, 0.0f));
                    } else
                    {
                        childs[childIndex].transform.position = prevGameObject.transform.position + new Vector3(0.0f, 0.0f, sign * displacementFactorDepth);
                        
                        //childs[childIndex].transform.position = prevGameObject.transform.position + new Vector3(0.0f, 0.0f, sign);
                        //print(childs[childIndex].transform.position + " = " + prevGameObject.transform.position + " + " + new Vector3(0.0f, 0.0f, sign));
                    }
                    prevGameObject = childs[childIndex];
                    childIndex++;
                } else
                {
                    return;
                }
            }
        }
    }

    private List<GameObject> FilterDocuments(List<GameObject> childs)
    {
        List<GameObject> documents = new List<GameObject>();
        foreach(GameObject child in childs)
        {
            if (child.GetComponent<Information>().GetSQObjectType().Equals("document"))
                documents.Add(child);
        }

        return documents;
    }

    private List<GameObject> FilterPackages(List<GameObject> childs)
    {
        List<GameObject> packages = new List<GameObject>();
        foreach (GameObject child in childs)
        {
            if (child.GetComponent<Information>().GetSQObjectType().Equals("package"))
                packages.Add(child);
        }

        return packages;
    }

    private float FindOutDisplacementFactorWidth(List<GameObject> childs)
    {
        float displacementFactorWidth = 0.0f;
        float prevDisplacementFactorWidth = 0.0f;
        Vector3 childSize;

        string lastSQType = "";

        foreach (GameObject child in childs)
        {
            childSize = child.GetComponent<Renderer>().bounds.size;

            if (childSize.x >= displacementFactorWidth)
            {
                lastSQType = child.GetComponent<Information>().GetSQObjectType();
                prevDisplacementFactorWidth = displacementFactorWidth;
                displacementFactorWidth = childSize.x;
            }
            else
            {
                if (childSize.z >= prevDisplacementFactorWidth)
                    prevDisplacementFactorWidth = childSize.z;
            }
        }

        if (lastSQType.Equals("package"))
            return (displacementFactorWidth/2) + (prevDisplacementFactorWidth/2) + 0.1f;
        return displacementFactorWidth + 0.1f;
    }

    private float FindOutDisplacementFactorDepth(List<GameObject> childs)
    {
        float displacementFactorDepth = 0.0f;
        float prevDisplacementFactorDepth = 0.0f;

        Vector3 childSize;

        string lastSQType = "";

        foreach (GameObject child in childs)
        {
            childSize = child.GetComponent<Renderer>().bounds.size;

            if (childSize.z >= displacementFactorDepth) {
                lastSQType = child.GetComponent<Information>().GetSQObjectType();
                prevDisplacementFactorDepth = displacementFactorDepth;
                displacementFactorDepth = childSize.z;
            } else
            {
                if(childSize.z >= prevDisplacementFactorDepth)
                    prevDisplacementFactorDepth = childSize.z;
            }
        }

        if (lastSQType.Equals("package"))
            return (displacementFactorDepth / 2) + (prevDisplacementFactorDepth / 2) + 0.1f;
        return displacementFactorDepth + 0.1f;
    }

    private float CalculateDistance(Vector3 startPoint, Vector3 endPoint)
    {
        return Vector3.Distance(startPoint, endPoint);
    }

    private void DeleteHelperGameObjects(List<GameObject> helperGameObjects)
    {
        List<GameObject> childs;
        foreach (GameObject helperGameObject in helperGameObjects)
        {
            childs = new List<GameObject>();
            foreach (Transform child in helperGameObject.transform)
            {
                childs.Add(child.gameObject);
            }

            foreach(GameObject child in childs)
            {
                child.transform.SetParent(helperGameObject.transform.parent);
            }

            Destroy(helperGameObject);
        }
    }
}
