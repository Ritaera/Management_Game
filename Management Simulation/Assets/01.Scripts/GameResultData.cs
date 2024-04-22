[System.Serializable]
public class GameResultData
{
    public float HappyPoint;
    public float SafetyPoint;
    public float BeliefPoint;
    public float CulturePoint;
    public int Gold;
    public int Date;

    public GameResultData()
    {
    }

    public GameResultData(float happyPoint, float safetyPoint, float beliefPoint, float culturePoint, int gold, int date)
    {
        HappyPoint = happyPoint;
        SafetyPoint = safetyPoint;
        BeliefPoint = beliefPoint;
        CulturePoint = culturePoint;
        Gold = gold;
        Date = date;
    }
}