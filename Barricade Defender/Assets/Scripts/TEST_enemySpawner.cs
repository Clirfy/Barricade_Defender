using UnityEngine;

public class TEST_enemySpawner : MonoBehaviour
{
    public GameObject[] enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector2 randomPos = new Vector2(10f, Random.Range(-4f, 4f));

            Instantiate(enemy[0], randomPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector2 randomPos = new Vector2(10f, Random.Range(-4f, 4f));

            Instantiate(enemy[1], randomPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector2 randomPos = new Vector2(10f, Random.Range(-4f, 4f));

            Instantiate(enemy[2], randomPos, Quaternion.identity);
        }
    }
}
