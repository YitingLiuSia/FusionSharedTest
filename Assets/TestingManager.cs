using Fusion;
using Fusion.Addons.ConnectionManagerAddon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestingManager : MonoBehaviour
{
    private ConnectionManager connectionManager;
    [SerializeField] InputAction connectionAction;

    private void Awake()
    {
        connectionManager = GameObject.FindObjectOfType<ConnectionManager>();

        connectionAction = new InputAction(binding: "<keyboard>/space");
        connectionAction.Enable();

    }
    private void Update()
    {
        if (connectionAction.triggered)
        {
            _ = UpdateAsync();

        }
    }

    void OnDestroy()
    {
        connectionAction.Disable();

    }

    async Task UpdateAsync()
    {
        Debug.Log("update async");
        await connectionManager.Connect();
        connectionManager.onWillConnect.AddListener(LoadNewScene);

    }
    private void LoadNewScene()
    {
        Debug.Log("load new scene " + SceneRef.FromIndex(1));
        connectionManager.runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);

    }
}
