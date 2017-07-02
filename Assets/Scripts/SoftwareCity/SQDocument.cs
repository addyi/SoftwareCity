using System.Collections.Generic;

public class SQDocument : ISQObject
{
    private Dictionary<string, float> metrics;

    public SQDocument()
    {
        metrics = new Dictionary<string, float>
        {
            { "M1", 4.2f },
            { "M2", 3.3f },
            { "M3", 60.0f }
        };
    }

    public Dictionary<string, float> GetInformation()
    {
        return metrics;
    }
}
