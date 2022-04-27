using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public bool IsWaveStarted;
    public int EnemySpawnMaxAmmount;
    [HideInInspector]
    public int WaveLevel = 1;
    public Button StartButton;
    public TextMeshProUGUI WaveLevelTMP;
    public TextMeshProUGUI EnemiesLeftTMP;

    [SerializeField]
    private Vector2 YposRange;
    [SerializeField]
    private float XposSpawn;
    private float enemySpawnCounter;
    private int enemySpawnedAmmount;
    private int enemiesLeft;

    private void Update()
    {
        if (IsWaveStarted)
        {
            enemySpawnCounter -= Time.deltaTime;

            if (enemySpawnCounter <= 0f && enemySpawnedAmmount < EnemySpawnMaxAmmount)
            {
                enemySpawnedAmmount++;
                enemySpawnCounter = Random.Range(0.1f, 1.2f);
                SpawnEnemy();
            }
            else if (enemySpawnedAmmount >= EnemySpawnMaxAmmount)
            {
                CheckIsWaveCleared();
            }
        }

        CountRemainingEnemies();
    }

    public void StartWave()
    {
        enemySpawnCounter = 1f;
        enemySpawnedAmmount = 0;
        StartButton.interactable = false;
        EnemiesLeftTMP.gameObject.SetActive(true);

        WaveLevelTMP.text = "Wave " + WaveLevel;
        WaveLevel++;
        EnemySpawnMaxAmmount = 5 + WaveLevel + Random.Range(0, WaveLevel + 1);
        enemiesLeft = EnemySpawnMaxAmmount;

        IsWaveStarted = true;
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, EnemyPrefab.Length);
        Vector2 randomPos = new Vector2(XposSpawn, Random.Range(YposRange.x, YposRange.y));

        Instantiate(EnemyPrefab[randomEnemy], randomPos, Quaternion.identity);
    }

    private bool CheckIsWaveCleared()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0)
        {
            EnemiesLeftTMP.gameObject.SetActive(false);
            return StartButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("wave not cleared; array of enemies on scene is not empty");
            return StartButton.interactable = false;
        }
    }

    private void CountRemainingEnemies()
    {
        EnemiesLeftTMP.text = "Enemies left " + enemiesLeft;
    }

    public int SubtractEnemiesLeft()
    {
        return enemiesLeft--;
    }
}
