using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SiaX.Utiltiy
{
    [CanSelectMultiple]
    public class BillboardTool : NetworkBehaviour
    {
        private Transform mainCamera;
        public override void Spawned()
        {
            if (Runner.LocalPlayer == this.Object.StateAuthority) {
                mainCamera = Camera.main.transform;
                Debug.Log("main camera in " + this.name + " is " + mainCamera);
            }
        }

        private void LateUpdate()
        {
            if (!mainCamera)
                return;

            Vector3 direction = transform.position - mainCamera.position;
            transform.rotation = Quaternion.LookRotation(-Vector3.up, -direction);// used to be up 

        }
    }
}