using System;
using Unity.Netcode;
using UnityEngine;


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
