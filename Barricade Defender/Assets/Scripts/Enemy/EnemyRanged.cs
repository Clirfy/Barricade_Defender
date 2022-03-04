using UnityEngine;

public class EnemyRanged : Enemy
{
    public GameObject BulletPrefab;
    public GameObject ShootPosition;

    private Vector2 shootPosVector;
    private DrawRangedArea DrawRangedArea;

    private void Awake()
    {
        DrawRangedArea = GetComponent<DrawRangedArea>();
    }

    private void FixedUpdate()
    {
        DrawRangedArea.TargetInRange();
    }

    protected override void Update()
    {
        Attack();
        Movement();
        UpdateHpSlider();
        Death();

        if (Time.time >= attackTimer && isAttacking)
        {
            attackTimer = Time.time + attackDelay;
            Shoot();
        }
    }

    private void Attack()
    {
        if (DrawRangedArea.TargetInRange() != null)
        {
            target = DrawRangedArea.TargetInRange().gameObject;
            isMoving = false;
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
        }

        else
        {
            isMoving = true;
            target = null;
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
        }
    }

    private void Shoot()
    {
        shootPosVector = new Vector2(ShootPosition.transform.position.x, ShootPosition.transform.position.y);

        var bullet = Instantiate(BulletPrefab, shootPosVector, Quaternion.identity);
        bullet.GetComponent<EnemyBulletController>().TargetTag = "CampfireBase";
        bullet.GetComponent<EnemyBulletController>().Damage = Damage;
    }
}
