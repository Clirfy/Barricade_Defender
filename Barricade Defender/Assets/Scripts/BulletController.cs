using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int BulletSpeed;
    public int Damage;
    public string TargetTag;
    public int effectId;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            Debug.Log(collision.name + " hit for " + Damage + " damage");

            collision.GetComponent<Enemy>().TakeDamage(Damage);

            switch (effectId)
            {
                case 0:
                    Destroy(gameObject);
                    break;

                case 1:
                    break;

                default:
                    break;
            }
        }
    }
}
