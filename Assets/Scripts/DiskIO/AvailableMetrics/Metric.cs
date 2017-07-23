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
        public float defaultValue;

        /// <summary>
        /// Gets or sets the datatype.
        /// </summary>
        public string datatype;

        public Metric(string n, string k, float dv, string dt)
        {
            this.name = n;
            this.key = k;
            this.defaultValue = dv;
            this.datatype = dt;
        }

        public override bool Equals(object obj)
        {
            Metric metric = obj as Metric;
            return this.name == metric.name &&
                   this.key == metric.key &&
                   this.datatype == metric.datatype &&
                   this.defaultValue == metric.defaultValue;
        }


    }
}
