using UnityEngine;

public class AllyArcher : Ally
{
    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            attackTimer = Time.time + attackDelay;
            var bullet = Instantiate(ArrowPrefab, ShootPosition.transform.position, Quaternion.identity);
            bullet.GetComponent<AllyBulletController>().TargetPos = target;
            bullet.GetComponent<AllyBulletController>().Damage = Damage;
        }
    }
}
