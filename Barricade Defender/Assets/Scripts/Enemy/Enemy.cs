using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int Hp;
    public int Damage;
    public int MoveSpeed;
    public Slider HpSlider;

    protected bool isMoving = true;
    protected bool isAttacking = false;
    protected Animator animator;
    protected GameObject target;
    [SerializeField]
    protected float attackDelay;
    protected float attackTimer;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        HpSlider.maxValue = Hp;
        attackTimer = Time.time;
    }

    protected virtual void Update()
    {
        Movement();
        UpdateHpSlider();
        Death();

        if (Time.time >= attackTimer && isAttacking)
        {
            target.GetComponent<PlayerStats>().TakeDamage(Damage);
            attackTimer = Time.time + attackDelay;
        }
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

    protected void Death()
    {
        if (Hp <= 0)
        {
            isMoving = false;
            isAttacking = false;
            Destroy(gameObject, 1f);
            animator.SetBool("IsDying", true);
            var collider = GetComponent<CapsuleCollider2D>();
            collider.enabled = false;
        }
    }
}
