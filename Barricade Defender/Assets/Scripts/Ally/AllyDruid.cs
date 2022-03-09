using System.Linq;
using UnityEngine;

public class AllyDruid : Ally
{
    public int MisslesCount;
    public float MisslesAttackRate;
    public GameObject SkillPrefab;
    public int SkillTargetCount;
    [Range(0f, 1f)]
    public float SlowPower;

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
        int counter = 0;
        GameObject[] nearestTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .OrderBy(o => Vector2.Distance(o.transform.position, transform.position))
            .ToArray();

        foreach (var item in nearestTargets)
        {
            if (item != null && counter < SkillTargetCount)
            {
                Debug.Log(item.name);
                var skill = Instantiate(SkillPrefab, item.transform.position, Quaternion.identity);
                skill.GetComponent<AllyDruidSkill>().SlowPower = SlowPower;
                counter++;
            }
        }
    }
}
