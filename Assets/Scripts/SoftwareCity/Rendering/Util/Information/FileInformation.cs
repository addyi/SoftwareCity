using DataModel.Metrics;
using DataModel.ProjectTree.Components;

namespace SoftwareCity.Rendering.Utils.Information
{
    public class FileInformation : BaseInformation {

        /// <summary>
        /// Save the language.
        /// </summary>
        private string language;

        /// <summary>
        /// Update values.
        /// </summary>
        /// <param name="treeComponent"></param>
        public override void UpdateValues(TreeComponent treeComponent)
        {
            base.UpdateValues(treeComponent);
            this.language = ((TreeLeafComponent)treeComponent).Language;
        }

        /// <summary>
        /// Print the file informations in a specific format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string metricString = "";
            foreach (TreeMetric metric in metrics)
            {
                metricString += "   " + metric.Key + ": <b>" + metric.Value + "</b>\n";
            }

            return
                "<b>Path:</b> <size=40pt>" + this.key +
                "</size>\n<b>Language:</b> <size=40pt>" + this.language + 
                "</size>\n<b>Metrics:</b> <size=40pt>\n" + metricString + 
                "</size>";
        }
    }
}

