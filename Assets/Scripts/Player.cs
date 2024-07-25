using System;
using Unity.Netcode;
using UnityEditor.Embree;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour
{
    private void Start()
    {
        if (IsLocalPlayer)
        {
            //Hide player model
        }
    }
}
