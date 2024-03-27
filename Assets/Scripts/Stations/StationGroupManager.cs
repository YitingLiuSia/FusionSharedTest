using System.Collections.Generic;
using UnityEngine;
using Fusion;


namespace SiaX
{
    public class StationGroupManager : NetworkBehaviour
    {
        public List<StationManager> stationManagers;
        public Material selectedMat;
        [Networked] public int StationNum { get; set; }
        [SerializeField] ColorPaletteManager cpm;
        [SerializeField] GameObject magnifyingManagerObj;
        [SerializeField] NetworkRunner runner; 

        public delegate void SpawnNetworkObjs();
        public SpawnNetworkObjs OnSpawnNetworkObjs;
    
        public override void Spawned()
        {
            OnSpawnNetworkObjs?.Invoke();
           
        }

        private void OnEnable()
        {
            OnSpawnNetworkObjs += RPC_TurnOffAllStations;
        }
        private void OnDisable()
        {

            OnSpawnNetworkObjs -= RPC_TurnOffAllStations;
        }

        private void OnDestroy()
        {
            OnSpawnNetworkObjs -= RPC_TurnOffAllStations;

        }
        [Rpc]
        public void RPC_TurnOffAllStations()
        {
            Debug.Log("Turn Of All Stations");
            magnifyingManagerObj.SetActive(false);

            foreach (var sm in stationManagers)
            {
                sm.gameObject.SetActive(true);

                SetMat(sm, selectedMat);

                sm.stationContent.SetActive(false);

            }
        }

        public void TurnOnOneStation(int index)
        {
            RPC_TurnOnOneStationFromIndex(index);
        }

        [Rpc]
        public void RPC_TurnOnOneStationFromIndex(int index)
        {
            for (int i = 0; i < stationManagers.Count; i++)
            {
                if (index != i)
                {
                    // set other station manager's content to be false
                    stationManagers[i].stationContent.SetActive(false);

                    SetMat(stationManagers[i], selectedMat);
                }
                else
                {
                    stationManagers[index].stationContent.SetActive(true);

                    SetMat(stationManagers[i], cpm.darkerMaterial);

                    // turn on magnifying object for station manager object that marks it to be true 
                    magnifyingManagerObj.SetActive(stationManagers[index].displayMagnifyingGlass);

                }
            }
        }
        private void SetMat(StationManager sm, Material mat)
        {
            foreach (Renderer render in sm.renders)
            {
                render.material = mat;
            }
        }
    }
}
