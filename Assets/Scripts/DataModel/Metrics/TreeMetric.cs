using System;

namespace DataModel.Metrics
{
    /// <summary>
    /// POCO for SonarQube metrics inside the tree
    /// </summary>
    [Serializable]
    public class TreeMetric
    {
        /// <summary>
        /// Metric Key
        /// </summary>
        public string Key;
        /// <summary>
        /// Metric Value
        /// </summary>
        public float Value;

        /// <summary>
        /// Default constructor, needed because of the Serialization to disk
        /// </summary>
        public TreeMetric() { }

        /// <summary>
        /// It's a constructor
        /// </summary>
        /// <param name="Key">Metric Key</param>
        /// <param name="Value">Metric Value</param>
        public TreeMetric(string Key, float Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(TreeMetric Key={0}, Value={1})", Key, Value);
        }
    }
}
