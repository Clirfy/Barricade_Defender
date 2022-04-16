using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnGamePaused;

    private void Awake()
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
            OnGamePaused.Invoke();
        }
    }

    public void ClickResumeGame()
    {
        OnGamePaused.Invoke();
    }
}
