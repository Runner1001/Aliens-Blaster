using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] float _footOffset = 0.5f;
    [SerializeField] private LayerMask _groundLayerMask;

    private SpriteRenderer _sr;
    private bool _isGrounded;
    private bool _isOnSnow;

    public bool IsGrounded => _isGrounded;
    public bool IsOnSnow => _isOnSnow;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateGround();
    }

    private void UpdateGround()
    {
        _isGrounded = false;
        _isOnSnow = false;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _sr.bounds.extents.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _groundLayerMask);
        if (hit.collider)
        {
            _isGrounded = true;
            _isOnSnow = hit.collider.CompareTag("Snow");
        }

        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - _sr.bounds.extents.y);
        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _groundLayerMask);
        if (hit.collider)
        {
            _isGrounded = true;
            _isOnSnow = hit.collider.CompareTag("Snow");
        }

        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - _sr.bounds.extents.y);
        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _groundLayerMask);
        if (hit.collider)
        {
            _isGrounded = true;
            _isOnSnow = hit.collider.CompareTag("Snow");
        }
    }

    void OnDrawGizmos()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Gizmos.color = Color.blue;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }
}
