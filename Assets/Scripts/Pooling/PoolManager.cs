using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private BlasterShot _blasterShotPrefab;
    [SerializeField] private ReturnToPool _blasterImpactExplosionPrefab;
    [SerializeField] private CatBomb _catBombPrefab;
    [SerializeField] private ReturnToPool _spikePrefab;

    ObjectPool<BlasterShot> _blasterShotPool;
    ObjectPool<ReturnToPool> _blasterImpactExplosionPool;
    ObjectPool<CatBomb> _catBombPool; 
    ObjectPool<ReturnToPool> _spikePool; 

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

        _catBombPool = new ObjectPool<CatBomb>(() =>
        {
            var bomb = Instantiate(_catBombPrefab);
            bomb.SetPool(_catBombPool);
            return bomb;
        },
        t => t.gameObject.SetActive(true),
        t => t.gameObject.SetActive(false));

        _spikePool = new ObjectPool<ReturnToPool>(() =>
        {
            var shot = Instantiate(_spikePrefab);
            shot.SetPool(_spikePool);
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

    public CatBomb GetCatBomb()
    {
        return _catBombPool.Get();
    }

    public ReturnToPool GetSpike()
    {
        return _spikePool.Get();
    }
}
