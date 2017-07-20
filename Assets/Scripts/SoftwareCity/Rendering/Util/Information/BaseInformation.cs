using DataModel;
using DataModel.Metrics;
using DataModel.ProjectTree.Components;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Information
{
    public class BaseInformation : MonoBehaviour {

        /// <summary>
        /// Save the id.
        /// </summary>
        protected string id;

        /// <summary>
        /// Save the key.
        /// </summary>
        protected string key;

        /// <summary>
        /// save the name of the component.
        /// </summary>
        protected string componentName;

        /// <summary>
        /// Save the qualifier.
        /// </summary>
        protected SqQualifier qualifier;

        /// <summary>
        /// Save the metrics of these component.
        /// </summary>
        protected List<TreeMetric> metrics;

        /// <summary>
        /// List with children gameobjects.
        /// </summary>
        [SerializeField]
        protected List<GameObject> childs;

        /// <summary>
        /// Set all specific values.
        /// </summary>
        /// <param name="treeComponent"></param>
        public virtual void UpdateValues(TreeComponent treeComponent)
        {
            this.id = treeComponent.ID;
            this.key = treeComponent.Key;
            this.componentName = treeComponent.Name;
            this.qualifier = treeComponent.Qualifier;
            this.metrics = treeComponent.Metrics;
        }

        /// <summary>
        /// Get current qualifier.
        /// </summary>
        /// <returns></returns>
        public SqQualifier GetQualifier()
        {
            return qualifier;
        }

        /// <summary>
        /// Set childs.
        /// </summary>
        /// <param name="childs"></param>
        public void SetChilds(List<GameObject> childs)
        {
            this.childs = childs;
        }

        /// <summary>
        /// Get all childs.
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetChilds()
        {
            return childs;
        }
    }
}

