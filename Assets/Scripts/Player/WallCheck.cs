using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] float wallDetectionDistance;
    [SerializeField] int numberOfPoints = 3;
    [SerializeField] PlayerMovement player;
    [SerializeField] float buffer;
    [SerializeField] LayerMask layerMask;

    RaycastHit2D[] results = new RaycastHit2D[100];

    public bool IsTouchingRightWall { get; private set; }
    public bool IsTouchingLeftWall { get; private set; }

    void Update()
    {
        UpdateWallTouching();
    }

    private void UpdateWallTouching()
    {
        IsTouchingRightWall = CheckForWall(Vector2.right);
        IsTouchingLeftWall = CheckForWall(Vector2.left);
    }

    private void DrawGizmosForSide(Vector2 direction)
    {
        if (player == null)
            return;

        var activeCollider = player.IsDucking ? player.DuckCollider : player.StandingCollider;
        float colliderHeight = activeCollider.bounds.size.y - 2 * buffer;
        float segmentSize = colliderHeight / (numberOfPoints - 1);

        for (int i = 0; i < numberOfPoints; i++)
        {
            var origin = transform.position - new Vector3(0, activeCollider.bounds.size.y / 2f, 0);
            origin += new Vector3(0, buffer + segmentSize * i, 0);
            origin += (Vector3)direction * wallDetectionDistance;
            Gizmos.DrawWireSphere(origin, 0.05f);
        }
    }

    private bool CheckForWall(Vector2 direction)
    {
        var activeCollider = player.IsDucking ? player.DuckCollider : player.StandingCollider;
        float colliderHeight = activeCollider.bounds.size.y - 2 * buffer;
        float segmentSize = colliderHeight / (numberOfPoints - 1);

        for (int i = 0; i < numberOfPoints; i++)
        {
            var origin = transform.position - new Vector3(0, activeCollider.bounds.size.y / 2f, 0);
            origin += new Vector3(0, buffer + segmentSize * i, 0);
            origin += (Vector3)direction * wallDetectionDistance;

            int hits = Physics2D.Raycast(origin, direction,
            new ContactFilter2D() { layerMask = layerMask, useLayerMask = true, useTriggers = true }, results, 0.1f);

            for (int hitIndex = 0; hitIndex < hits; hitIndex++)
            {
                var hit = results[hitIndex];
                if (hit.collider?.isTrigger == false)
                    return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        DrawGizmosForSide(Vector2.right);
        DrawGizmosForSide(Vector2.left);
    }
}