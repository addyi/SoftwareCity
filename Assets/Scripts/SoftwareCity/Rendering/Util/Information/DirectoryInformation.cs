
using DataModel;
using DataModel.ProjectTree.Components;
using System.Collections.Generic;

namespace SoftwareCity.Rendering.Utils.Information
{
    public class DirectoryInformation : BaseInformation {

        private string childString = "";

        /// <summary>
        /// Set the specific values.
        /// </summary>
        /// <param name="treeComponent"></param>
        public override void UpdateValues(TreeComponent treeComponent)
        {
            base.UpdateValues(treeComponent);
            
            foreach(TreeComponent component in ((DirComponent)treeComponent).components)
            {
                childString += "   " + ((qualifier == SqQualifier.UNDEFINED) ? SqQualifier.DIRECTORY : qualifier) + ": <b>" + component.Name + "</b>\n";
            }
        }

        /// <summary>
        /// To format the directory information as string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return 
                "<b>Childs:</b> <size=40pt>\n" + this.childString + 
                "</size>";
        }
    }
}

