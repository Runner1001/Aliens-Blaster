using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    [SerializeField] private bool _onlyFromTop;
    [SerializeField] private float _bounciness = 200f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_onlyFromTop && Vector2.Dot(other.contacts[0].normal, Vector2.down) < 0.5f)
            return;

        var player = other.collider.GetComponent<PlayerAIO>();

        if (player)
        {
            player.Bounce(other.contacts[0].normal, _bounciness);
        }
    }
}
