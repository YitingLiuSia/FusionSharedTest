using UnityEngine;

namespace SiaX.Utility
{
    public class ObjecLifetime : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}