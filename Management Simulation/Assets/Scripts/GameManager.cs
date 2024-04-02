using DiceGame.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    private int gold = 0;
    [SerializeField]
    private int date = 0;

    public int Gold 
    {
        get
        {
            return gold;
        }
        set
        {
            gold = Gold;
        }
    }
    public int Date 
    {
        get
        {
            return date;
        }
        set
        {
            date = Date;
        }
    }

    public int everyTurnGold;

    public Action PointUpdate;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Date = 300;
        PointUpdate?.Invoke();
    }

    // Jang => CardScriptableObject Script에서 선택한 ScriptableObject를 받아와 저장하기 위해 리스트 생성
    [SerializeField] List<CardScriptableObject> valueList = new List<CardScriptableObject>();

    // Jang => CardScriptableObject에서 instance 호출을 통해 Coroutine을 하려했으나 불가능해서
    // 새로운 함수를 생성 후 내부에서 코루틴 호출
    public void SetStart(CardScriptableObject scriptableobj)
    {
        // Jang => valueList에 받아온 scriptableobj 추가
        valueList.Add(scriptableobj);

        // Jang => 카드에 적용된 턴수가 모든 카드가 같지 않고 다르기 때문에 계산식을 반복문을 통해서 작성
        // Jang => 위에서 valueList에 Scriptableobj에서 받아온 scriptableobj에서 작성된 턴수가 존재하면 반복문을 돌리고
        // Jang => 턴수가 0이 되면 리스트에서 제거
        for (int i = 0; i < valueList.Count; i++)
        {
            // Jang => 카드가 영향을 미치는 턴수는 valueList[i]._cardAffectTurn임으로 cardAffectTurn이 0보다 크면 코루틴 실행
            if (valueList[i]._cardAffectTurn > 0)
            {
                StartCoroutine(CardValueSetGameValue(valueList[i]));
            }
            // Jang => cardAffectTurn이 0, 0보다 작은경우는 리스트에서 제거
            else
            {
                valueList.RemoveAt(i);
            }
        }
        SetGoldValue(gold, everyTurnGold);
        date--;
    }

    // Jang => 게임내의 value값 설정 코루틴
    IEnumerator CardValueSetGameValue(CardScriptableObject scriptableObjects)
    {
        // Jang => Property에 접근하여 set을 통해 값을 계산
        HappyPoint.Value += scriptableObjects._cardHappyAffectValue;
        SafetyPoint.Value += scriptableObjects._cardSafetyAffectValue;
        BeliefPoint.Value += scriptableObjects._cardFaithAffectValue;
        CulturePoint.Value += scriptableObjects._cardCulturalAffectValue;
        gold += scriptableObjects._cardGoldAffectValue;
        scriptableObjects._cardAffectTurn--;

        yield return null;

    }

    // 매턴 지급되는 골드, 건물 업그레이드를 통한 업그레이드
    public void SetGoldValue(int gold, int goldvalue)
    {
        gold += everyTurnGold;
        //todo => event invoke;
    }
}