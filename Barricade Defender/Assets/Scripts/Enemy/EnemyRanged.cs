using UnityEngine;

public class EnemyRanged : Enemy
{
    public GameObject BulletPrefab;
    public GameObject ShootPosition;

    private Vector2 shootPosVector;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= attackTimer && isAttacking)
        {
            attackTimer = Time.time + attackDelay;
            Shoot();
        }
    }

    private void Shoot()
    {
        shootPosVector = new Vector2(ShootPosition.transform.position.x, ShootPosition.transform.position.y);

        var bullet = Instantiate(BulletPrefab, shootPosVector, Quaternion.identity);
        //bullet.GetComponent<EnemyBulletController>().TargetTag = "CampfireBase";
        bullet.GetComponent<EnemyBulletController>().Damage = Damage;
    }
}
