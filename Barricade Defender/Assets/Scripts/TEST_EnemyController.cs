using UnityEngine;

public class TEST_EnemyController : MonoBehaviour
{
    public int MoveSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = Vector2.left * MoveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log(gameObject.name + " Hit");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
