using Fusion.Addons.ConnectionManagerAddon;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestingManager : MonoBehaviour
{
    private ConnectionManager connectionManager;
    public KeyCode connectionKey; 

    private void Update()
    {
        if (Input.GetKeyDown(connectionKey))
        {
           _= UpdateAsync();

        }
    }
    async Task UpdateAsync()
    {
        Debug.Log("update async");
        await connectionManager.Connect();

    }
}
