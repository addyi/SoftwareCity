using UnityEngine;
using DiskIO.ProjectTreeSaveLoader;
using DataModel.ProjectTree.Components;
using DataModel;

//calls to generate a test enviroment
public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject enviroment;

    private bool enviromentExist;

    private void Start()
    {
        enviromentExist = false;

        Model m = Model.GetInstance();

        ComponentTreeStream.SaveProjectComponent<ProjectComponent>(m.GetTree());
        Debug.Log(ComponentTreeStream.LoadProjectComponent<ProjectComponent>());
    }

    public void CreateEnvironment()
    {
        if (!enviromentExist)
        {
            Instantiate(enviroment, transform.localPosition, transform.localRotation);
            enviromentExist = true;
        }

    }

}