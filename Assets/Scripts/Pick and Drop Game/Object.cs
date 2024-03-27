using UnityEngine;
using Fusion;
using UnityEngine.XR.Interaction.Toolkit;

namespace SiaX
{
    public class Object : NetworkBehaviour
    {
        public string correctInfo;
        private Vector3 _originalPos;
        private Quaternion _originalRot;
        [HideInInspector]
        public XRGrabInteractable grabable;
        [HideInInspector]
        public Rigidbody rigidBody;

        private void Awake()
        {
            GetObjectName();
            grabable = GetComponent<XRGrabInteractable>();
            rigidBody = GetComponent<Rigidbody>();

        }

        private void OnEnable()
        {
            grabable?.selectExited.AddListener(FreezeVelocity);
            grabable?.activated.AddListener(FreezeVelocity);
        }

        private void FreezeVelocity(ActivateEventArgs arg0)
        {
            FreezeVelocity();
        }

        private void FreezeVelocity(SelectExitEventArgs arg0)
        {
            FreezeVelocity();
        }

        private void OnDisable()
        {
            grabable?.selectExited.RemoveAllListeners();
            grabable.activated.RemoveAllListeners();

        }
        private void OnValidate()
        {
            GetObjectName();
        }
        public void GetObjectName()
        {
            correctInfo = gameObject.name;
        }
        private void Start()
        {
            if (transform == null)
                return;
            // get the current location and rotation of the logo and store it in _originalPos
            _originalPos = transform.position;
            _originalRot = transform.rotation;
        }
        public void ResetObjectLocation()
        {
            if (transform == null)
                return;
            // put the transform and rotation to that of the originalPos
            if (_originalPos != null)
                transform.position = _originalPos;
            if (_originalRot != null)
                transform.rotation = _originalRot;

            FreezeVelocity();

        }
        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.GetComponent<Object>())
            {
                Debug.Log("enter collision " + collision.gameObject.name);

                rigidBody.isKinematic = true;
                FreezeVelocity();
            }
            else {
                rigidBody.isKinematic = false;

            }
        }
        private void FreezeVelocity()
        {
            if (rigidBody == null)
                return;

            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
}
