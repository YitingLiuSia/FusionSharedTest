using Fusion;
using Fusion.Addons.ConnectionManagerAddon;
using System.Threading.Tasks;
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
       // connectionManager.onWillConnect.AddListener(LoadNewScene);

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

    }
    private void LoadNewScene()
    {
        Debug.Log("scene ref from index 1 isvalid" + SceneRef.FromIndex(1).IsValid);

        Debug.Log("load new scene " + SceneRef.FromIndex(1));
        connectionManager.runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Additive);

    }
}
