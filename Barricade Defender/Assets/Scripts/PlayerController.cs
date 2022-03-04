using System;
using UnityEngine;

public class PlayerController : PlayerStats
{
    public GameObject BulletPrefab;
    public GameObject ShootPosition;
    public int Damage;

    [SerializeField]
    private int moveSpeed;

    private Rigidbody2D rb;
    private Vector2 shootPosVector;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HpCurrent = HpMax;
    }

    void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(0f, 0f);

        if (horizontalInput > 0.5f || horizontalInput < -0.5f)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        if (verticalInput > 0.5f || verticalInput < -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * moveSpeed);
        }
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
}
