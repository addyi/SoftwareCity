using DataModel;
using DataModel.Metrics;
using DataModel.ProjectTree.Components;
using SoftwareCity.Rendering.Utils.Colorizer;
using SoftwareCity.Rendering.Utils.Information;
using UnityEngine;
using UnityEngine.Rendering;

namespace SoftwareCity.Rendering.Utils {
    public class ComponentProducer : MonoBehaviour {


        private readonly string linesOfCode = "ncloc";
        private readonly string testSuccess = "test_success_density";
        private readonly string functions = "functions";
        private readonly string commentLinesDensity = "comment_lines_density";

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
            documentGameObject.GetComponent<MeshFilter>().mesh = CalculatePyramid(FindSpecificMetricValue(testSuccess, documentComponent));
            documentGameObject.GetComponent<Renderer>().material = contentMaterial;
            documentGameObject.GetComponent<Renderer>().material.color = GetComponent<DocumentColorizer>().DocumentColor(FindSpecificMetricValue(functions, documentComponent));
            documentGameObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
            documentGameObject.GetComponent<Renderer>().lightProbeUsage = LightProbeUsage.Off;
            documentGameObject.GetComponent<Renderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
            documentGameObject.GetComponent<Renderer>().receiveShadows = false;
            documentGameObject.GetComponent<Collider>().enabled = false;
            documentGameObject.GetComponent<Renderer>().enabled = false;
            documentGameObject.transform.position = Vector3.zero;
            documentGameObject.name = documentComponent.Name;

            documentGameObject.transform.localScale = CalculateDocumentSize(FindSpecificMetricValue(linesOfCode, documentComponent)/100.0f, FindSpecificMetricValue(commentLinesDensity, documentComponent) / 100.0f);

            return documentGameObject;
        }
        
        /// <summary>
        /// Calculate the positions of the pyramid corners at the top.
        /// </summary>
        /// <param name="documentGameObject"></param>
        private Mesh CalculatePyramid(float percent)
        {
            return this.gameObject.GetComponent<CustomMeshGenerator>().GeneratePyramid(percent / 100.0f);
        }

        /// <summary>
        /// Calculate the specific size depend on the the metric.
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateDocumentSize(float widthDepth, float height)
        {
            return new Vector3(0.1f + widthDepth, 1f + height, 0.1f + widthDepth);
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

        private float FindSpecificMetricValue(string key, TreeComponent documentComponent)
        {
            foreach (TreeMetric m in documentComponent.Metrics)
            {
                if (m.Key.Equals(key))
                {
                    return (float)m.Value;
                }
            }
            return 0.0f;
        }
    }
}

