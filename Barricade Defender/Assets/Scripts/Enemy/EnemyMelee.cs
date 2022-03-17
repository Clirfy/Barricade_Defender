using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            attackTimer = Time.time + attackDelay;
            if (target.CompareTag("CampfireBase"))
            {
                FindObjectOfType<BaseCampfire>().TakeDamage(Damage);
            }
            if (target.CompareTag("Player"))
            {
                //hit player;
            }
        }
    }
}
