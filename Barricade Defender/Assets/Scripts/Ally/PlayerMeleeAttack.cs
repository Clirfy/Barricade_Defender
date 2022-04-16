using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [HideInInspector]
    public int Damage;

    private void Start()
    {
        Destroy(gameObject, .1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage);
            Debug.Log("Player melee attack for: " + Damage);
        }
    }
}
