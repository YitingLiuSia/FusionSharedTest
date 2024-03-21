using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.tvOS;
using UnityEngine.XR.Interaction.Toolkit;

public class LocalCamera : NetworkBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AudioListener audioListener;

    [SerializeField] Transform head;
    [SerializeField] ActionBasedController leftHand;
    [SerializeField] ActionBasedController rightHand;
    public override void Spawned()
    {
        cam.enabled = Object.HasStateAuthority;
        audioListener.enabled = Object.HasStateAuthority;
        leftHand.enabled = Object.HasStateAuthority;
        rightHand.enabled = Object.HasStateAuthority;
        Debug.Log($"camera enabled is {Object.HasStateAuthority}");

    }

    private void Update()
    {
        if (!Object.HasStateAuthority)
            return;

        head.SetPositionAndRotation(cam.transform.position, Quaternion.identity); 
    }


}
