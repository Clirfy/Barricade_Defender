using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTargetsInRange : MonoBehaviour
{
    //public int TargetCount;
    public bool isGizmoEnabled;
    public LayerMask Layer;
    public float AttackRangeOffsetX;
    public Vector2 AttackRange;

    private List<Collider2D> targetsList;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        GetTargets(TargetCount);
    //    }
    //}

    public Collider2D[] GetTargets(int count)
    {
        Collider2D[] targetsFound = Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x + AttackRangeOffsetX, gameObject.transform.position.y), AttackRange, 0f, Layer);

        if (targetsFound != null)
        {
            targetsList = new List<Collider2D>();

            for (int i = 0; i < count && i < targetsFound.Length; i++)
            {
                targetsList.Add(targetsFound[i]);
            }

            return targetsList.ToArray();
        }

        else
        {
            return null;
        }
    }

    Collider2D GetClosestEnemy(Collider2D[] enemies)
    {
        Collider2D bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (isGizmoEnabled)
            Gizmos.DrawWireCube(new Vector2(gameObject.transform.position.x + AttackRangeOffsetX, gameObject.transform.position.y), AttackRange);
    }
}
