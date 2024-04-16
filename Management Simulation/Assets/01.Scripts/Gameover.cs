
using System.Collections.Generic;
using UnityEngine;

public interface Gameover : SceneManagers
{

    public static void GameOverCheck()
    {
        // Belief, Culture < 0 이 되어도 게임 종료 조건이 아님 => 나머지는 게임 종료조건임으로 OR 연산
        if (GameManager.instance.HappyPoint.Value < 0 || GameManager.instance.SafetyPoint.Value < 0 ||
            GameManager.instance.Gold < 0 || GameManager.instance.Date < 30)
        {
            // 패배 조건
            if (GameManager.instance.HappyPoint.Value < 0)
            {
                Resources.Load<EndingScriptableObject>("EndingScriptableObject/BadHappyPointEnding");
            }
            else if (GameManager.instance.SafetyPoint.Value < 0)
            {
                Resources.Load<EndingScriptableObject>("EndingScriptableObject/BadSafetyPointEnding");
            }
            else if (GameManager.instance.Gold < 0)
            {
                Resources.Load<EndingScriptableObject>("EndingScriptableObject/BadGoldEnding");
            }

            // 승리 조건
            if (GameManager.instance.BeliefPoint.Value > 80 && GameManager.instance.CulturePoint.Value > 80)
            {
                Resources.Load<EndingScriptableObject>("EndingScriptableObject/HappyBothEnding");

            }
            else if (GameManager.instance.BeliefPoint.Value > 80)
            {
                Resources.Load<EndingScriptableObject>("EndingScriptableObject/HappyBeliefEnding");
            }
            else if (GameManager.instance.CulturePoint.Value > 80)
            {
                Resources.Load<EndingScriptableObject>("EndingScriptableObject/HappyCultureEnding");
            }
            SceneManagers.LoadScenes(MoveScene.EndGame);
        }
    }
}
