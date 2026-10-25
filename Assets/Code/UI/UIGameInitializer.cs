using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameInitializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
