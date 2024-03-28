using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
// get the canvas the player is using or looking at 
// see if the component has the magnifying panel 
// turn on the magnifying panel 
namespace SiaX.Magnifying
{
    public class MagnifyingManager : MonoBehaviour
    {
        public bool magnifyingGlassPickedUp;
        public XRGrabInteractable magnifyingInteractable;
        public GameObject objCanvas;
        public float distanceThreshold;
        [Header("Default Setup")]
        public string defaultTitle;
        public string defaultMagnifyingContent;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI magnifyingText;
        private void OnValidate()
        {
            titleText.text = defaultTitle;
            magnifyingText.text = defaultMagnifyingContent; 
        }
    }
}