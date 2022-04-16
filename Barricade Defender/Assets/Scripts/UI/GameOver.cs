using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverWindow;

    private PlayerController player;
    private BaseCampfire baseCampfire;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        baseCampfire = FindObjectOfType<BaseCampfire>();

        player.OnDeath.AddListener(ListenOnGameOver);
        baseCampfire.OnDeath.AddListener(ListenOnGameOver);
    }

    private void ListenOnGameOver()
    {
        GameOverWindow.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
    }
}
