using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    public float damage = 10;
    public float explosionRange = 0;
    public float explosiveDamage = 5;
    public float range = 100000;
    private float _distanceTraveled = 0;
    public float projectileSpeed = 1;
    private Rigidbody _rb;
    [SerializeField] private GameObject explosionPrefab;
    public GameObject player;


    
    
    void Update()
    {
        _distanceTraveled += projectileSpeed * Time.deltaTime;
        if (_distanceTraveled >= range)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player) return;
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
        if (explosionRange > 0)
        {
            Explode();
        }
        Debug.Log("Projectile hit something!");
        Destroy(gameObject);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (var hit in colliders)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Player>().TakeDamage(explosiveDamage);
            }
        }
    }
}
