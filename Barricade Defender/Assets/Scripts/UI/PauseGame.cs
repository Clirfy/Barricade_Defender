using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject PauseGameWindow;

    private PlayerController player;
    private GameManager gameManager;
    private bool isPaused = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();

        gameManager.OnGamePaused.AddListener(SwitchPause);
    }

    public void SwitchPause()
    {
        isPaused = !isPaused;

        switch (isPaused)
        {
            case true:
                PauseGameWindow.SetActive(true);
                Time.timeScale = 0f;
                player.enabled = false;
                break;

            case false:
                PauseGameWindow.SetActive(false);
                Time.timeScale = 1f;
                player.enabled = true;
                break;
        }
    }
}
