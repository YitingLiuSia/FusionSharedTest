using UnityEngine;

public class ObjecLifetime : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

  
}
