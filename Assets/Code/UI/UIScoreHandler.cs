using TMPro;
using UnityEngine;

public class UIScoreHandler : MonoBehaviour, IEventListener<ScoreChangedData>
{
    TextMeshProUGUI scoreText;


    void IEventListener<ScoreChangedData>.OnEvent(ScoreChangedData eventData)
    {
        scoreText.text = $"Score: {eventData.score}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        EventBus.Subscribe<ScoreChangedData>(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
