using System.Collections.Generic;
using UnityEngine;
using SoftwareCity.Envelope.Dimension;
using SoftwareCity.Rendering.Utils;
using DataModel.ProjectTree.Components;

namespace SoftwareCity.Rendering
{
    public class SoftwareCityBuilder : MonoBehaviour
    {
        /// <summary>
        /// Border of the packages.
        /// </summary>
        private readonly Vector3 packageBorder = new Vector3(0.2f, 0.0f, 0.2f);

        /// <summary>
        /// Save a list with gameobject helpers for generating
        /// </summary>
        private List<GameObject> helperGameObjects = new List<GameObject>();

        /// <summary>
        /// Height of a package.
        /// </summary>
        private float levelHeight = 0.1f;

        /// <summary>
        /// The current package level.
        /// </summary>
        private int packageLevel;

        /// <summary>
        /// Shifting factor to place the packages.
        /// </summary>
        private float shiftingFactorYDirection = -0.01f;

        /// <summary>
        /// Save the height of the tallest building.
        /// </summary>
        private float maxDocumentHeight;

        /// <summary>
        /// Save the PackageColorizer reference.
        /// </summary>
        private PackageColorizer packageColorizer;

        /// <summary>
        /// Save the ComponentProducer reference.
        /// </summary>
        private ComponentProducer componentProducer;

        private GameObject envelope;

        private void Start()
        {
            Build(GameObject.FindGameObjectWithTag("InitialGameObject").GetComponent<Initial>().rootProjectComponent);
        }

        /// <summary>
        /// Method to build a new software city.
        /// </summary>
        /// <param name="root"></param>
        public void Build(ProjectComponent root)
        {
            packageLevel = 1;
            maxDocumentHeight = 0.0f;

            packageColorizer = GetComponent<PackageColorizer>();
            componentProducer = GetComponent<ComponentProducer>();
            envelope = GameObject.FindGameObjectWithTag("Envelope");

            GameObject rootGameObject = TraverseTree(root, packageLevel);

            DeleteHelperGameObjects(helperGameObjects);

            AddCityToEnvelope(rootGameObject);

            TreeToLinearStructur(rootGameObject);
        }

        /// <summary>
        /// Method to traverse the object tree to building the city.
        /// </summary>
        /// <param name="treeObject"></param>
        /// <param name="packageLevel"></param>
        /// <returns></returns>
        private GameObject TraverseTree(TreeComponent treeObject, int packageLevel)
        {
            if (treeObject is DirComponent)
            {
                List<GameObject> childs = new List<GameObject>();

                foreach (TreeComponent child in ((DirComponent)treeObject).components)
                {
                    childs.Add(TraverseTree(child, packageLevel + 1));
                }

                List<GameObject> childDocuments = ComponentFilter.FilterDocuments(childs);
                List<GameObject> childPackages = ComponentFilter.FilterPackages(childs);
                
                if(childPackages.Count > 0 && childDocuments.Count > 0)
                {
                    //ComponentLayout.Helix(childDocuments);
                    ComponentLayout.Corner(childDocuments);

                    GameObject helper = componentProducer.GenerateHelper();

                    helperGameObjects.Add(helper);

                    helper.transform.localScale = CalculatePackageSize(childDocuments, helper);
                    SetPackageGameObjectAsParent(helper, childDocuments);

                    childPackages.Add(helper);
                    ComponentLayout.Helix(childPackages);
                }
                else
                {
                    if(childDocuments.Count > 0 && childPackages.Count == 0)
                    {
                        ComponentLayout.Corner(childDocuments);
                    } else
                    {
                        if (childDocuments.Count == 0 && childPackages.Count > 0) {
                            ComponentLayout.Helix(childPackages);
                        }

                    }
                        //ComponentLayout.Helix(childs);
                }

                GameObject packageGameObject = componentProducer.GeneratePackage();
                packageGameObject.GetComponent<Information>().SetChilds(childs);

                packageGameObject.GetComponent<Renderer>().material.color = packageColorizer.PackageLevelColor(packageLevel);

                if (childPackages.Count > 0)
                {
                    packageGameObject.transform.localScale = CalculatePackageSize(childPackages, packageGameObject);
                    SetPackageGameObjectAsParent(packageGameObject, childPackages);
                }
                else
                {
                    packageGameObject.transform.localScale = CalculatePackageSize(childs, packageGameObject);
                    SetPackageGameObjectAsParent(packageGameObject, childs);
                }

                return packageGameObject;
            }
            else
            {
                GameObject documentGameObject = componentProducer.GenerateDocument();

                if (documentGameObject.GetComponentInChildren<Renderer>().bounds.size.y > maxDocumentHeight)
                    maxDocumentHeight = documentGameObject.GetComponentInChildren<Renderer>().bounds.size.y;

                return documentGameObject;
            }
        }

        /// <summary>
        /// Set the correct local position of the specific space in y direction.
        /// </summary>
        /// <param name="childsDocuments"></param>
        private void SetDocumentYPosition(List<GameObject> childsDocuments)
        {
            foreach(GameObject childDocument in childsDocuments)
            {
                childDocument.transform.localPosition = new Vector3(childDocument.transform.localPosition.x, childDocument.transform.localScale.y / 2, childDocument.transform.localPosition.z);
            }
        }

        /// <summary>
        /// Method to add gameobjects as childs.
        /// </summary>
        /// <param name="packageGameObject"></param>
        /// <param name="childs"></param>
        private void SetPackageGameObjectAsParent(GameObject packageGameObject, List<GameObject> childs)
        {
            foreach (GameObject child in childs)
            {
                child.transform.SetParent(packageGameObject.transform);
            }
        }

        /// <summary>
        /// Calculate the package size.
        /// </summary>
        /// <param name="childs"></param>
        /// <param name="packageGameObject"></param>
        /// <returns></returns>
        private Vector3 CalculatePackageSize(List<GameObject> childs, GameObject packageGameObject)
        {
            Vector3 minPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 maxPosition = new Vector3(0.0f, 0.0f, 0.0f);

            foreach (GameObject child in childs)
            {
                Bounds childBounds = child.GetComponentInChildren<Renderer>().bounds;

                if (minPosition.x > childBounds.min.x)
                    minPosition.x = childBounds.min.x;

                if (minPosition.z > childBounds.min.z)
                    minPosition.z = childBounds.min.z;

                if (maxPosition.x < childBounds.max.x)
                    maxPosition.x = childBounds.max.x;

                if (maxPosition.z < childBounds.max.z)
                    maxPosition.z = childBounds.max.z;
            }
            float horizontalDistance = Vector3.Distance(new Vector3(minPosition.x, 0.0f, 0.0f), new Vector3(maxPosition.x, 0.0f, 0.0f));

            float verticalDistance = Vector3.Distance(new Vector3(0.0f, 0.0f, minPosition.z), new Vector3(0.0f, 0.0f, maxPosition.z));

            packageGameObject.transform.position = (minPosition + maxPosition) * 0.5f;
            packageGameObject.transform.position = new Vector3(packageGameObject.transform.position.x, shiftingFactorYDirection * packageLevel, packageGameObject.transform.position.z);

            return new Vector3(horizontalDistance, levelHeight, verticalDistance) + packageBorder;
        }

        /// <summary>
        /// Delete all gameobject helpers.
        /// </summary>
        /// <param name="helperGameObjects"></param>
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

                foreach (GameObject child in childs)
                {
                    child.transform.SetParent(helperGameObject.transform.parent);
                }

                Destroy(helperGameObject);
            }
        }

        /// <summary>
        /// Add the generated city to the Envelope gameobject.
        /// </summary>
        /// <param name="root"></param>
        private void AddCityToEnvelope(GameObject root)
        {
            this.gameObject.transform.parent.localScale = new Vector3(root.transform.localScale.x * 0.03f, (maxDocumentHeight + 0.1f) * 0.03f, root.transform.localScale.z * 0.03f);

            root.transform.SetParent(this.gameObject.transform);
            root.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * 0.03f, this.gameObject.transform.localScale.y * 0.03f, this.gameObject.transform.localScale.z * 0.03f);

            this.gameObject.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);

            this.gameObject.transform.parent.GetComponent<EnvelopeDimension>().UpdateDimensionPoints();
        }

        public float GetHeight()
        {
            return maxDocumentHeight;
        }

        private void TreeToLinearStructur(GameObject treeNode)
        {
            treeNode.transform.SetParent(envelope.transform);

            if(treeNode.GetComponent<Information>().GetChilds() == null)
            {
                return;
            }

            foreach (GameObject child in treeNode.GetComponent<Information>().GetChilds())
            {
                TreeToLinearStructur(child);
            }
        }
    }
}