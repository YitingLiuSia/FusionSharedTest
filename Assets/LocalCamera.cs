using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCamera : NetworkBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AudioListener audioListener;
    public override void Spawned()
    {
        cam.enabled = Object.HasStateAuthority;
        audioListener.enabled = Object.HasStateAuthority;
        Debug.Log($"camera enabled is {Object.HasStateAuthority}");
    }

}
