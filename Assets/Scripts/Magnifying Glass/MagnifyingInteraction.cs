using UnityEngine;
using TMPro;
using Fusion;
using UnityEngine.XR.Interaction.Toolkit;

namespace SiaX.Magnifying
{
    public class MagnifyingInteraction : NetworkBehaviour
    {
        XRGrabInteractable grabInteractable;
        [Networked] public bool isGrabbled { get; set; }
        [SerializeField] MagnifyingManager mm;
        public bool useMagnifyingGlass = true;
        [SerializeField] GameObject magnifyingPanel;
        [SerializeField] TextMeshProUGUI magnifyingText;
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] GameObject objCanvas;
        float currentDistance;

        private void Awake()
        {
            grabInteractable = this.GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            Init();
        }
        // used to be in OnEnable 
        private void Init()
        {

            objCanvas.SetActive(false);
            grabInteractable.selectEntered.AddListener(TurnOnBool);
            grabInteractable.selectExited.AddListener(TurnOffBool);

            if (useMagnifyingGlass)
            {
                grabInteractable.selectEntered.AddListener(TurnOnMagnifyingContent);
                grabInteractable.selectExited.AddListener(TurnOffMagnifyingContent);
            }
        }

        private void TurnOffMagnifyingContent(SelectExitEventArgs arg0)
        {
            DisplayDefaultInfoForMagnifying();
        }
        private void TurnOnMagnifyingContent(SelectEnterEventArgs arg0)
        {
            DisplayContentIfCloser();
        }
        private void TurnOffBool(SelectExitEventArgs arg0)
        {
            RPC_ToggleBool(false);
            Debug.Log("TURN OFF BOOL ");
        }
        private void TurnOnBool(SelectEnterEventArgs arg0)
        {
            RPC_ToggleBool(true);
            Debug.Log("TURN ON BOOL");
        }
        private void OnDisable()
        {
            grabInteractable.selectEntered.RemoveAllListeners();
            grabInteractable.selectExited.RemoveAllListeners();
        }
        public override void FixedUpdateNetwork()
        {
            if (useMagnifyingGlass)
            {
                if (mm.magnifyingInteractable != null)
                {
                    currentDistance = Vector3.Distance(this.transform.position, mm.magnifyingInteractable.transform.position);
                }

                if (isGrabbled)
                {
                    DisplayContentIfCloser();
                }

                mm.objCanvas.SetActive(useMagnifyingGlass);
            }
        }
        private void DisplayContentIfCloser()
        {
            RPC_DisplayContentIfCloser();
        }
        private void DisplayDefaultInfoForMagnifying()
        {
            RPC_DisplayDefaultInfoForMagnifying();
        }

        #region RPCs
        [Rpc]
        private void RPC_ToggleBool(bool grabbed)
        {
            isGrabbled = grabbed;
            objCanvas.SetActive(isGrabbled);

            if (mm != null)
            {
                mm.magnifyingGlassPickedUp = isGrabbled;
            }

        }
        [Rpc]
        private void RPC_DisplayContentIfCloser()
        {
            if (currentDistance < mm.distanceThreshold)
            {
                mm.magnifyingText.text = magnifyingText.text;
                mm.titleText.text = titleText.text;
            }
            else
            {
                RPC_DisplayDefaultInfoForMagnifying();

            }
        }

        [Rpc]
        private void RPC_DisplayDefaultInfoForMagnifying()
        {
            Debug.Log("mm in RPC_DisplayDefaultInfoForMagnifying is null? " + mm == null);
            mm.titleText.text = mm.defaultTitle;
            mm.magnifyingText.text = mm.defaultMagnifyingContent + currentDistance;
        }

        #endregion
    }
}
