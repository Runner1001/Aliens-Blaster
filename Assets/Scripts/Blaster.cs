using UnityEngine;

public class Blaster : MonoBehaviour, IItem
{
    [SerializeField] private BlasterShot _blasetShotPrefab;
    [SerializeField] private Transform _firePoint;

    private PlayerAIO _playerAIO;


    void Awake()
    {
        _playerAIO = GetComponentInParent<PlayerAIO>();
    }

    private void Fire()
    {
        BlasterShot shot = PoolManager.Instance.GetBlasterShot();
        shot.Launch(_playerAIO.Direction, _firePoint.position);
    }

    public void Use()
    {
        if(GameManager.CinematicPlaying == false)
            Fire();
    }
}
