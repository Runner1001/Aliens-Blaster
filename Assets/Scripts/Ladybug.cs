using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ladybug : MonoBehaviour, ITakeLaserDamage
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _raycastDistance = 0.2f;
    [SerializeField] private LayerMask _forwardRaycastLayerMask;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Collider2D _col;
    private Vector2 _direction = Vector2.left;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }

    void Update()
    {
        CheckGroundInFront();
        CheckInFront();

        _rb.velocity = new Vector2(_direction.x * _speed, _rb.velocity.y);
    }

    private void CheckInFront()
    {
        Vector2 offset = _direction * _col.bounds.extents.x;
        Vector2 origin = (Vector2)transform.position + offset;


        var hits = Physics2D.RaycastAll(origin, _direction, _raycastDistance, _forwardRaycastLayerMask);

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                _direction *= -1;
                _sr.flipX = _direction == Vector2.right;
                break;
            }
        }
    }

    private void CheckGroundInFront()
    {
        bool canContinueWalking = false;

        var downOrigin = GetDownRayPosition(_col);
        var downHits = Physics2D.RaycastAll(downOrigin, Vector2.down, _raycastDistance);
        foreach (var hit in downHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
                canContinueWalking = true;
        }

        if (!canContinueWalking)
        {
            _direction *= -1;
            _sr.flipX = _direction == Vector2.right;
            return;
        }
    }

    void OnDrawGizmos()
    {
        var collider = GetComponent<Collider2D>();

        Vector2 offset = _direction * collider.bounds.extents.x;
        Vector2 origin = (Vector2)transform.position + offset;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + (_direction * _raycastDistance));

        var downOrigin = GetDownRayPosition(collider);
        Gizmos.DrawLine(downOrigin, downOrigin + (Vector2.down * _raycastDistance));
    }

    private Vector2 GetDownRayPosition(Collider2D collider)
    {
        var bounds = collider.bounds;

        if (_direction == Vector2.left)
            return new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
        else
            return new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);
    }

    public void TakeLaserDamage()
    {
        _rb.velocity = Vector2.zero;
    }
}
