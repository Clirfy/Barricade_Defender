using UnityEngine;

public class AllyDruid : Ally
{
    public int MisslesCount;
    public float MisslesAttackRate;
    public GameObject SkillPrefab;
    public int SkillTargetCount;

    private int misslesFired;
    private float misslesAttackRateCounter;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill();
        }

        if (Time.time >= attackTimer && isAttacking)
        {
            if (misslesFired < MisslesCount)
            {
                animator.SetBool("isAttacking", true);
                if (misslesAttackRateCounter <= Time.time)
                {
                    var bullet = Instantiate(ArrowPrefab, ShootPosition.transform.position, Quaternion.identity);
                    bullet.GetComponent<AllyBullet>().TargetPos = target;
                    bullet.GetComponent<AllyBullet>().Damage = Damage;

                    misslesAttackRateCounter = MisslesAttackRate + Time.time;
                    misslesFired++;
                }
            }

            else
            {
                attackTimer = Time.time + attackDelay;
                misslesFired = 0; 
                animator.SetBool("isAttacking", false);
            }
        }
    }

    private void Skill()
    {
        //can do complete random targets every time if taken all targets found in range and then get x random items
        Collider2D[] targets = ReturnTargetsInRange.GetTargets(SkillTargetCount);

        foreach (var item in targets)
        {
            Instantiate(SkillPrefab, item.gameObject.transform.position, Quaternion.identity);
        }
    }
}
