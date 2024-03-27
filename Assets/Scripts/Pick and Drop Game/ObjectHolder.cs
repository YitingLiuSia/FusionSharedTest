using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SiaX
{
    public class ObjectHolder : NetworkBehaviour
    {
        [Header("Rename the gameobject to the same name with the object in Object Group")]
        public Transform objectTarget;
        public string correctInfo;

        public ObjectGame objectGame;
        public bool strictCheck;
        [SerializeField]
        private Object objectOnPedestal;
        private List<Object> objectsOnPedestal;
        public bool correct { get; set; }
        public Material correctMaterial;
        public Material inCorrectMaterial;
        public Material defaultMaterial;
        public MeshRenderer childMeshRenderer;

        public Outline outline;
        public delegate void SpawnNetworkObjs(); 
        public SpawnNetworkObjs OnSpawnNetworkObjs;

        /// <summary>
        /// Automatically apply the material for the child mesh renderer for ease of testing 
        /// </summary>
        private void ApplyDefaultMaterial()
        {
            childMeshRenderer = GetComponentInChildren<MeshRenderer>();
            childMeshRenderer.material = defaultMaterial;
        }
        private void Awake()
        {
            objectsOnPedestal = new List<Object>();

        }
        public override void Spawned()
        {
            OnSpawnNetworkObjs?.Invoke();
           
        }
        public override void FixedUpdateNetwork()
        {
          //  outline.enabled = objectOnPedestal != null;
        }
        private void GetObjectName()
        {
            correctInfo = gameObject.name;
        }
      
        private void OnEnable()
        {
            OnSpawnNetworkObjs+=Init;
        }

        private void OnDisable()
        {
            OnSpawnNetworkObjs -= Init;

        }

        private void Init()
        {

            strictCheck = objectGame.strictCheck;
            correct = false;
            objectsOnPedestal = new List<Object>();
            objectOnPedestal = null;
            outline.enabled = false;
            GetObjectName();
            ApplyDefaultMaterial();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Object>())
            {
                objectOnPedestal = other.GetComponent<Object>();
                objectsOnPedestal.Add(objectOnPedestal);
            }
        }

        //only allow one object on the pedastol 
        private void OnTriggerStay(Collider other)
        {
            if (objectsOnPedestal == null)
                return;

            if (objectsOnPedestal.Count >= 1)
            {
                objectOnPedestal = objectsOnPedestal[objectsOnPedestal.Count - 1];
                SnapToLocation(objectOnPedestal);

                if (objectsOnPedestal.Count >= 2)
                {
                    for (int i = 0; i < objectsOnPedestal.Count - 1; i++)
                    {
                        objectsOnPedestal[i].ResetObjectLocation();
                    }
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {

            if (other.GetComponent<Object>())
            {
                if (objectsOnPedestal.Any() && objectOnPedestal == objectsOnPedestal.Last()) //objectOnPedestal != null &&
                {
                    objectOnPedestal = null;
                    objectsOnPedestal.Clear();


                }
            }
        }

        private void SnapToLocation(Object objectOnPedestal)
        {
            Transform trans = objectOnPedestal.transform;
            trans.position = objectTarget.position;
            trans.rotation = objectTarget.rotation;
        }


        public void CheckAnswers()
        {
            RPC_CheckCorrectInfo();
        }

        // check correct info based on the list of the right info the object can have 
        [Rpc]
        private void RPC_CheckCorrectInfo()
        {

            if (strictCheck)
            {
                if (objectOnPedestal == null)
                {

                    IncorrectDisplay();
                }
                else
                {
                    if (objectOnPedestal.correctInfo == correctInfo)
                    {
                        CorrectDisplay();
                    }
                    else
                    {
                        IncorrectDisplay();
                    }
                }
            }
            else
            {

                if (objectOnPedestal && objectGame.currentInfoNames.Contains(objectOnPedestal.correctInfo))
                {
                    CorrectDisplay();
                }
                else
                {
                    IncorrectDisplay();
                }
            }
        }

        private void IncorrectDisplay()
        {
            correct = false;
            childMeshRenderer.material = inCorrectMaterial;
        }
        private void CorrectDisplay()
        {
            correct = true;
            childMeshRenderer.material = correctMaterial;
        }
        [Rpc]
        public void RPC_ResetColor()
        {
            correct = false;
            childMeshRenderer.material = defaultMaterial;
            objectsOnPedestal?.Clear();

            outline.enabled = false;

            if (objectOnPedestal != null)
            {
                objectOnPedestal = null;

            }
        }


    }
}