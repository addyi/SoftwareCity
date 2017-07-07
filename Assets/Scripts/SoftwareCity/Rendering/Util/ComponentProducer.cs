using UnityEngine;

namespace SoftwareCity.Rendering.Utils {
    public class ComponentProducer : MonoBehaviour {

        /// <summary>
        /// IMPLEMENT !!!!!!!
        /// </summary>
        private static readonly Vector3 localScaleOfDocument = new Vector3(0.2f, 1f, 0.2f);

        [SerializeField]
        private GameObject documentPrefab;

        /// <summary>
        /// Create a new document gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GenerateDocument()
        {
            //GameObject documentGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject documentGameObject = Instantiate(documentPrefab) as GameObject;
            documentGameObject.AddComponent<Information>();
            documentGameObject.GetComponent<Information>().SetSQObjectType("document");
            documentGameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            documentGameObject.GetComponentInChildren<Collider>().enabled = false;
            documentGameObject.GetComponentInChildren<Renderer>().enabled = false;
            documentGameObject.transform.position = Vector3.zero;
            documentGameObject.name = "Document";

            CalculatePyramid(documentGameObject);

            documentGameObject.transform.localScale = CalculateDocumentSize();

            return documentGameObject;
        }
        
        /// <summary>
        /// Calculate the positions of the pyramid corners at the top.
        /// </summary>
        /// <param name="documentGameObject"></param>
        private void CalculatePyramid(GameObject documentGameObject)
        {
            DocumentPyramidInformation documentPyramidInformation = documentGameObject.GetComponent<DocumentPyramidInformation>();

            float percent = Random.Range(0.0f, 1.0f);

            documentPyramidInformation.SetPosition(percent);
        }

        /// <summary>
        /// Calculate the specific size depend on the the metric.
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateDocumentSize()
        {
            float widthHeight = Random.Range(0.1f, 1.0f);
            return new Vector3(widthHeight, Random.Range(0.1f, 2.0f), widthHeight);
        }

        /// <summary>
        /// Create a new package gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GeneratePackage()
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
        public GameObject GenerateHelper()
        {
            GameObject helperGameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            helperGameobject.AddComponent<Information>();
            helperGameobject.GetComponent<Information>().SetSQObjectType("package");
            helperGameobject.name = "Helper";

            return helperGameobject;
        }
    }
}

