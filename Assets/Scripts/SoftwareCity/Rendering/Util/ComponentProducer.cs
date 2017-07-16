using DataModel;
using DataModel.ProjectTree.Components;
using SoftwareCity.Rendering.Utils.Information;
using UnityEngine;
using UnityEngine.Rendering;

namespace SoftwareCity.Rendering.Utils {
    public class ComponentProducer : MonoBehaviour {

        /// <summary>
        /// IMPLEMENT !!!!!!!
        /// </summary>
        private static readonly Vector3 localScaleOfDocument = new Vector3(0.2f, 1f, 0.2f);

        [SerializeField]
        private GameObject documentPrefab;

        [SerializeField]
        private Material contentMaterial;

        /// <summary>
        /// Create a new document gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GenerateDocument(TreeComponent documentComponent)
        {
            GameObject documentGameObject = Instantiate(documentPrefab) as GameObject;
            documentGameObject.AddComponent<FileInformation>();
            documentGameObject.GetComponent<FileInformation>().UpdateValues(documentComponent);
            documentGameObject.AddComponent<ComponentClickListener>();
            documentGameObject.GetComponent<MeshFilter>().mesh = CalculatePyramid();
            documentGameObject.GetComponent<Renderer>().sharedMaterial = contentMaterial;
            documentGameObject.GetComponent<Renderer>().sharedMaterial.color = Color.red;
            documentGameObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
            documentGameObject.GetComponent<Renderer>().lightProbeUsage = LightProbeUsage.Off;
            documentGameObject.GetComponent<Renderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
            documentGameObject.GetComponent<Renderer>().receiveShadows = false;
            documentGameObject.GetComponent<Collider>().enabled = false;
            documentGameObject.GetComponent<Renderer>().enabled = false;
            documentGameObject.transform.position = Vector3.zero;
            documentGameObject.name = documentComponent.Name;

            documentGameObject.transform.localScale = CalculateDocumentSize();

            return documentGameObject;
        }
        
        /// <summary>
        /// Calculate the positions of the pyramid corners at the top.
        /// </summary>
        /// <param name="documentGameObject"></param>
        private Mesh CalculatePyramid()
        {
            float percent = Random.Range(0.0f, 1.0f);

            return this.gameObject.GetComponent<CustomMeshGenerator>().GeneratePyramid(percent);
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
        public GameObject GeneratePackage(TreeComponent packageComponent)
        {
            GameObject packageGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            packageGameObject.AddComponent<DirectoryInformation>();
            packageGameObject.GetComponent<DirectoryInformation>().UpdateValues(packageComponent);
            packageGameObject.AddComponent<ComponentClickListener>();
            packageGameObject.GetComponentInChildren<Renderer>().sharedMaterial = contentMaterial;
            packageGameObject.GetComponentInChildren<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
            packageGameObject.GetComponentInChildren<Renderer>().lightProbeUsage = LightProbeUsage.Off;
            packageGameObject.GetComponentInChildren<Renderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
            packageGameObject.GetComponentInChildren<Renderer>().receiveShadows = false;
            packageGameObject.GetComponent<Collider>().enabled = false;
            packageGameObject.GetComponent<Renderer>().enabled = false;
            packageGameObject.name = packageComponent.Name;

            return packageGameObject;
        }

        /// <summary>
        /// Create a new package gameobject without informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GenerateHelper()
        {
            GameObject helperGameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            helperGameobject.AddComponent<BaseInformation>();
            helperGameobject.name = "Helper";

            return helperGameobject;
        }
    }
}

