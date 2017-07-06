using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.ProjectTree.Components
{
    abstract class TreeComponent : IComparable
    {
        // TODO ADDYI fucking toString methode 
        public string ID;
        public string Key;
        public string Name;
        public string Path;
        public SqQualifier Qualifier;
        // TODO ADDYI  public List<Metric> Metrics;

        public abstract TreeComponent InsertComponentAt(string[] path, TreeComponent component);
        public abstract TreeComponent UpdateComponent(TreeComponent component);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("Illegal Argument: null Object");
            }
            if (!(obj is TreeComponent))
            {
                throw new ArgumentException("Illegal Argument: obj isn't a TreeComponent");
            }
            TreeComponent other = (TreeComponent)obj;
            return ID == other.ID && Key == other.Key && Name == other.Name
                && Path == other.Path && Qualifier == other.Qualifier;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("Illegal Argument: null Object");
            }
            if (!(obj is TreeComponent))
            {
                throw new ArgumentException("Illegal Argument: obj isn't a TreeComponent");
            }

            TreeComponent other = (TreeComponent)obj;
            int qualifierComparison = CompareQualifier(other.Qualifier);

            if (qualifierComparison == 0)
            {
                return Path.CompareTo(other.Path);
            }
            else
            {
                return qualifierComparison;
            }
        }

        public override string ToString()
        {
            return Name + "\n\t" + Path;
        }

        private int CompareQualifier(SqQualifier Qualifier)
        {
            int ThisQualifier = QualifierToInt(this.Qualifier);
            int OtherQualifier = QualifierToInt(Qualifier);
            return ThisQualifier.CompareTo(OtherQualifier);
        }

        private static int QualifierToInt(SqQualifier qualifier)
        {
            switch (qualifier)
            {
                case SqQualifier.SUB_PROJECT: return 1;
                case SqQualifier.DIRECTORY: return 2;
                case SqQualifier.FILE: return 3;
                case SqQualifier.PROJECT: return 0;
                case SqQualifier.UNIT_TEST: return 4;
                default: throw new ArgumentException("Unknown Argument for Qualifier: \"" + qualifier + "\"");
            }
        }

        public string[] GetSplittedPath()
        {
            return Path.Split('/');
        }

        public int GetPathDepth()
        {
            return GetSplittedPath().Length;
        }

        public static T[] SubArray<T>(T[] data, int startIndex, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, startIndex, result, 0, length);
            return result;
        }

        public static SqQualifier QualifierForString(string qualifier)
        {
            switch (qualifier)
            {
                case "BRC": return SqQualifier.SUB_PROJECT;
                case "DIR": return SqQualifier.DIRECTORY;
                case "FIL": return SqQualifier.FILE;
                case "TRK": return SqQualifier.PROJECT;
                case "UTS": return SqQualifier.UNIT_TEST;
                default: throw new ArgumentException("Unknown Argument for Qualifier: \"" + qualifier + "\"");
            }
        }
    }


}
