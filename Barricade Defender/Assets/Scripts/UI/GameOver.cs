using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverWindow;
    public TextMeshProUGUI ScoreText;

    private PlayerController player;
    private BaseCampfire baseCampfire;
    private WaveManager waveManager;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        baseCampfire = FindObjectOfType<BaseCampfire>();
        waveManager = FindObjectOfType<WaveManager>();

        player.OnDeath.AddListener(ListenOnGameOver);
        baseCampfire.OnDeath.AddListener(ListenOnGameOver);
    }

    private void ListenOnGameOver()
    {
        GameOverWindow.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
        ScoreText.text = "Score: " + waveManager.WaveLevel.ToString();
    }
}
