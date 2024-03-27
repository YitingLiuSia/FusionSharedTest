using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestingManager : MonoBehaviour
{
    [SerializeField] InputAction connectionAction;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] InputField inputField;
    private void Awake()
    {

        connectionAction = new InputAction(binding: "<keyboard>/space");
        connectionAction.Enable();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Debug.Log($"scene at {i} is {SceneManager.GetSceneByBuildIndex(i).name}");
        }

    }
    private void Update()
    {
        if (connectionAction.triggered)
        {
            LoadNewScene();

        }
    }

    void OnDestroy()
    {
        connectionAction.Disable();
    }

    public void LoadNewScene()
    {
        /*        Debug.Log("scene ref from index 1 isvalid" + SceneRef.FromIndex(1).IsValid);
                Debug.Log("load new scene " + SceneRef.FromIndex(1));
        */

        playerInfo.InvokePlayerSet(inputField.text);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        // connectionManager.runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);

    }
}
