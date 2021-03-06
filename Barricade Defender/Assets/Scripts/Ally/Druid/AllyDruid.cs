using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllyDruid : Ally
{
    public int MisslesCount;
    public float MisslesAttackRate;
    public GameObject SkillPrefab;
    public int SpellDmg;
    [HideInInspector]
    public int SpellDmgModifier;
    public int SkillTargetCount;
    [Range(0f, 1f)]
    public float SlowPower;
    public TextMeshProUGUI StatsTMP;
    public float spellCd;
    public Slider CdSlider;

    private int misslesFired;
    private float misslesAttackRateCounter;
    [SerializeField]
    private float skillCastingTimer;
    private float spellCdTimer;

    protected override void Start()
    {
        base.Start();

        spellCdTimer = spellCd;
        CdSlider.maxValue = spellCd;
    }

    protected override void Update()
    {
        base.Update();

        UpdateStatsText();
        SpellDmg = Damage * SpellDmgModifier;
        spellCdTimer += Time.deltaTime;
        CdSlider.value = spellCdTimer;

        if (Input.GetKeyDown(KeyCode.Q) && spellCdTimer >= spellCd)
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
        int targetDmgDivider;
        int counter = 0;
        GameObject[] nearestTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .OrderBy(o => Vector2.Distance(o.transform.position, transform.position))
            .ToArray();

        if (nearestTargets.Length == 0)
        {
            Debug.LogWarning("druid skill - no targets found to cast a spell");
            return;
        }

        targetDmgDivider = nearestTargets.Length;

        if (targetDmgDivider > SkillTargetCount)
        {
            targetDmgDivider = SkillTargetCount;
        }

        SpellDmg /= targetDmgDivider;

        foreach (var item in nearestTargets)
        {
            if (item != null && counter < SkillTargetCount)
            {
                Debug.Log(item.name);
                var skill = Instantiate(SkillPrefab, new Vector2(item.transform.position.x - 0.5f, item.transform.position.y), Quaternion.identity);
                skill.GetComponent<AllyDruidSkill>().SlowPower = SlowPower;
                skill.GetComponent<AllyDruidSkill>().Damage = SpellDmg;
                counter++;
                Debug.Log("skill dmg: " + SpellDmg);
                animator.SetBool("isCasting", true);
            }
        }
        spellCdTimer = 0f;
    }

    private IEnumerator CoStopCastingAnim(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isCasting", false);
    }

    private void UpdateStatsText()
    {
        StatsTMP.text = "Level: " + Level +
            "\nDamage: " + Damage +
            "\nSpell Damage: " + SpellDmg +
            "\nSpell Slow Power: " + (SlowPower * 100) + "%" +
            "\nSpell Targets Count: " + SkillTargetCount;
    }
}
