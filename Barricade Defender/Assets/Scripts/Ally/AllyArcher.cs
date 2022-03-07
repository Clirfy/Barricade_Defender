using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyArcher : MonoBehaviour
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

    private void Start()
    {
        animator = GetComponent<Animator>();
        DrawAttackArea = GetComponent<DrawAttackArea>();

        attackTimer = Time.time;
    }

    private void Update()
    {
        Attack();

        if (Time.time >= attackTimer && isAttacking)
        {
            attackTimer = Time.time + attackDelay;
            var bullet = Instantiate(ArrowPrefab, ShootPosition.transform.position, Quaternion.identity);
            bullet.GetComponent<AllyBulletController>().TargetPos = target;
            bullet.GetComponent<AllyBulletController>().Damage = Damage;
        }
    }

    private void Attack()
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
