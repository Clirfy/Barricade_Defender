using UnityEngine;

public class Ally : MonoBehaviour
{
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

    protected void Start()
    {
        animator = GetComponent<Animator>();
        DrawAttackArea = GetComponent<DrawAttackArea>();

        attackTimer = Time.time;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (DrawAttackArea.TargetInRange() != null)
        {
            if (isWaitingToAttack)
            {
                attackTimer = Time.time + attackDelay;
                isWaitingToAttack = false;
            }

            target = DrawAttackArea.TargetInRange().gameObject;
            isAttacking = true;
            animator.SetBool("isShooting", true);
        }

        else
        {
            target = null;
            isAttacking = false;
            animator.SetBool("isShooting", false);
            isWaitingToAttack = true;
        }
    }
}
