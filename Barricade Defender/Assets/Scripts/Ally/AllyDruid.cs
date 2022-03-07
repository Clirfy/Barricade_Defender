using UnityEngine;

public class AllyDruid : Ally
{
    public int MisslesCount;
    public float MisslesAttackRate;

    private int misslesFired;
    private float misslesAttackRateCounter;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            if (misslesFired < MisslesCount)
            {
                if (misslesAttackRateCounter <= Time.time)
                {
                    var bullet = Instantiate(ArrowPrefab, ShootPosition.transform.position, Quaternion.identity);
                    bullet.GetComponent<AllyBulletController>().TargetPos = target;
                    bullet.GetComponent<AllyBulletController>().Damage = Damage;

                    misslesAttackRateCounter = MisslesAttackRate + Time.time;
                    misslesFired++;
                }
            }
            else
            {
                attackTimer = Time.time + attackDelay;
                misslesFired = 0;
            }
        }
    }
}
