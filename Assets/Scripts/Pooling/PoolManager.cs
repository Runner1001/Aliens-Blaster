using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private BlasterShot _blasterShotPrefab;
    [SerializeField] private ReturnToPool _blasterImpactExplosionPrefab;

    ObjectPool<BlasterShot> _blasterShotPool;
    ObjectPool<ReturnToPool> _blasterImpactExplosionPool;

    public static PoolManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        _blasterShotPool = new ObjectPool<BlasterShot>(AddNewBlasterShotToPool, t => t.gameObject.SetActive(true), t => t.gameObject.SetActive(false));
        _blasterImpactExplosionPool = new ObjectPool<ReturnToPool>(() =>
        {
            var shot = Instantiate(_blasterImpactExplosionPrefab);
            shot.SetPool(_blasterImpactExplosionPool);
            return shot;
        },
        t => t.gameObject.SetActive(true),
        t => t.gameObject.SetActive(false));
        
    }

    private BlasterShot AddNewBlasterShotToPool()
    {
        var shot = Instantiate(_blasterShotPrefab);
        shot.SetPool(_blasterShotPool);
        return shot;
    }

    public BlasterShot GetBlasterShot()
    {
        return _blasterShotPool.Get();
    }

    public ReturnToPool GetBlasterShotExplosion(Vector2 point)
    {
        var explosion = _blasterImpactExplosionPool.Get();
        explosion.transform.position = point;
        return explosion;
    }
}
