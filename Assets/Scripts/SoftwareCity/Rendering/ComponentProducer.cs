using UnityEngine;

public class ComponentProducer : MonoBehaviour {

    /// <summary>
    /// IMPLEMENT !!!!!!!
    /// </summary>
    private static readonly Vector3 localScaleOfDocument = new Vector3(0.2f, 0.3f, 0.2f);

    public static GameObject GenerateDocument()
    {
        GameObject documentGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        documentGameObject.AddComponent<Information>();
        documentGameObject.GetComponent<Information>().SetSQObjectType("document");
        documentGameObject.GetComponent<Renderer>().material.color = Color.red;
        documentGameObject.GetComponent<Collider>().enabled = false;
        documentGameObject.GetComponent<Renderer>().enabled = false;
        documentGameObject.transform.localScale = localScaleOfDocument;
        documentGameObject.transform.position = new Vector3(documentGameObject.transform.position.x, documentGameObject.GetComponent<Renderer>().bounds.size.y / 2, documentGameObject.transform.position.z);

        return documentGameObject;
    }

    public static GameObject GeneratePackage()
    {
        GameObject packageGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        packageGameObject.AddComponent<Information>();
        packageGameObject.GetComponent<Information>().SetSQObjectType("package");
        packageGameObject.GetComponent<Collider>().enabled = false;
        packageGameObject.GetComponent<Renderer>().enabled = false;

        return packageGameObject;
    }

    public static GameObject GenerateHelper()
    {
        GameObject helperGameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        helperGameobject.AddComponent<Information>();
        helperGameobject.GetComponent<Information>().SetSQObjectType("package");

        return helperGameobject;
    }
}
