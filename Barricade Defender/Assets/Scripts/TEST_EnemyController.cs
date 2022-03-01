using UnityEngine;

public class TEST_EnemyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log(gameObject.name + " Hit");
        }
    }
}
