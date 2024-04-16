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
    private int _gold = 0;
    [SerializeField]
    private int _date = 0;

    private int _count = 0;

    private int _listCount = 0;

    // 다음턴 증가량
    [SerializeField]
    private int _nextTurnGold;
    private int _nextTurnHappy;
    private int _nextTurnSafety;
    private int _nextTurnBelief;
    private int _nextTurnCulture;

    // Jang => CardScriptableObject Script에서 선택한 ScriptableObject를 받아와 저장하기 위해 리스트 생성
    [SerializeField] List<CardScriptableObject> cardlist = new List<CardScriptableObject>();
    //Jang => UpgradeScriptableObject Script에서 선택한 ScriptableObject를 받아와 저장하기 위해 리스트 생성
    [SerializeField] List<UpgradeScriptableObject> upgradeList = new List<UpgradeScriptableObject>();
    // 카드 ScriptableObject의 카드 턴 값을 각가 관리하는 배열.
    [SerializeField] private List<int> cardTurns = new List<int>();
    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
        }
    }
    public int Date
    {
        get
        {
            return _date;
        }
        private set { }
    }
    public int nextTurnGold
    {
        get
        {
            return _nextTurnGold;
        }
        private set { }
    }

    public Action PointUpdate;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        // ScriptableObject에서 턴 값만 받아오기.
        foreach (var card in CardDataManager.Instance.cardData)
        {
            cardTurns.Add(card.cardAffectTurn);
        }
    }

    private void Update()
    {
        PointUpdate();
    }

    // Jang => CardScriptableObject에서 instance 호출을 통해 Coroutine을 하려했으나 불가능해서
    // 새로운 함수를 생성 후 내부에서 코루틴 호출
    public void SetStart(CardScriptableObject scriptableobj, int index)
    {
        _listCount = cardlist.Count;
        // Jang => valueList에 받아온 scriptableobj 추가
        cardlist.Add(scriptableobj);

        // Jang => 카드에 적용된 턴수가 모든 카드가 같지 않고 다르기 때문에 계산식을 반복문을 통해서 작성
        // Jang => 위에서 valueList에 Scriptableobj에서 받아온 scriptableobj에서 작성된 턴수가 존재하면 반복문을 돌리고
        // Jang => 턴수가 0이 되면 리스트에서 제거
        for (int i = 0; i < cardlist.Count; i++)
        {
            // Jang => 카드가 영향을 미치는 턴수는 valueList[i]._cardAffectTurn임으로 cardAffectTurn이 0보다 크면 코루틴 실행

            if (cardTurns[index] > 0)
            {
              CardPlusSetValue(cardlist[i], index);
            }
            // Jang => cardAffectTurn이 0, 0보다 작은경우는 리스트에서 제거
            else
            {
                CardDeleteSetValue(cardlist[i]);
                cardlist.RemoveAt(index);
            }
        }
        // 현재 값 + 다음 턴 증가량 값 계산 함수 호출
        GameSetValue();

        // 날짜 턴수 증가
        _date++;
    }

    // Jang => 게임내의 value값 설정 코루틴
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
    {
        if (cardlist.Count > _listCount)
        {
            Utils.Log($"{_nextTurnGold} + {scriptableObjects.cardGoldAffectValue}");

            _nextTurnGold += scriptableObjects.cardGoldAffectValue;
            _nextTurnSafety += scriptableObjects.cardSafetyAffectValue;
            _nextTurnHappy += scriptableObjects.cardHappyAffectValue;
            _nextTurnBelief += scriptableObjects.cardFaithAffectValue;
            _nextTurnCulture += scriptableObjects.cardCulturalAffectValue;
        }
        --cardTurns[index];
    }

    // 건물 업그레이드 값 계산
    public void UpGradeValueSet(UpgradeScriptableObject upgradeScriptableObject)
    {
        if (_count >= upgradeList.Count)
        {
            return;
        }

        // 다음턴 값 계산
        _nextTurnGold += upgradeScriptableObject.upGradeEveryTurnGold;
        _nextTurnSafety += upgradeScriptableObject.upGradeSafetyAffectValue;
        _nextTurnHappy += upgradeScriptableObject.upGradeHappyAffectValue;
        _nextTurnBelief += upgradeScriptableObject.upGradeFaithAffectValue;
        _nextTurnCulture += upgradeScriptableObject.upGradeCulturalAffectValue;

        // 건물 업그레이드 강화시 사용할 골드
        _gold -= upgradeScriptableObject.upGradeGold;

      
    }

    // 건물 업그레이드 + 카드 값 
    public void GameSetValue()
    {
        HappyPoint.Value += _nextTurnHappy;
        SafetyPoint.Value += _nextTurnSafety;
        BeliefPoint.Value += _nextTurnBelief;
        CulturePoint.Value += _nextTurnCulture;
        _gold += _nextTurnGold;
    }



    public void AddUpgradeList(UpgradeScriptableObject upgradeScriptableObject)
    {
        Utils.Log($"GameManager{_count}");
        _count = upgradeList.Count;
        upgradeList.Add(upgradeScriptableObject);
        Utils.Log($"GameManager{upgradeList.Count}");

        UpGradeValueSet(upgradeScriptableObject);
    }

    public void CardDeleteSetValue(CardScriptableObject scriptableObjects)
    {
        _nextTurnGold -= scriptableObjects.cardGoldAffectValue;
        _nextTurnSafety -= scriptableObjects.cardSafetyAffectValue;
        _nextTurnHappy -= scriptableObjects.cardHappyAffectValue;
        _nextTurnBelief -= scriptableObjects.cardFaithAffectValue;
        _nextTurnCulture -= scriptableObjects.cardCulturalAffectValue;
    }

}