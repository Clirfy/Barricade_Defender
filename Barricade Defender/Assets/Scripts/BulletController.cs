using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int BulletSpeed;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime);
    }
}
