using UnityEngine;
using Fusion;
using Fusion.XR.Shared.Rig;

public class NetworkRigSimple : NetworkBehaviour { 
    public bool IsLocalNetworkRig => Object.HasStateAuthority;

    [Header("RigComponents")]
    [SerializeField]
    private NetworkTransform playerTransform;

    [SerializeField]
    private NetworkTransform headTransform;

    [SerializeField]
    private NetworkTransform leftHandTransform;

    [SerializeField]
    private NetworkTransform rightHandTransform;

    HardwareRig hardwareRig;

    public override void Spawned()
    {
        base.Spawned();

        if (IsLocalNetworkRig)
        {
            hardwareRig = FindObjectOfType<HardwareRig>();
            if (hardwareRig == null)
                Debug.LogError("Missing HardwareRig in the scene");
        }
        // else it means that this is a client
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

       /* if (GetInput<RigState>(out var input))
        {
            playerTransform.transform.SetPositionAndRotation(input.playAreaPosition, input.playAreaRotation);

            headTransform.transform.SetPositionAndRotation(input.headsetPosition, input.headsetRotation);

            leftHandTransform.transform.SetPositionAndRotation(input.leftHandPosition, input.leftHandRotation);

            rightHandTransform.transform.SetPositionAndRotation(input.rightHandPosition, input.rightHandRotation);

        }*/
    }

    public override void Render()
    {
        base.Render();
        if (IsLocalNetworkRig)
        {
            playerTransform.transform.SetPositionAndRotation(hardwareRig.transform.position, hardwareRig.transform.rotation);

            headTransform.transform.SetPositionAndRotation(hardwareRig.headset.transform.position, hardwareRig.headset.transform.rotation);

            leftHandTransform.transform.SetPositionAndRotation(hardwareRig.leftHand.transform.position, hardwareRig.leftHand.transform.rotation);

            rightHandTransform.transform.SetPositionAndRotation(hardwareRig.rightHand.transform.position, hardwareRig.rightHand.transform.rotation);

        }
    }
}
