using UnityEngine;
using Foundry;
using System.Collections.Generic;
using Fusion;
using UnityEngine.XR.Interaction.Toolkit;
using System;
namespace SiaX
{
    [System.Serializable]
    public class StationManager : NetworkBehaviour
    {
        // toggle station based on the gameobject 
        public XRSimpleInteractable stationSimpleInteractable;
/*        [SerializeField] private XRGrabInteractable stationGrabInteractable;
*/        public GameObject stationContent;
        public List<Renderer> renders;
        public bool isToggledOn;
        private StationGroupManager sgManager;
        public bool displayMagnifyingGlass;
        public PullRigManager pullRigManager;

        void Awake()
        {
            sgManager = GameObject.FindObjectOfType<StationGroupManager>();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            sgManager = GameObject.FindObjectOfType<StationGroupManager>();
        }

#endif
        private void OnEnable()
        {
            stationSimpleInteractable.selectEntered.AddListener(ToggleContent);
            stationSimpleInteractable.selectEntered.AddListener(AnimateHandleOn);
            stationSimpleInteractable.selectExited.AddListener(AnimateHandleOff);
        }

        private void AnimateHandleOff(SelectExitEventArgs arg0)
        {
            RPC_AnimationHandle(false);
        }

        private void AnimateHandleOn(SelectEnterEventArgs arg0)
        {
            RPC_AnimationHandle(true);
        }
        private void ToggleContent(SelectEnterEventArgs arg0)
        {
            RPC_ToggleStationContent();
        }
        private void OnDestroy()
        {
            stationSimpleInteractable.selectEntered.RemoveAllListeners();
            stationSimpleInteractable.selectEntered.RemoveAllListeners();
            stationSimpleInteractable.selectExited.RemoveAllListeners();
        }
        private void OnDisable()
        {
            stationSimpleInteractable.selectEntered.RemoveAllListeners();
            stationSimpleInteractable.selectEntered.RemoveAllListeners();
            stationSimpleInteractable.selectExited.RemoveAllListeners();
        }

        [Rpc]
        public void RPC_ToggleStationContent()
        {
            ToggleContent(sgManager);
        }

        [Rpc]
        public void RPC_AnimationHandle(bool on)
        {
            if (pullRigManager) pullRigManager.AnimatePullRigHandle(on);
        }
        public void ToggleContent(StationGroupManager sgManager)
        {
            int index = sgManager.stationManagers.IndexOf(this);
            sgManager.StationNum = index;
            sgManager.TurnOnOneStation(index);

            Debug.Log("station num is " + sgManager.StationNum);
        }
    }
}
