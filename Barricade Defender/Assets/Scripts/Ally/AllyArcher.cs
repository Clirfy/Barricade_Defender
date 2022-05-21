using TMPro;
using UnityEngine;

public class AllyArcher : Ally
{
    public float firstAttackDelay;
    public TextMeshProUGUI StatsTMP;

    [SerializeField]
    private int levelUpCost;
    [SerializeField]
    private TextMeshProUGUI levelUpText;
    private BaseCampfire baseCampfire;
    private float firstAttackDelayTimer;
    private PlayerController player;

    protected override void Start()
    {
        base.Start();

        player = FindObjectOfType<PlayerController>();
        baseCampfire = FindObjectOfType<BaseCampfire>();
    }

    protected override void Update()
    {
        base.Update();
        //Damage = player.Damage / 2;

        UpdateStatsText();

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

    private void UpdateStatsText()
    {
        StatsTMP.text = "Level: " + Level +
            "\nDamage: " + Damage;
    }

    public void LevelUp()
    {
        if (levelUpCost <= baseCampfire.Money)
        {
            baseCampfire.TakeMoney(levelUpCost);
            levelUpCost = Mathf.RoundToInt(levelUpCost * 1.5f);
            Damage += 1;
            Level += 1;
            levelUpText.text = levelUpCost.ToString();
        }
        else
        {
            Debug.LogWarning("not enough gold to level up");
        }
    }
}
