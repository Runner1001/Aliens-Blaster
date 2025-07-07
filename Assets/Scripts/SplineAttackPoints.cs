using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineAttackPoints : MonoBehaviour
{
    [SerializeField] SplineContainer splineContainer;
    [SerializeField] List<float> attackPoints;

    public Queue<float> GetAsQueue()
    {
        return new Queue<float>(attackPoints);
    }

    void OnDrawGizmos()
    {
        foreach(var point in attackPoints)
        {
            Vector3 position = splineContainer.EvaluatePosition(point);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position, 0.2f);
        }
    }
}
