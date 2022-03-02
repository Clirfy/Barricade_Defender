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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        HpSlider.maxValue = Hp;
        attackTimer = Time.time;
    }

    private void Update()
    {
        Movement();
        UpdateHpSlider();
        Death();

        if(Time.time >= attackTimer && isAttacking)
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
            Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = false;
        
        if (collision.gameObject.CompareTag("CampfireBase"))
        {
            isMoving = false;
            target = collision.gameObject;
            isAttacking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isMoving = true;
        target = null;
        isAttacking = false;
    }
}
