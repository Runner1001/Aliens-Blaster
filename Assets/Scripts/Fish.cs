using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class Fish : MonoBehaviour
{
    [SerializeField] SplineAnimate splineAnimate;
    [SerializeField] Animator animator;
    [SerializeField] SplineAttackPoints splineAttackPoints;
    [SerializeField] int spikeCount = 3;
    [SerializeField] float spread = 15f;
    [SerializeField] float origin = 0f;
    [SerializeField] float fireSpeed = 10f;

    float nextAttackPoint;
    Queue<float> attackPoints;

    void Start()
    {
        GetComponentInChildren<ShootAnimationWrapper>().OnShoot += ShootSpikes;
        RefreshAttackPoints();
    }

    private void ShootSpikes()
    {
        for (int i = 0; i < spikeCount; i++)
        {
            var angle = i - (spikeCount / 2);
            var offset = spread * angle;
            var finalAngle = origin + offset;
            var spike = PoolManager.Instance.GetSpike();
            spike.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, finalAngle));
            spike.GetComponent<Rigidbody2D>().velocity = spike.transform.right * fireSpeed;
        }
    }

    void Update()
    {
        var elapsed = splineAnimate.NormalizedTime % 1;
        if (elapsed >= nextAttackPoint)
        {
            animator.SetTrigger("Attack");

            if (attackPoints.Any())
                nextAttackPoint = attackPoints.Dequeue();
            else
                RefreshAttackPoints();
        }
    }

    private void RefreshAttackPoints()
    {
        attackPoints = splineAttackPoints.GetAsQueue();
        nextAttackPoint = attackPoints.Dequeue();
    }
}
