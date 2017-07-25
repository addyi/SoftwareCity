using DataModel.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    [Serializable]
    public abstract class TreeLeafComponent : TreeComponent
    {
        public string Language;

        public TreeLeafComponent() { }

        protected TreeLeafComponent(SqComponent component) : base(component)
        {
            Language = component.language;
        }

        public override TreeComponent InsertComponentAt(string[] path, TreeComponent component)
        {
            if (component == null || path == null || path.Length <= 0)
                return null;
            if (path.Length == 0 && Name == component.Name)
                return UpdateComponent(component);
            return null;
        }

        public override float GetMaxForMetric(string MetricKey)
        {
            foreach (TreeMetric tm in Metrics)
            {
                if (tm.Key == MetricKey)
                {
                    return tm.Value;
                }
            }
            return 0;
        }
    }
}
