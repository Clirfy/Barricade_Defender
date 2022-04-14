using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public UnityEvent OnGamePaused;
    private GameObject Player;
    private SceneLoader sceneLoader;

    protected override void Awake()
    {
        DontDestroy();
    }

    private void Update()
    {
        PauseGame();
    }

    private void DontDestroy()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            Player.GetComponent<PlayerController>().enabled = false;
            OnGamePaused.Invoke();
        }
    }
}
