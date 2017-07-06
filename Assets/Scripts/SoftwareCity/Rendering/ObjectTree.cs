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
            p1.AddChild(new SQDocument());
            p1.AddChild(new SQDocument());

            SQPackage p2 = new SQPackage();

            SQPackage p3 = new SQPackage();

            SQPackage p4 = new SQPackage();
            SQPackage p5 = new SQPackage();
            SQPackage p6 = new SQPackage();
            SQPackage p7 = new SQPackage();
            SQPackage p8 = new SQPackage();
            SQPackage p9 = new SQPackage();
            SQPackage p10 = new SQPackage();
            SQPackage p11 = new SQPackage();
            SQPackage p12 = new SQPackage();
            SQPackage p13 = new SQPackage();
            SQPackage p14 = new SQPackage();

            p12.AddChild(p14);
            p12.AddChild(p13);
            p11.AddChild(p12);
            p10.AddChild(p11);
            p9.AddChild(p10);
            p8.AddChild(p9);
            p7.AddChild(p8);

            p6.AddChild(p7);
            p5.AddChild(p6);
            p4.AddChild(p5);
            p3.AddChild(p4);

            p2.AddChild(p3);

            p1.AddChild(p2);

            root.AddChild(p1);

            softwareCityBuilder.Build(root);
        }
    }
}

