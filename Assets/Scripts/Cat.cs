using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private CatBomb _catBombPrefab;
    [SerializeField] private Transform _firePoint;

    private CatBomb _catBomb;

    void Start()
    {
        SpawnCatBomb();
        var shootAnimationWrapper = GetComponentInChildren<ShootAnimationWrapper>();
        shootAnimationWrapper.OnShoot += ShootCatBomb;
        shootAnimationWrapper.OnReload += SpawnCatBomb;
    }

    private void SpawnCatBomb()
    {
        if (_catBomb == null)
            _catBomb = Instantiate(_catBombPrefab, _firePoint);
    }

    private void ShootCatBomb()
    {
        _catBomb.Lunch(Vector2.up + Vector2.left);
        _catBomb = null;
    }
}
