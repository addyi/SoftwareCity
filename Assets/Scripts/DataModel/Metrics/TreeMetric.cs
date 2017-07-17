

namespace DataModel.Metrics
{
    public class TreeMetric
    {
        public readonly string Key;
        public readonly double Value;

        public TreeMetric(string Key, double Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public override string ToString()
        {
            return string.Format("(TreeMetric Key={0}, Value={1})", Key, Value);
        }
    }
}
