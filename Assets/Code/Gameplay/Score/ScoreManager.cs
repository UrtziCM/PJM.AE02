using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IEventListener<ScoreData> 
{
    private int score;
    private int highScore;

    private void Start()
    {
        EventBus.Subscribe<ScoreData>(this);
    }

    public void OnEvent(ScoreData eventData)
    {
        AddScore(eventData.score);
    }

    private void AddScore(int score)
    {
        int oldScore = this.score;
        this.score += score;
        EventBus.Call(new ScoreChangedData(oldScore, this.score));
    }
}
