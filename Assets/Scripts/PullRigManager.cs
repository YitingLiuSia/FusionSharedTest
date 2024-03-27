
using UnityEngine;
using System.Collections;

namespace SiaX
{
    public class PullRigManager : MonoBehaviour
    {
        public Transform armPull;
      //  public float ArmPullAmount = 45f;
      //  public bool rigPulled;
      //  public delegate bool PullRigEvent(bool rigPulled);
      //  public PullRigEvent onRigPulled;


        [Header("Animation Of Station Box")]
        public float animationDuration = 2f;
        public Vector3 initialRotation;
        public Vector3 finalRotation;
        public Vector3 currentRotation;
        public bool shouldAnimate = false;
        private Quaternion initialQuaternion;
        private Quaternion finalQuaternion;

        private void Start()
        {

            Init();
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            Init();
        }

#endif
        private void Init()
        {
            currentRotation = armPull.rotation.eulerAngles;
            initialRotation = currentRotation;
            finalRotation = currentRotation + new Vector3(90, 0, 0);
            initialQuaternion = Quaternion.Euler(currentRotation);
            finalQuaternion = Quaternion.Euler(finalRotation);
        }

        private void Update()
        {
            /* # if UNITY_EDITOR
            AnimatePullRigHandle(Input.GetKeyDown(KeyCode.Space));
             #endif*/

        }

        public void AnimatePullRigHandle(bool check)
        {
            Debug.Log("should AnimatePullRigHandle");
            shouldAnimate = check;

            if (shouldAnimate)
            {
                StartCoroutine(AnimateRotation());

            }

        }

        private void FixedUpdate()
        {
            currentRotation = armPull.rotation.eulerAngles;

        }
        private IEnumerator AnimateRotation()
        {
            Debug.Log("StartCoroutine AnimateRotation ");

            float timeElapsed = 0f;

            while (timeElapsed < animationDuration)
            {
                float t = timeElapsed / animationDuration;
                armPull.rotation = Quaternion.Lerp(initialQuaternion, finalQuaternion, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            armPull.rotation = finalQuaternion;

            // Rotate back to the initial rotation
            timeElapsed = 0f;
            while (timeElapsed < animationDuration)
            {
                float t = timeElapsed / animationDuration;
                armPull.rotation = Quaternion.Lerp(finalQuaternion, initialQuaternion, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            armPull.rotation = initialQuaternion; // Ensure it's set to the initial rotation exactly

            shouldAnimate = false;
            Debug.Log("StartCoroutine AnimateRotation OVER ");

        }


    }
}

