using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] float _footOffset = 0.5f;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _waterLayerMask;
    [SerializeField] float _groundCheckOffset = 0.5f;

    private bool _isGrounded;
    private bool _isOnSnow;
    private bool _isInWater;
    RaycastHit2D[] results = new RaycastHit2D[100];

    public bool IsGrounded => _isGrounded;
    public bool IsOnSnow => _isOnSnow;
    public bool IsInWater => _isInWater;

    void Update()
    {
        UpdateGround();
    }

    private void UpdateGround()
    {
        _isGrounded = false;
        _isOnSnow = false;
        _isInWater = false;

        //Check center
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _groundCheckOffset);
        CheckGrounding(origin);

        //Check left
        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - _groundCheckOffset);
        CheckGrounding(origin);

        //Check right
        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - _groundCheckOffset);
        CheckGrounding(origin);
    }

    private void CheckGrounding(Vector2 origin)
    {
        int hits = Physics2D.Raycast(origin, Vector2.down, 
            new ContactFilter2D() { layerMask = _groundLayerMask, useLayerMask = true, useTriggers = true }, results, 0.1f);

        for (int i = 0; i < hits; i++)
        {
            var hit = results[i];

            if (!hit.collider)
                continue;

            _isGrounded = true;
            _isOnSnow |= hit.collider.CompareTag("Snow");
        }

        var water = Physics2D.OverlapPoint(origin, _waterLayerMask);
        if(water != null)
        {
            _isInWater = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _groundCheckOffset);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - _groundCheckOffset);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - _groundCheckOffset);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        
    }

    
}
