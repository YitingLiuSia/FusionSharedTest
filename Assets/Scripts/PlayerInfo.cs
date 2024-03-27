using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

namespace SiaX
{
    public class PlayerInfo : NetworkBehaviour
    {
        string userNameTitle = "userName";
        string playerName;
        public TextMeshProUGUI nameText;
        public delegate void onPlayerGet();
        public event onPlayerGet OnPlayerGet;

        [Rpc]
        public void RPC_DisplayUserName(string _name)
        {
            nameText.text = _name;
        }
        /// <summary>
        /// Get Player Info such as name and avatar skin when scene loads 
        /// </summary>
        public void GetPlayerInfo()
        {
            Debug.Log("get player info " + userNameTitle);
            playerName = PlayerPrefs.GetString(userNameTitle);
            Debug.Log($"playerName {playerName}");
            RPC_DisplayUserName(playerName);
        }

        public override void Spawned()
        {
            OnPlayerGet?.Invoke();
        }

        private void Awake()
        {
            OnPlayerGet += GetPlayerInfo;
        }
        private void OnDestroy()
        {
            OnPlayerGet -= GetPlayerInfo;
        }

    }
}