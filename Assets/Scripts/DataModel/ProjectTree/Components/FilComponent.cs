using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    class FilComponent : TreeLeafComponent
    {
        public FilComponent(Component component)
        {
            ID = component.id;
            Key = component.key;
            Name = component.name;
            Path = component.path;
            Language = component.language;
            Qualifier = QualifierForString(component.qualifier);
            // TODO ADDYI  this.Metrics = component.measures;
        }

        public override TreeComponent UpdateComponent(TreeComponent component)
        {
            if (component is FilComponent && Name == component.Name)
            {
                FilComponent f = (FilComponent)component;
                this.ID = f.ID;
                this.Key = f.Key;
                this.Name = f.Name;
                this.Path = f.Path;
                this.Qualifier = f.Qualifier;
                // TODO ADDYI this.Metrics = f.Metrics;
                this.Language = f.Language;
                return this;
            }
            return null;
        }
    }
}
