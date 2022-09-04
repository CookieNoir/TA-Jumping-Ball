using UnityEngine;

public abstract class PlatformBlocksTemplate : ScriptableObject
{
    public virtual GameObject[] GetPrefabs()
    {
        return null;
    } 
}