using System.Linq;
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
        GameObject[] nearestTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .OrderBy(o => Vector2.Distance(o.transform.position, transform.position))
            .ToArray();

        for (int i = 0; i < SkillTargetCount; i++)
        {
            Debug.Log(nearestTargets[i].name);
            Instantiate(SkillPrefab, nearestTargets[i].transform.position, Quaternion.identity);
        }
    }
}
