using Fusion;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SiaX.Utility
{
    public class SceneChangePortal : MonoBehaviour
    {
        [SerializeField] ParticleSystem ActivePS;
        public bool isActive = false;
        public int WaitTime;
        //  [SerializeField] PinPad PinPadScript;
        public int LoadLevel = 2;
        NetworkRunner runner;
        private void Awake()
        {
            if (isActive == false)
            {
                ActivePS.Stop();
            }
            runner = GameObject.FindObjectOfType<NetworkRunner>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GoToScene();
            }
        }
        public void GoToScene()
        {
            Debug.Log("We made it to GoToScene");
            if (isActive)
            {
                Debug.Log("Should load scene portal jump");
               // SceneManager.LoadSceneAsync(LoadLevel, LoadSceneMode.Single);
                if (runner.IsSceneAuthority)
                {
                    runner.LoadScene(SceneRef.FromIndex(LoadLevel));
                }
                isActive = false;
            }
            else
            {
                Debug.Log("Portal not active");
            }
        }
        public void ActivatePortal()
        {
            Debug.Log("Portal Activated");
            ActivePS.Play();
            StartCoroutine(PortalTimeOut());
            isActive = true;
        }

        private IEnumerator PortalTimeOut()
        {
            yield return new WaitForSeconds(WaitTime);
            isActive = false;
            ActivePS.Stop();
            Debug.Log("Portal Deactivated");

            /* if(PinPadScript != null)
                 PinPadScript.ClearKeysOut();*/

        }


    }
}