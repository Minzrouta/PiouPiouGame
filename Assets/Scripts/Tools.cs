using UnityEngine;

public class Tools
{
    public static int IntClamp(int value, int min, int max)
    {
        return value < min ? min : value > max ? max : value;
    }    
}
