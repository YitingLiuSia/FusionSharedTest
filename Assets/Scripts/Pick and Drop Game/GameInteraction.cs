using System.Collections.Generic;
using UnityEngine;
using Fusion;
using static SiaX.GameInteraction;
using UnityEngine.XR.Interaction.Toolkit;

namespace SiaX
{
    public class GameInteraction : NetworkBehaviour
    {
        public List<ObjectGame> objectGames;
        public XRGrabInteractable checkAnswerBtn;
        public XRGrabInteractable resetAnswerBtn;
        public MeshRenderer checkBtnMR;
        public MeshRenderer resetBtnMR;

        public Material defaultMat;
        public Material highlightMat;
        public bool usingColorPaletteManager = true;
        [SerializeField] private ColorPaletteManager colorPM;
        public delegate void SpawnNetworkObjs();
        public SpawnNetworkObjs OnSpawnNetworkObjs;
        public override void Spawned()
        {
            OnSpawnNetworkObjs?.Invoke();

        }
        private void OnEnable()
        {
            OnSpawnNetworkObjs += RPC_DefaultCheckStation;
            OnSpawnNetworkObjs += RPC_DefaultResetStation;

        }

        private void OnDisable()
        {
            OnSpawnNetworkObjs -= RPC_DefaultCheckStation;
            OnSpawnNetworkObjs -= RPC_DefaultResetStation;

        }

        public void HighlightCheckStation()
        {
            RPC_HighlightCheckStation();
        }

        public void HighlightResetStation()
        {
            RPC_HighlightResetStation();
        }

        public void DefaultCheckStation()
        {
            RPC_DefaultCheckStation();
        }

        public void DefaultResetStation()
        {
            RPC_DefaultResetStation();
        }

        [Rpc]
        void RPC_HighlightCheckStation()
        {
            MatForStation(checkBtnMR, colorPM.highlightMaterial, highlightMat);

        }
        [Rpc]
        void RPC_HighlightResetStation()
        {

            MatForStation(resetBtnMR, colorPM.highlightMaterial, highlightMat);

        }
        [Rpc]

        void RPC_DefaultCheckStation()
        {

            MatForStation(checkBtnMR, colorPM.defaultMaterial, defaultMat);
        }
        [Rpc]
        void RPC_DefaultResetStation()
        {

            MatForStation(resetBtnMR, colorPM.defaultMaterial, defaultMat);

        }

        private void MatForStation(MeshRenderer mr, Material matfromCPM, Material normalMat)
        {
            if (usingColorPaletteManager)
            {

                BtnToMat(mr, matfromCPM);
            }
            else
            {
                BtnToMat(mr, normalMat);

            }
        }
        private void BtnToMat(MeshRenderer btnMR, Material mat)
        {
            btnMR.material = mat;

        }

    }
}
