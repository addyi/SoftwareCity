using System.Collections.Generic;

public class SQPackage : ISQObject
{
    private Dictionary<string, float> metrics;

    private List<ISQObject> childs;

    public SQPackage()
    {
        metrics = new Dictionary<string, float>
        {
            { "M1", 4.2f },
            { "M2", 3.3f },
            { "M3", 60.0f }
        };

        childs = new List<ISQObject>();
    }

    public Dictionary<string, float> GetInformation()
    {
        return metrics;
    }

    public void AddChild(ISQObject child)
    {
        childs.Add(child);
    }

    public List<ISQObject> GetChilds()
    {
        return childs;
    }
}
