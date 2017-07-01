

namespace DataModel.ProjectTree
{
    class Metric
    {
        public readonly string Name;
        public readonly string Key;
        public readonly double DefaultValue;
        public readonly string DataType;
        public readonly double Value;
        public readonly bool IsDefaultValue;




        public Metric(string Name, string Key,double DefaultValue,string DataType,double Value,bool IsDefaultValue)
        {
            this.Name = Name;
            this.Key = Key;
            this.DefaultValue = DefaultValue;
            this.DataType = DataType;
            this.Value = Value;
            this.IsDefaultValue = IsDefaultValue;
        }


    }


}
