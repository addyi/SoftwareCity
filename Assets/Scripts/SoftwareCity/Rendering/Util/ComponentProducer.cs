using UnityEngine;

namespace SoftwareCity.Rendering.Utils {
    public class ComponentProducer : MonoBehaviour {

        /// <summary>
        /// IMPLEMENT !!!!!!!
        /// </summary>
        private static readonly Vector3 localScaleOfDocument = new Vector3(0.2f, 2f, 0.2f);

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
            documentGameObject.transform.localScale = localScaleOfDocument;
            documentGameObject.transform.position = new Vector3(
                documentGameObject.transform.position.x, 
                documentGameObject.GetComponent<Renderer>().bounds.size.y * 0.5f, 
                documentGameObject.transform.position.z);

            return documentGameObject;
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

            return helperGameobject;
        }
    }
}

