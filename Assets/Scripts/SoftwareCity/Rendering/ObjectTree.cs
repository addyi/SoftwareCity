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

            SQPackage p1 = new SQPackage();
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());

            SQPackage p2 = new SQPackage();
            p2.AddChild(new SQDocument());
            p2.AddChild(new SQDocument());
            p2.AddChild(new SQDocument());
            p2.AddChild(new SQDocument());
            p2.AddChild(new SQDocument());
            p2.AddChild(new SQDocument());
            p2.AddChild(new SQDocument());

            SQPackage p3 = new SQPackage();
            p3.AddChild(new SQDocument());
            p3.AddChild(new SQDocument());
            p3.AddChild(new SQDocument());
            p3.AddChild(new SQDocument());
            p3.AddChild(new SQDocument());
            p3.AddChild(new SQDocument());
            p3.AddChild(new SQDocument());


            root.AddChild(p1);
            root.AddChild(p2);
            root.AddChild(p3);
            p1.AddChild(p2);
            p1.AddChild(p3);

            softwareCityBuilder.Build(root);
        }
    }
}

