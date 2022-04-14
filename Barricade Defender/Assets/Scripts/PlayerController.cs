using System;
using System.Collections;
using UnityEngine;

public class PlayerController : PlayerStats
{
    public GameObject BulletPrefab;
    public GameObject ShootPosition;
    public int Damage;
    public float MeleeAttackTime;

    [SerializeField]
    private int moveSpeed;

    private Animator animator;
    private float lastXPos;
    private Rigidbody2D rb;
    private Vector2 shootPosVector;
    private bool canAttack = true;
    private bool canMove = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HpCurrent = HpMax;
    }

    void Update()
    {
        MeleeAttack();
        MeleeSpell();
        //Shoot();

        if (canMove == false)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        Movement();
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }

        if (horizontalInput > 0f)
        {
            lastXPos = 1f;
        }

        if (horizontalInput < 0f)
        {
            lastXPos = -1f;
        }

        animator.SetFloat("xPos", lastXPos);
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        gameObject.transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -19f, 19f),
            Mathf.Clamp(transform.position.y, -8, 2));
    }

    private void Shoot()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (Input.GetMouseButtonDown(0))
        {
            shootPosVector = new Vector2(ShootPosition.transform.position.x, ShootPosition.transform.position.y);
            

            var bullet = Instantiate(BulletPrefab, shootPosVector, Quaternion.identity);
            bullet.GetComponent<BulletController>().TargetTag = "Enemy";
            bullet.GetComponent<BulletController>().Damage = Damage;
            bullet.transform.right = direction;
        }
    }

    private void MeleeAttack()
    {
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
            canMove = false;
            canAttack = false;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isAttackingMelee", true);
            StartCoroutine(CoDoAttack(MeleeAttackTime, "isAttackingMelee"));
        }
    }

    private IEnumerator CoDoAttack(float attackTime, string attackTypeName)
    {
        yield return new WaitForSeconds(attackTime);
        canMove = true;
        canAttack = true;
        animator.SetBool(attackTypeName, false);
        animator.SetBool("isAttacking", false);
    }

    private void MeleeSpell()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (Input.GetMouseButtonDown(2) && canAttack)
        {
            canMove = false;
            canAttack = false;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isAttackingSpell", true);
            StartCoroutine(CoDoAttack(MeleeAttackTime, "isAttackingSpell"));
            shootPosVector = new Vector2(ShootPosition.transform.position.x, ShootPosition.transform.position.y);

            var bullet = Instantiate(BulletPrefab, shootPosVector, Quaternion.identity);
            bullet.GetComponent<BulletController>().TargetTag = "Enemy";
            bullet.GetComponent<BulletController>().Damage = Damage;
            bullet.transform.right = direction;
        }
    }
}
