using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public int BulletSpeed;
    public int Damage;
    //public string TargetTag;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(Vector2.left * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CampfireBase"))
        {
            Debug.Log(collision.name + " hit for " + Damage + " damage");

            collision.GetComponent<BaseCampfire>().TakeDamage(Damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.name + " hit for " + Damage + " damage");

            collision.GetComponent<PlayerController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
