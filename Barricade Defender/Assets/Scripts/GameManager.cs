using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public bool IsWaveStarted;
    public float TimeToNextEnemySpawn;
    public int EnemySpawnMaxAmmount;

    public Button StartButton;

    private float enemySpawnCounter;
    private int enemySpawnedAmmount;

    private void Update()
    {
        if (IsWaveStarted)
        {
            enemySpawnCounter -= Time.deltaTime;

            if (enemySpawnCounter <= 0f && enemySpawnedAmmount < EnemySpawnMaxAmmount)
            {
                enemySpawnedAmmount++;
                enemySpawnCounter = TimeToNextEnemySpawn;
                SpawnEnemy();
            }
            else if( enemySpawnedAmmount >= EnemySpawnMaxAmmount)
            {
                CheckIsWaveCleared();
            }
        }
    }

    public void StartWave()
    {
        enemySpawnCounter = TimeToNextEnemySpawn;
        enemySpawnedAmmount = 0;
        StartButton.interactable = false;
        IsWaveStarted = true;
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, EnemyPrefab.Length);
        Vector2 randomPos = new Vector2(10f, Random.Range(-4f, 4f));

        Instantiate(EnemyPrefab[randomEnemy], randomPos, Quaternion.identity);
    }

    private bool CheckIsWaveCleared()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0)
        {
            return StartButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("wave not cleared; array of enemies on scene is not empty");
            return StartButton.interactable = false;
        }
    }
}
