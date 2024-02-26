using Fusion;
using UnityEngine;

public class XRControllerManager : MonoBehaviour
{
    [SerializeField] Transform localLleftController;
    [SerializeField] Transform localRightController;

    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;

    bool hasStateAuthroity;
    void Start()
    {

        hasStateAuthroity = GetComponent<NetworkObject>().HasStateAuthority;

        if (hasStateAuthroity)
        {
            localLleftController = GameObject.Find("Left Controller").transform;
            localRightController = GameObject.Find("Right Controller").transform;

            leftController = localLleftController;
            rightController = localRightController;
            Debug.Log("XRControllerManager has state authority");
        }
    }


    private void Update()
    {
       /* if (hasStateAuthroity)
        {
            leftController.SetParent(localLleftController);
            rightController.SetParent(localRightController);
        }*/
    }
}
