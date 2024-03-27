using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

namespace SiaX
{
    public class ObjectGame : NetworkBehaviour
    {
        public bool start;
        public int timerInterval;
        public float timerLeft;
        public TMP_Text timer;
        public GameObject objectGroup;
        public GameObject objectHolderGroup;
        public bool strictCheck;// object has to be in the correct order 


        [Header("If Select Object Center")]
        public bool useObjectCenter;
        public ObjectCenter objectCenter;

        [Header("Name the Object and Object Holder Groups with the same name for easy testing in hierarchy")]
        [DisplayWithoutEdit, NonReorderable] public List<ObjectHolder> objectHolders;
        [DisplayWithoutEdit, NonReorderable] public List<Object> objects;
        [DisplayWithoutEdit, NonReorderable] public List<string> currentInfoNames;
        public delegate void SpawnNetworkObjs();
        public SpawnNetworkObjs OnSpawnNetworkObjs;
        private void Start()
        {

            timerLeft = timerInterval;
            timer.text = timerLeft.ToString("F2").Replace(".", ":");
            start = false;

            GetObjectCollection();
            if (objectCenter == null)
                return;

            if (objectCenter.gameObject.activeInHierarchy)
            {
                objectCenter.gameObject.SetActive(useObjectCenter);

            }

        }

        #region Reset Game On Quit and Disable 

        public override void Spawned()
        {
            OnSpawnNetworkObjs?.Invoke();
        }

        private void OnEnable()
        {
            OnSpawnNetworkObjs+= ResetGame;
        }
        private void OnDisable()
        {
            OnSpawnNetworkObjs -= ResetGame;

        }
        #endregion

        private void GetObjectCollection()
        {
            objectHolders = new List<ObjectHolder>();
            currentInfoNames = new List<string>();
            foreach (ObjectHolder objHolder in objectHolderGroup.GetComponentsInChildren<ObjectHolder>())
            {
                objectHolders.Add(objHolder);
                currentInfoNames.Add(objHolder.gameObject.name);
            }

            objects = new List<Object>();

           foreach (Object obj in objectGroup.GetComponentsInChildren<Object>())
            {
                objects.Add(obj);
            }
        }

        public void StartGame()
        {
            RPC_StartGame();
        }

        private void Update()
        {
            if (start)
            {
                timerLeft -= 1 * Time.deltaTime;
                timer.text = timerLeft.ToString("F2").Replace(".", ":");
            }

            if (timerLeft < 0)
            {
                ResetTimer();

                CheckTheAnswers();
            }
        }

        public void CheckAnswer()
        {
            RPC_CheckAnswer();
        }

        public void ResetGame()
        {
            RPC_ResetGame();
        }

        private void ResetObjects()
        {
           // Debug.Log($"in RESET objects count is : {objects.Count}");


            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].ResetObjectLocation();
            }

            for (int i = 0; i < objectHolders.Count; i++)
            {
                objectHolders[i].RPC_ResetColor();
            }


            if (useObjectCenter)
            {

                objectCenter.RPC_ResetAnswerCheck();

            }

        }

        private void ResetTimer()
        {
            start = false;

            timerLeft = timerInterval;
            timer.text = timerLeft.ToString("F2").Replace(".", ":");
        }

        // check the answers if it is strict check, it has to be on the exact slot 
        // if it is not strict check, it can be in any spot in the group 
        private void CheckTheAnswers()
        {

            for (int i = 0; i < objectHolders.Count; i++)
            {
                objectHolders[i].CheckAnswers();

            }


            if (useObjectCenter)
            {

                objectCenter.RPC_DisplayAnswerCheck();

            }
        }



    
        #region Rpc's

        [Rpc]
        void RPC_StartGame()
        {
            start = true;

            ResetObjects();

        }

        [Rpc]
        void RPC_ResetGame()
        {
            Debug.Log("Reset Game pressed");
            ResetTimer();
            ResetObjects();


        }


        [Rpc]
        void RPC_CheckAnswer()
        {
           // Debug.Log("Check Answer pressed");
            CheckTheAnswers();


        }

        #endregion
    }
}