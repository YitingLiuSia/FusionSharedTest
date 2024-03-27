using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SiaX
{
    public class ValidationManager : MonoBehaviour
    {
        // this is to test the validation of answers 


        public GameObject collectionGroup;
        [SerializeField] private List<GameObject> collections;
        [SerializeField]private List<string> collectionNames;

        public List<string> correctAnswers;
        public MeshRenderer station;

        private Material defaultMat;
        [SerializeField] private Material answerCorrectMat;
        [SerializeField] private Material answerWrongMat;

   
        void Start()
        {
            defaultMat = station.material;
            collectionNames = new List<string>();
            collections = new List<GameObject>();
        }
     
        // add this when ths item is droped on the station 
        void AddToAnswerCollection(GameObject go)
        {
            // avoid duplicated 
            if(collections.Contains(go))
                return;
            
            collections.Add(go);
            
            // collect name list to compare with the correct answer 
            collectionNames.Add(go.name);

        }


        public void ValidateAnswers()
        {
            // if the lists are the same then the player has answered all the questions correctly
            if (collectionNames == correctAnswers)
            {
                CorrectAnswerIndication();
            }
            // if the lists are not the same then the player has answered at least one question incorrectly
            else
            {
                WrongAnswerIndication();
            }
        }

        public void Reset()
        {
            station.material = defaultMat;
            collections.Clear();
            collectionNames.Clear();
        }

        private void CorrectAnswerIndication()
        {
            station.material = answerCorrectMat;
        }

        private void WrongAnswerIndication()
        {
            station.material = answerWrongMat;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}