using System;
namespace DiskIO.AvailableMetrics
{
    [Serializable]
    public class Metric
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string name;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string key;

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public double defaultvalue;

        /// <summary>
        /// Gets or sets the datatype.
        /// </summary>
        public string datatype;


    }
}
