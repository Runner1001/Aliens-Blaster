using UnityEngine;

public class Blaster : MonoBehaviour, IItem
{
    [SerializeField] private BlasterShot _blasetShotPrefab;
    [SerializeField] private Transform _firePoint;

    private PlayerMovement _playerMovement;


    void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Fire()
    {
        BlasterShot shot = PoolManager.Instance.GetBlasterShot();
        shot.Launch(_playerMovement.Direction, _firePoint.position);
    }

    public void Use()
    {
        if(GameManager.CinematicPlaying == false)
            Fire();
    }
}
