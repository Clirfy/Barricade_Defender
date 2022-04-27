using UnityEngine;

public class AllyArcher : Ally
{
    public float firstAttackDelay;

    private float firstAttackDelayTimer;
    private PlayerController player;

    protected override void Start()
    {
        base.Start();

        player = FindObjectOfType<PlayerController>();
    }

    protected override void Update()
    {
        base.Update();
        Damage = player.Damage / 2;

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
