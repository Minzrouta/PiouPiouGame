using System;
using Unity.Netcode;
using UnityEngine;


public class Player : NetworkBehaviour
{
    public float playerHealth = 100;
    public float playerMaxHealth = 100;
    public float playerEnergy = 100;
    public float playerMaxEnergy = 100;
    public float playerEnergyRegen = 1;
    
    private void Start()
    {
        if (IsLocalPlayer)
        {
            //Hide player model
        }
    }
    
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        //Respawn player
    }
}
