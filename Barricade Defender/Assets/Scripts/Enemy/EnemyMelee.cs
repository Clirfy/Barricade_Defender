using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            attackTimer = Time.time + attackDelay;
            target.GetComponent<PlayerStats>().TakeDamage(Damage);
        }
    }
}
