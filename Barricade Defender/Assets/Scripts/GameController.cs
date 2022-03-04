using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] EnemyPrefab;

    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var enemy = Instantiate(EnemyPrefab[0]);
                float randomYPos = Random.Range(-4f, 4f);
                enemy.transform.position = new Vector2(10f, randomYPos);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var enemy = Instantiate(EnemyPrefab[1]);
                float randomYPos = Random.Range(-4f, 4f);
                enemy.transform.position = new Vector2(10f, randomYPos);
            }
    }
}
