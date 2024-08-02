using Unity.VisualScripting;
using UnityEngine;

public static class Tools
{
    public static int IntClamp(int value, int min, int max)
    {
        return value < min ? min : value > max ? max : value;
    }    

    public static Object InstantiateProjectile(this Object thisObj, GameObject player, Object original, Vector3 position, Quaternion rotation)
    {
        var projectile = Object.Instantiate(original, position, rotation);
        projectile.GetComponent<Projectile>().player = player;
        return projectile;
    }
}
