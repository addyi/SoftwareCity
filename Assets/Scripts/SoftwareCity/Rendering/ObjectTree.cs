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

            SQPackage p4 = new SQPackage();
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());
            p4.AddChild(new SQDocument());

            SQPackage p5 = new SQPackage();
            p5.AddChild(new SQDocument());

            SQPackage p6 = new SQPackage();
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());
            p6.AddChild(new SQDocument());

            SQPackage p7 = new SQPackage();
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());
            p7.AddChild(new SQDocument());

            SQPackage p8 = new SQPackage();
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());
            p8.AddChild(new SQDocument());

            SQPackage p9 = new SQPackage();
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());
            p9.AddChild(new SQDocument());

            root.AddChild(p1);
            root.AddChild(p2);
            root.AddChild(p3);
            root.AddChild(p4);
            root.AddChild(p5);

            p1.AddChild(p9);
            p1.AddChild(p9);
            p1.AddChild(p9);
            p1.AddChild(p9);
            
        }
    }
}

