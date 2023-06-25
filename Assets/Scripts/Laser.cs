using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Vector2 _direction = Vector2.left;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private SpriteRenderer _laserBurst;
    [SerializeField] private LineRenderer _lineRenderer;

    private bool _isOn;

    void Awake()
    {       
        Toggle(false);
        
    }

    void Update()
    {
        if (!_isOn)
        {
            _laserBurst.enabled = false;
            return;
        }

        var endPoint = (Vector2)transform.position + (_direction * _distance);

        var firstHit = Physics2D.Raycast(transform.position, _direction, _distance);
        if(firstHit.collider)
        {
            endPoint = firstHit.point;
            _laserBurst.transform.position = endPoint;
            _laserBurst.enabled = true;
            _laserBurst.transform.localScale = Vector3.one * (0.5f + Mathf.PingPong(Time.time, 1f));

            var laserDamagable = firstHit.collider.GetComponent<ITakeLaserDamage>();
            laserDamagable?.TakeLaserDamage();
        }
        else
        {
            _laserBurst.enabled = false;
        }

        _lineRenderer.SetPosition(1, endPoint);
    }

    void OnValidate()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, transform.position);

        var endPoint = (Vector2)transform.position + (_direction * _distance);

        var firstHit = Physics2D.Raycast(transform.position, _direction, _distance);
        if (firstHit.collider)
            endPoint = firstHit.point;

        _lineRenderer.SetPosition(1, endPoint);
        _laserBurst.transform.position = endPoint;
    }

    public void Toggle(bool state)
    {
        _isOn = state;
        _lineRenderer.enabled = state;
    }

}
