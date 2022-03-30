using System.Collections;
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
    [SerializeField]
    private float skillCastingTimer;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill();
            StartCoroutine(CoStopCastingAnim(skillCastingTimer));
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
        else if (isAttacking == false)
        {
            animator.SetBool("isAttacking", false);
            misslesFired = 0;
        }
    }

    private void Skill()
    {
        int spellDmg = Damage * 2;
        int targetDmgDivider;
        int counter = 0;
        GameObject[] nearestTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .OrderBy(o => Vector2.Distance(o.transform.position, transform.position))
            .ToArray();

        targetDmgDivider = nearestTargets.Length;

        if (targetDmgDivider > SkillTargetCount)
        {
            targetDmgDivider = SkillTargetCount;
        }

        spellDmg /= targetDmgDivider;

        foreach (var item in nearestTargets)
        {
            if (item != null && counter < SkillTargetCount)
            {
                Debug.Log(item.name);
                var skill = Instantiate(SkillPrefab, new Vector2(item.transform.position.x - 0.5f, item.transform.position.y), Quaternion.identity);
                skill.GetComponent<AllyDruidSkill>().SlowPower = SlowPower;
                skill.GetComponent<AllyDruidSkill>().Damage = spellDmg;
                counter++;
                Debug.Log("skill dmg: " + spellDmg);
                animator.SetBool("isCasting", true);
            }
        }
    }

    private IEnumerator CoStopCastingAnim(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isCasting", false);
    }
}
