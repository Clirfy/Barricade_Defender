using UnityEngine;
using UnityEngine.UI;

public class TEST_EnemyController : MonoBehaviour
{
    public int Hp;
    public int Damage;
    public int MoveSpeed;

    public Slider HpSlider;
    //public Text HpText;

    private Rigidbody2D rb;
    private bool isMoving = true;
    private bool isAttacking = false;
    [SerializeField]
    private float attackDelay;
    private float attackTimer;
    private GameObject target;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        HpSlider.maxValue = Hp;
        attackTimer = Time.time;
    }

    private void Update()
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

    private void UpdateHpSlider()
    {
        HpSlider.value = Hp;
    }

    private void Death()
    {
        if (Hp <= 0)
        {
            isMoving = false;
            Destroy(gameObject, 1f);
            animator.SetBool("IsDying", true);
            var collider = GetComponent<CapsuleCollider2D>();
            collider.enabled = false;
        }
    }

    private void Movement()
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        isMoving = false;

        if (collision.gameObject.CompareTag("CampfireBase"))
        {
            target = collision.gameObject;
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isMoving = true;
        target = null;
        isAttacking = false;

        animator.SetBool("IsAttacking", false);
    }
}
