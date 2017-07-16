using DataModel;
using DataModel.Metrics;
using DataModel.ProjectTree.Components;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Information
{
    public class BaseInformation : MonoBehaviour {

        protected string id;

        protected string key;

        protected string componentName;

        protected SqQualifier qualifier;

        protected List<TreeMetric> metrics;

        [SerializeField]
        protected List<GameObject> childs;

        public virtual void UpdateValues(TreeComponent treeComponent)
        {
            this.id = treeComponent.ID;
            this.key = treeComponent.Key;
            this.componentName = treeComponent.Name;
            this.qualifier = treeComponent.Qualifier;
            this.metrics = treeComponent.Metrics;
        }

        public SqQualifier GetQualifier()
        {
            return qualifier;
        }

        public void SetChilds(List<GameObject> childs)
        {
            this.childs = childs;
        }

        public List<GameObject> GetChilds()
        {
            return childs;
        }
    }
}

