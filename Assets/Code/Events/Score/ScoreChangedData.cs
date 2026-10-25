internal class ScoreChangedData : IEventData
{
    public int oldScore;
    public int score;

    public ScoreChangedData(int oldScore, int score)
    {
        this.oldScore = oldScore;
        this.score = score;
    }
}