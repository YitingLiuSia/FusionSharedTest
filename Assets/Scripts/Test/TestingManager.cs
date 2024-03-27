using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class TestingManager : MonoBehaviour
{
    [SerializeField] InputAction connectionAction;
    [SerializeField] TMP_InputField inputField;
    string userNameTitle = "userName";
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
            SetUserNameInOffline();
            LoadNewScene();
        }
    }
    void OnDestroy()
    {
        connectionAction.Disable();
    }
    public void SetUserNameInOffline()
    {
        PlayerPrefs.SetString(userNameTitle, inputField.text);
    }
    public void LoadNewScene()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
