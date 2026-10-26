using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLifecycleDispatcher : MonoBehaviour, IEventListener<LifeLostData>, IEventListener<PauseStartData>, IEventListener<PauseEndData>
{

    public void OnEvent(LifeLostData eventData)
    {
        if (eventData.currentLifes <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        EventBus.Call(new GameEndData());
        Debug.Log("End game");
        SceneManager.LoadScene("MainMenu");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        EventBus.Call(new BeforeGameStartData());
    }

    private IEnumerator Start()
    {
        EventBus.Subscribe<LifeLostData>(this);
        EventBus.Subscribe<PauseStartData>(this);
        EventBus.Subscribe<PauseEndData>(this);
        yield return null; // Esperamos un frame para que pasen todos los Starts de los objetos de la escena antes de empezar a jugar.
        Debug.Log("Start game");
        EventBus.Call(new GameStartData());
        Time.timeScale = 1.0f;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<LifeLostData>(this);
        EventBus.Unsubscribe<PauseStartData>(this);
        EventBus.Unsubscribe<PauseEndData>(this);
    }

    public void OnEvent(PauseStartData eventData)
    {
        Time.timeScale = 0;
    }

    public void OnEvent(PauseEndData eventData)
    {
        Time.timeScale = 1;
    }

    
}
