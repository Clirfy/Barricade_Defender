using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int Hp;
    public int Damage;
    public float MoveSpeed;
    public Slider HpSlider;
    public GameObject deathAnim;
    public int MoneyReward;

    protected bool isMoving = true;
    protected bool isAttacking = false;
    protected Animator animator;
    protected GameObject target;
    [SerializeField]
    protected float attackDelay;
    protected float attackTimer;
    protected Rigidbody2D rb;
    protected DrawAttackArea DrawAttackArea;
    protected bool isWaitingToAttack = true;
    protected WaveManager waveManager;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DrawAttackArea = GetComponent<DrawAttackArea>();
        waveManager = FindObjectOfType<WaveManager>();

        HpSlider.maxValue = Hp;
        attackTimer = Time.time;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        ScaleStatsWithWaveLevel(waveManager.WaveLevel);
    }

    protected virtual void Update()
    {
        if (Hp <= 0)
        {
            DeathTest();
            return;
        }

        Movement();
        UpdateHpSlider();
        Attack();
    }

    public int TakeDamage(int damage)
    {
        Hp -= damage;
        return Hp;
    }

    protected void UpdateHpSlider()
    {
        HpSlider.value = Hp;
    }

    protected void Movement()
    {
        if (isMoving)
        {
            rb.velocity = Vector2.left * MoveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected void DeathTest()
    {
        var death = Instantiate(deathAnim, transform.position, Quaternion.identity);
        death.GetComponent<Animator>().SetBool("IsDying", true);

        FindObjectOfType<BaseCampfire>().GetMoney(MoneyReward);
        waveManager.SubtractEnemiesLeft();
        Destroy(gameObject);
    }

    private void Attack()
    {
        if (DrawAttackArea.TargetInRange() != null)
        {
            if (isWaitingToAttack)
            {
                attackTimer = Time.time + attackDelay;
                isWaitingToAttack = false;
            }

            target = DrawAttackArea.TargetInRange().gameObject;
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
            isWaitingToAttack = true;
        }
    }

    private void ScaleStatsWithWaveLevel(int waveLevel)
    {
        Hp = Hp + waveLevel;
        Damage = Damage + waveLevel / 2;
    }
}
