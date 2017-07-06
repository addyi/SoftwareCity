using System.Collections.Generic;
using System;

namespace Webservice.Response.ComponentTree
{
    [Serializable]
    public class Component : IComparable
    {
        public string id;
        public string key;
        public string name;
        public string qualifier;
        public string path;
        public string language;
        public List<Measure> measures;

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentException("Illegal Argument: null Objecto");
            if (!(obj is Component))
                throw new ArgumentException("Illegal Argument: obj isn't a Component");

            Component other = (Component)obj;

            int qualifierComparison = CompareQualifier(other.qualifier);
            if (qualifierComparison != 0)
                return qualifierComparison;
            int pathDepthComparison = ComparePathDepth(other.path);
            if (pathDepthComparison != 0)
                return pathDepthComparison;

            return path.CompareTo(other.path);
        }

        private int ComparePathDepth(string path)
        {
            int ThisPathDepth = this.path.Split('/').Length;
            int OtherPathDepth = path.Split('/').Length;
            return ThisPathDepth.CompareTo(OtherPathDepth);
        }

        private int CompareQualifier(string qualifier)
        {
            int ThisQualifier = QualifierToInt(this.qualifier);
            int OtherQualifier = QualifierToInt(qualifier);
            return ThisQualifier.CompareTo(OtherQualifier);
        }

        private static int QualifierToInt(string qualifier)
        {
            switch (qualifier)
            {
                case "BRC": return 1;
                case "DIR": return 2;
                case "FIL": return 3;
                case "TRK": return 0;
                case "UTS": return 4;
                default: throw new ArgumentException("Unknown Argument for Qualifier: \"" + qualifier + "\"");
            }
        }

        public override string ToString()
        {
            return string.Format("(Component: id={0}, key={1}, name={2}, qualifier={3}, path={4}, language={5}, measures={6})", id, key, name, qualifier, path, language, measures);
        }

    }
}
