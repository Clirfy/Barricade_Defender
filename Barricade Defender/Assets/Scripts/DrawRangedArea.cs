using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRangedArea : MonoBehaviour
{
    public bool isGizmoEnabled;
    public LayerMask Layer;
    public float AttackRangeOffsetX;
    public Vector2 AttackRange;
    public Collider2D[] TargetCollider;

    public Collider2D TargetInRange()
    {
        TargetCollider = Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x + AttackRangeOffsetX, gameObject.transform.position.y), AttackRange, 0f, Layer);

        if (TargetCollider.Length != 0)
        {
            Debug.Log("Target in range : " + TargetCollider[TargetCollider.Length - 1].name);

            return TargetCollider[TargetCollider.Length - 1];
        }

        else
        {
            return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isGizmoEnabled)
            Gizmos.DrawWireCube(new Vector2(gameObject.transform.position.x + AttackRangeOffsetX, gameObject.transform.position.y), AttackRange);
    }
}
