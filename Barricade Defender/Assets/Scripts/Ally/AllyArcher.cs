using UnityEngine;

public class AllyArcher : Ally
{
    public float firstAttackDelay;

    private float firstAttackDelayTimer;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            animator.SetBool("isAttacking", true);
            firstAttackDelayTimer -= Time.deltaTime;

            if (firstAttackDelayTimer <= 0f)
            {
                attackTimer = Time.time + attackDelay;
                var bullet = Instantiate(ArrowPrefab, ShootPosition.transform.position, Quaternion.identity);
                bullet.GetComponent<AllyBullet>().TargetPos = target;
                bullet.GetComponent<AllyBullet>().Damage = Damage;
            }
        }

        else if (isAttacking == false)
        {
            animator.SetBool("isAttacking", false);
            firstAttackDelayTimer = firstAttackDelay;
        }
    }
}
