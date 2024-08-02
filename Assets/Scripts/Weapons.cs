using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage = 10;
    public float explosionRange = 0;
    public float explosiveDamage = 5;
    public float range = 100;
    public float fireRate = 1;
    public float reloadTime = 1;
    public float projectileSpeed = 100;
    public int maxAmmo = 30;
    public int currentAmmo;
    public bool isReloading = false;
    public bool isMelee = false;
    
    [SerializeField] private GameObject projectilePrefab;
    
    public KeyCode fireButton = KeyCode.Mouse0;
    public KeyCode reloadButton = KeyCode.R;
    public KeyCode meleeButton = KeyCode.V;
    
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (Input.GetKeyDown(fireButton))
        {
            Fire();
        }

        if (Input.GetKeyDown(reloadButton))
        {
            Reload();
        }

        if (Input.GetKeyDown(meleeButton))
        {
            Melee();
        }
    }

    private void Fire()
    {
        if (isMelee)
        {
            Melee();
            return;
        }
        
        if (currentAmmo <= 0 || isReloading) return;
        
        currentAmmo--;
        // Summon a projectile prefab with the correct direction and speed
        var head = transform.Find("Head");
        var rotation = head.rotation;
        var projectile = head.InstantiateProjectile(gameObject, projectilePrefab, head.position, rotation);
        projectile.GetComponent<Rigidbody>().linearVelocity = rotation * Vector3.forward * projectileSpeed;
    }
    
    public void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo) return;
        
        isReloading = true;
        // Reload logic
    }
    
    public void Melee()
    {
        // Melee logic
    }
    
    
}
