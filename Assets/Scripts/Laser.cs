using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Vector2 _direction = Vector2.left;
    [SerializeField] private float _distance = 10f;

    private LineRenderer _lineRenderer;
    private bool _isOn;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Toggle(false);
    }

    void Update()
    {
        if (!_isOn)
            return;

        var endPoint = (Vector2)transform.position + (_direction * _distance);

        var firstHit = Physics2D.Raycast(transform.position, _direction, _distance);
        if(firstHit.collider)
        {
            endPoint = firstHit.point;

            var laserDamagable = firstHit.collider.GetComponent<ITakeLaserDamage>();
            laserDamagable?.TakeLaserDamage();
        }

        _lineRenderer.SetPosition(1, endPoint);
    }

    public void Toggle(bool state)
    {
        _isOn = state;
        _lineRenderer.enabled = state;
    }

}
