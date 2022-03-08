using UnityEngine;

public class AllyArcher : Ally
{
    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            animator.SetBool("isAttacking", true);
            attackTimer = Time.time + attackDelay;
            var bullet = Instantiate(ArrowPrefab, ShootPosition.transform.position, Quaternion.identity);
            bullet.GetComponent<AllyBullet>().TargetPos = target;
            bullet.GetComponent<AllyBullet>().Damage = Damage;
        }

        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
