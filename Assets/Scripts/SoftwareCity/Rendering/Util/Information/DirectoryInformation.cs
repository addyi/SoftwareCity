
using DataModel.ProjectTree.Components;
using System.Collections.Generic;

namespace SoftwareCity.Rendering.Utils.Information
{
    public class DirectoryInformation : BaseInformation {

        private string childString = "";

        public override void UpdateValues(TreeComponent treeComponent)
        {
            base.UpdateValues(treeComponent);
            
            foreach(TreeComponent component in ((DirComponent)treeComponent).components)
            {
                childString += "   <b>Name:</b> " + component.Name + 
                    ", <b>Qualifier:</b> " + component.Qualifier + 
                    "\n";
            }
        }

        public override string ToString()
        {
            return "<b>Type:</b> <size=40pt>" + qualifier +
                "</size>\n<b>Id:</b> <size=40pt>" + this.id +
                "</size>\n<b>Key:</b> <size=40pt>" + this.key +
                "</size>\n<b>Name:</b> <size=40pt>" + this.componentName +
                "</size>\n<b>Childs:</b> <size=40pt>\n" + this.childString + 
                "</size>";
        }
    }
}

