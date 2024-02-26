using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerLooker : MonoBehaviour
{

    private Transform xrOrigin;
    private Transform looker;

    private bool hasStateAuthority; 
    void Start()
    {
        hasStateAuthority = GetComponent<NetworkObject>().HasStateAuthority;

        if (hasStateAuthority) {
            xrOrigin = GameObject.Find("XR Origin").transform;
            looker = GameObject.Find("Looker").transform;
        }
    }
    void Update()
    {
        if (!hasStateAuthority)
            return;


        transform.position = xrOrigin.position;
        transform.LookAt(looker.position);
        
    }
}
