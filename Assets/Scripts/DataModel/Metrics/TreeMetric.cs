using System;

namespace DataModel.Metrics
{
    /// <summary>
    /// POCO for SonarQube metrics inside the tree
    /// </summary>
    [Serializable]
    public class TreeMetric : IComparable
    {
        /// <summary>
        /// Metirc Name
        /// </summary>
        public string Name;
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
        /// <param name="Name">Metric Name</param>
        /// <param name="Key">Metric Key</param>
        /// <param name="Value">Metric Value</param>
        public TreeMetric(string Name, string Key, float Value)
        {
            this.Name = Name;
            this.Key = Key;
            this.Value = Value;
        }

        public int CompareTo(object obj)
        {
            if (this == obj)
            {
                return 0;
            }
            if (!(this is TreeMetric))
            {
                return -1;
            }
            return this.Name.CompareTo(((TreeMetric)obj).Name);
        }

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(TreeMetric Name={0}, Key={1}, Value={2})", Name, Key, Value);
        }
    }
}
