using UnityEngine;

namespace SoftwareCity.Rendering.Utils {
    public class ComponentProducer : MonoBehaviour {

        /// <summary>
        /// IMPLEMENT !!!!!!!
        /// </summary>
        private static readonly Vector3 localScaleOfDocument = new Vector3(0.2f, 1f, 0.2f);

        /// <summary>
        /// Create a new document gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public static GameObject GenerateDocument()
        {
            GameObject documentGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            documentGameObject.AddComponent<Information>();
            documentGameObject.GetComponent<Information>().SetSQObjectType("document");
            documentGameObject.GetComponent<Renderer>().material.color = Color.red;
            documentGameObject.GetComponent<Collider>().enabled = false;
            documentGameObject.GetComponent<Renderer>().enabled = false;
            documentGameObject.name = "Document";

            documentGameObject.transform.localScale = CalculateDocumentSize();

            return documentGameObject;
        }
        
        /// <summary>
        /// Calculate the specific size depend on the the metric.
        /// </summary>
        /// <returns></returns>
        private static Vector3 CalculateDocumentSize()
        {
            float widthHeight = Random.Range(0.1f, 1.0f);
            return new Vector3(widthHeight, Random.Range(0.1f, 2.0f), widthHeight);
        }

        /// <summary>
        /// Create a new package gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public static GameObject GeneratePackage()
        {
            GameObject packageGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            packageGameObject.AddComponent<Information>();
            packageGameObject.GetComponent<Information>().SetSQObjectType("package");
            packageGameObject.GetComponent<Collider>().enabled = false;
            packageGameObject.GetComponent<Renderer>().enabled = false;
            packageGameObject.name = "Package";

            return packageGameObject;
        }

        /// <summary>
        /// Create a new package gameobject without informations.
        /// </summary>
        /// <returns></returns>
        public static GameObject GenerateHelper()
        {
            GameObject helperGameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            helperGameobject.AddComponent<Information>();
            helperGameobject.GetComponent<Information>().SetSQObjectType("package");
            helperGameobject.name = "Helper";

            return helperGameobject;
        }
    }
}

