using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private Canvas pauseCanvas;

    private void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

    public void Pause()
    {
        EventBus.Call(new PauseStartData());
        pauseCanvas.enabled = true;
    }

    public void Resume()
    {
        EventBus.Call(new PauseEndData());
        pauseCanvas.enabled = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Pause();
        }
    }
}
