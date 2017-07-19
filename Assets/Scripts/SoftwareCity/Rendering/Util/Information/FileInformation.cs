using DataModel.Metrics;
using DataModel.ProjectTree.Components;

namespace SoftwareCity.Rendering.Utils.Information
{
    public class FileInformation : BaseInformation {

        private string language;

        public override void UpdateValues(TreeComponent treeComponent)
        {
            base.UpdateValues(treeComponent);
            this.language = ((TreeLeafComponent)treeComponent).Language;
        }

        public override string ToString()
        {
            string metricString = "";
            foreach (TreeMetric metric in metrics)
            {
                metricString += "   " + metric.Key + ": " + metric.Value + "\n";
            }

            return "<b>Type:</b> <size=40pt>" + qualifier + 
                "</size>\n<b>Id:</b> <size=40pt>" + this.id + 
                "</size>\n<b>Key:</b> <size=40pt>" + this.key + 
                "</size>\n<b>Name:</b> <size=40pt>" + this.componentName + 
                "</size>\n<b>Language:</b> <size=40pt>" + this.language + 
                "</size>\n<b>Metrics:</b> <size=40pt>\n" + metricString + 
                "</size>";
        }
    }
}

