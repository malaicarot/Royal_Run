using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
// [RequireComponent(typeof(PooledObject))]

public class ItemPool : ObjectPool
{
    public static ItemPool ItemPoolSingleton;


    void Awake()
    {
        if (ItemPoolSingleton == null)
        {
            ItemPoolSingleton = this;
        }
    }

    public ItemMarkPool GetItem(string itemName, Vector3 itemPosition, Quaternion quaternion)
    {
        PooledObject pooledObject = ItemPoolSingleton.GetPooledObject(itemName);
        ItemMarkPool item = pooledObject.GetComponent<ItemMarkPool>();
        item.transform.position = itemPosition;
        item.transform.rotation = quaternion;
        return item;
    }

}
