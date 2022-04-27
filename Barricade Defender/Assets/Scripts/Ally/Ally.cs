using UnityEngine;

public class Ally : MonoBehaviour
{
    public int Level = 1;
    public int Damage;
    public GameObject ArrowPrefab;
    public GameObject ShootPosition;

    protected bool isAttacking = false;
    protected Animator animator;
    protected GameObject target;
    [SerializeField]
    protected float attackDelay;
    protected float attackTimer;
    protected DrawAttackArea DrawAttackArea;
    protected bool isWaitingToAttack = true;
    protected ReturnTargetsInRange returnTargetsInRange;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        DrawAttackArea = GetComponent<DrawAttackArea>();
        returnTargetsInRange = GetComponent<ReturnTargetsInRange>();

        attackTimer = Time.time;
        animator.SetBool("isAttacking", false);
        Level = 1;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            Attack();
        }
    }

    protected void Attack()
    {
        if (DrawAttackArea.TargetInRange() != null)
        {
            isAttacking = true;
            target = DrawAttackArea.TargetInRange().gameObject;
        }

        else
        {
            target = null;
            isAttacking = false;
        }
    }
}
