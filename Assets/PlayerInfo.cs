using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class PlayerInfo : NetworkBehaviour
{
    string userNameTitle = "userName";
    string playerName;
    public TextMeshProUGUI nameText;
/*    public delegate void onPlayerSet(string name);
    public event onPlayerSet OnPlayerSet;*/

    public delegate void onPlayerGet();
    public event onPlayerGet OnPlayerGet;

    /*   public void InvokePlayerSet(string _name) {

           OnPlayerSet?.Invoke(_name);
       }
       /// <summary>
       /// Set Player Info such as name and avatar skin when hitting the button 
       /// </summary>
       public void SetPlayerInfo(string _name)
       {
           Debug.Log("set player info " + _name);
           PlayerPrefs.SetString(userNameTitle, _name);
       }*/

    // display the local name to network 
    // face the other players
    // 

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
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
    }


}
