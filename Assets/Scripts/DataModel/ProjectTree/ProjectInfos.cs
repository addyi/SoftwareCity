using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree
{
    class ProjectInfos
    {
        public enum Qualifier { BRC, DIR, FIL, TRK, UTS }

        public readonly string ID;
        public readonly string Key;
        public readonly string Name;
        public readonly Qualifier ComponentType;
        //TODO ADDYI public List<Metric> measures;

        public ProjectInfos(BaseComponent baseComponent)
        {
            this.ID = baseComponent.id;
            this.Key = baseComponent.key;
            this.Name = baseComponent.name;
            this.ComponentType = QualifierForString(baseComponent.qualifier);
        }

        private Qualifier QualifierForString(string qualifier)
        {
            switch (qualifier)
            {
                case "BRC": return Qualifier.BRC;
                case "DIR": return Qualifier.DIR;
                case "FIL": return Qualifier.FIL;
                case "TRK": return Qualifier.TRK;
                case "UTS": return Qualifier.UTS;
                default: throw new ArgumentException("Unknown Argument for Qualifier: \"" + qualifier + "\"");

            }
        }
        
    }
}
