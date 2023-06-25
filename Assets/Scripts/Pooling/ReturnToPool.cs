using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;

    private ObjectPool<ReturnToPool> _pool;

    private void OnEnable()
    {
        Invoke(nameof(Release), _delay);
    }

    private void Release()
    {
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<ReturnToPool> pool)
    {
        _pool = pool;
    }
}
