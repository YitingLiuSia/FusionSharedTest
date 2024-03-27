using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace SiaX
{
    public class ObjectCenter : NetworkBehaviour
    {

        public List<string> centerInfo;
        [SerializeField] private TMPro.TextMeshProUGUI centerText;
        public string defaultCenterInfo;
        public int correctIndex;
       [Networked] public int currentIndex { get; set; }
        [SerializeField] private Object obj;

        public bool isCenterTextSelectable;
        [Header("Material Section")]
        public MeshRenderer meshRender;
        public Material correctMaterial;
        public Material inCorrectMaterial;
        public Material defaultMaterial;
        public Material highlightMaterial;

        private void Init()
        {
            centerText.text = defaultCenterInfo;

            if (!isCenterTextSelectable)
            {
                currentIndex = correctIndex;
                string centerStr = centerInfo[currentIndex];
                SetStringToCurrentIndex(centerStr);

            }
        }

        public override void Spawned()
        {
            currentIndex = 0;

            Init();


        }
        // called in editor 
        public void SelectCenterInfo()
        {
            RPC_SelectCenterInfo();
        }

        [Rpc]
        public void RPC_SelectCenterInfo()
        {
            Debug.Log("RPC select center info in Object Center");

            if (!isCenterTextSelectable)
                return;

            currentIndex++;

            if (currentIndex >= centerInfo.Count)
            {
                currentIndex = 0;
            }
            string centerStr = centerInfo[currentIndex];

            SetStringToCurrentIndex(centerStr);
            meshRender.material = highlightMaterial;

        }

        private void SetStringToCurrentIndex(string centerStr)
        {
            gameObject.name = centerStr;
            centerText.text = centerStr;
            obj.GetObjectName();
        }

        [Rpc]
        public void RPC_DisplayAnswerCheck()
        {
            if (!isCenterTextSelectable)
                return;

            if (correctIndex == currentIndex)
            {
                CorrectDisplay();

            }
            else
            {
                IncorrectDisplay();

            }


        }

        [Rpc]
        public void RPC_ResetAnswerCheck()
        {
            if (isCenterTextSelectable)
            {
                currentIndex = -1;
            }

            centerText.text = defaultCenterInfo;
            meshRender.material = defaultMaterial;

        }



        private void IncorrectDisplay()
        {
            meshRender.material = inCorrectMaterial;
        }

        private void CorrectDisplay()
        {
            // Debug.Log("center object display CORRECT");

            meshRender.material = correctMaterial;
        }

    }
}
