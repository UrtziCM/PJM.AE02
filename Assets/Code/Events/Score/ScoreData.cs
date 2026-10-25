using UnityEngine;

public class ScoreData: IEventData
{
    public int score;
    public GameObject gameObject;

    public ScoreData(int score, GameObject gameObject)
    {
        this.score = score;
        this.gameObject = gameObject;
    }
}