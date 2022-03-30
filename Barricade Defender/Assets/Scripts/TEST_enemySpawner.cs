using UnityEngine;

public class TEST_enemySpawner : MonoBehaviour
{
    public Vector2 YposRange;
    [SerializeField]
    private float XposSpawn;
    public GameObject[] enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector2 randomPos = new Vector2(XposSpawn, Random.Range(YposRange.x, YposRange.y));

            Instantiate(enemy[0], randomPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector2 randomPos = new Vector2(XposSpawn, Random.Range(YposRange.x, YposRange.y));

            Instantiate(enemy[1], randomPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector2 randomPos = new Vector2(XposSpawn, Random.Range(YposRange.x, YposRange.y));

            Instantiate(enemy[2], randomPos, Quaternion.identity);
        }
    }
}
