using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    string userNameTitle = "userName";
    string playerName;
    public TextMeshProUGUI nameText;
    public delegate void onPlayerSet(string name);
    public event onPlayerSet OnPlayerSet;

    public delegate void onPlayerGet();
    public event onPlayerGet OnPlayerGet;

    public void InvokePlayerSet(string _name) {

        OnPlayerSet?.Invoke(_name);
    }

    public void InvokePlayerGet() {

        OnPlayerGet?.Invoke();

    }
    /// <summary>
    /// Set Player Info such as name and avatar skin when hitting the button 
    /// </summary>
    public void SetPlayerInfo(string _name)
    {
        Debug.Log("set player info " + _name);
        PlayerPrefs.SetString(userNameTitle, _name);
    }

    public void DisplayUserName(string _name)
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
        DisplayUserName(playerName);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        OnPlayerSet += SetPlayerInfo;
        OnPlayerGet += GetPlayerInfo;
    }

    private void OnDestroy()
    {

        OnPlayerSet -= SetPlayerInfo;
        OnPlayerGet -= GetPlayerInfo;
    }

}
