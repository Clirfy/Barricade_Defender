using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Press 1 to spawn EnemyPrefab")]
    public GameObject EnemyPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var enemy = Instantiate(EnemyPrefab);
            float randomYPos = Random.Range(-4f, 4f);
            enemy.transform.position = new Vector2(10f, randomYPos);
        }
    }
}
