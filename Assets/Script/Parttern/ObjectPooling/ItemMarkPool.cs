using UnityEngine;


[RequireComponent(typeof(PooledObject))]
public class ItemMarkPool : PooledObject
{
    public void ItemRelease()
    {
        Release();
    }
}
