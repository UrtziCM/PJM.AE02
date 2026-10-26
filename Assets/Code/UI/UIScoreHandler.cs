using TMPro;
using UnityEngine;

public class UIScoreHandler : MonoBehaviour, IEventListener<ScoreChangedData>
{
    TextMeshProUGUI scoreText;


    public void OnEvent(ScoreChangedData eventData)
    {
        scoreText.text = $"Score: {eventData.score}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        EventBus.Subscribe<ScoreChangedData>(this);

    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ScoreChangedData>(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
