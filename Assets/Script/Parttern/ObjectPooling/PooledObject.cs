using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPool pool;
    public ObjectPool _pool { get => pool; set => pool = value; }

    public void Release()
    {
        if (_pool != null)
        {
            _pool.ReturnToPool(this);
        }
    }
}
