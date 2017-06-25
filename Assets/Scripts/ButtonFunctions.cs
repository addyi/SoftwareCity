using UnityEngine;

//calls to generate a test enviroment
public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject enviroment;

    private bool enviromentExist;

    private void Start()
    {
        enviromentExist = false;
    }

    public void CreateEnviroment()
    {
        if(!enviromentExist)
        {
            Instantiate(enviroment, transform.localPosition, transform.localRotation);
            enviromentExist = true;
        }

    }
}
