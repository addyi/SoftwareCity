using SoftwareCity.Rendering;
using UnityEngine;

namespace SoftwareCity.Rendering
{
    public class ObjectTree : MonoBehaviour {

        [SerializeField]
        private SoftwareCityBuilder softwareCityBuilder;

	    // Use this for initialization
	    void Start () {
            SQPackage root = new SQPackage();
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());
            root.AddChild(new SQDocument());


            softwareCityBuilder.Build(root);
        }
    }
}

