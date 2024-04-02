using DiceGame.Singleton;
using System;

[Serializable]
public class GameFloat
{
    public float Value = 0f;

    [NonSerialized]
    public float Min = 0f;

    [NonSerialized]
    public float Max = 100f;
}

public class GameManager : SingletonMonoBase<GameManager>
{
    public GameFloat HappyPoint = new GameFloat();
    public GameFloat SafetyPoint = new GameFloat();
    public GameFloat BeliefPoint = new GameFloat();
    public GameFloat CulturePoint = new GameFloat();
    public GameFloat Gold = new GameFloat();
    public GameFloat Date = new GameFloat();

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}