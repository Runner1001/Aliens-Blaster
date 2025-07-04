using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _pos1;
    [SerializeField] private Vector3 _pos2;
    [SerializeField] private float _speed = 0.1f;

    [Range(0f, 1f)]
    [SerializeField] private float _pct;

    [ContextMenu(nameof(SetPosition1))] public void SetPosition1() => _pos1 = transform.position;
    [ContextMenu(nameof(SetPosition2))] public void SetPosition2() => _pos2 = transform.position;

    void Update()
    {
        _pct = Mathf.PingPong(Time.time * _speed, 1f);
        transform.position = Vector3.Lerp(_pos1, _pos2, _pct);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerAIO>();

        if (player != null)
            player.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerAIO>();

        if (player != null)
            player.transform.SetParent(null);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var collider = GetComponent<BoxCollider2D>();
        Gizmos.DrawWireCube(_pos1, collider.bounds.size);
        Gizmos.DrawWireCube(_pos2, collider.bounds.size);

        Gizmos.color = Color.blue;
        var currentPosition = Vector3.Lerp(_pos1, _pos2, _pct);
        Gizmos.DrawWireCube(currentPosition, collider.bounds.size);
    }
}
