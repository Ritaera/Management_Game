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
    #region 게임 내부 데이터
    public GameFloat HappyPoint = new GameFloat();
    public GameFloat SafetyPoint = new GameFloat();
    public GameFloat BeliefPoint = new GameFloat();
    public GameFloat CulturePoint = new GameFloat();

    [SerializeField]
    private int _gold = 0;
    private int _date = 0;
    private int _count = 0;

    // 다음턴 증가량
    [SerializeField]
    public int NextTurnGold { get; private set; } = 10;
    public int NextTurnHappy { get; private set; }
    public int NextTurnSafety { get; private set; }
    public int NextTurnBelief { get; private set; }
    public int NextTurnCulture { get; private set; }
    #endregion

    #region 게임 내부 저장용 자료구조
    // Jang => CardScriptableObject Script에서 선택한 ScriptableObject를 받아와 저장하기 위해 리스트 생성
    public List<CardScriptableObject> cardlist = new List<CardScriptableObject>();
    //Jang => UpgradeScriptableObject Script에서 선택한 ScriptableObject를 받아와 저장하기 위해 리스트 생성
    [SerializeField] List<UpgradeScriptableObject> upgradeList = new List<UpgradeScriptableObject>();
    // 카드 ScriptableObject의 카드 턴 값을 각가 관리하는 배열.
    public List<int> cardTurns = new List<int>();
    // 작성자 : 장승호(2024.04.15)
    // 이전 턴에 선택된 카드 인덱스 받아오기 위해 List사용 
    public List<int> previouseSelectedCardIndex = new List<int>();

    #endregion

    #region 프로퍼티
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

    #endregion

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
        //PointUpdate();
    }

    // Jang => CardScriptableObject에서 instance 호출을 통해 Coroutine을 하려했으나 불가능해서
    // 새로운 함수를 생성 후 내부에서 코루틴 호출
    public void SetStart(CardScriptableObject scriptableobj, int index)
    {

        // 선택된 카드의 인덱스 정보를 저장.
        previouseSelectedCardIndex.Add(index);

        // Jang => valueList에 받아온 scriptableobj 추가
        cardlist.Add(scriptableobj);


        // 현재 값 + 다음 턴 증가량 값 계산 함수 호출
        GameSetValue();

        // Jang => 카드에 적용된 턴수가 모든 카드가 같지 않고 다르기 때문에 계산식을 반복문을 통해서 작성
        // Jang => 위에서 valueList에 Scriptableobj에서 받아온 scriptableobj에서 작성된 턴수가 존재하면 반복문을 돌리고
        // Jang => 턴수가 0이 되면 리스트에서 제거
        for (int i = 0; i < cardlist.Count; i++)
        {

            // Jang => 카드가 영향을 미치는 턴수는 valueList[i]._cardAffectTurn임으로 cardAffectTurn이 0보다 크면 코루틴 실행

            if (cardTurns[previouseSelectedCardIndex[i]] > 0)
            {
                CardPlusSetValue(cardlist[i], previouseSelectedCardIndex[i]);
            }
            // Jang => cardAffectTurn이 0, 0보다 작은경우는 리스트에서 제거
            else
            {
                CardDeleteSetValue(cardlist[i]);
                cardlist.RemoveAt(i);
                previouseSelectedCardIndex.RemoveAt(i);
            }
        }


        // 날짜 턴수 증가
        _date++;
    }

    // Jang => 게임내의 value값 설정 코루틴
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
    {
        // 작성자 장승호.(2024.04.15)
        // ScriptableObject의 내부 bool값 체크하여 true시 건너뛰고 false시 계산식 실행

        if (scriptableObjects.isAlreadycomputation == false)
        {
            NextTurnGold += scriptableObjects.cardGoldAffectValue;
            NextTurnSafety += scriptableObjects.cardSafetyAffectValue;
            NextTurnHappy += scriptableObjects.cardHappyAffectValue;
            NextTurnBelief += scriptableObjects.cardFaithAffectValue;
            NextTurnCulture += scriptableObjects.cardCulturalAffectValue;
            scriptableObjects.isAlreadycomputation = true;
        }

        // 카드 턴수는 계산과 상관없이 매번 감소되어야함
        cardTurns[index]--;

    }
    // 작성자 장승호(2024.04.15)
    // nextTurn 값 삭제 함수
    public void CardDeleteSetValue(CardScriptableObject scriptableObjects)
    {
        NextTurnGold -= scriptableObjects.cardGoldAffectValue;
        NextTurnSafety -= scriptableObjects.cardSafetyAffectValue;
        NextTurnHappy -= scriptableObjects.cardHappyAffectValue;
        NextTurnBelief -= scriptableObjects.cardFaithAffectValue;
        NextTurnCulture -= scriptableObjects.cardCulturalAffectValue;

    }

    // 건물 업그레이드 + 카드 값 
    public void GameSetValue()
    {
        HappyPoint.Value += NextTurnHappy;
        SafetyPoint.Value += NextTurnSafety;
        BeliefPoint.Value += NextTurnBelief;
        CulturePoint.Value += NextTurnCulture;
        _gold += NextTurnGold;
    }
    public void AddUpgradeList(UpgradeScriptableObject upgradeScriptableObject)
    {
        _count = upgradeList.Count;

        upgradeList.Add(upgradeScriptableObject);
        UpGradeValueSet(upgradeScriptableObject);
    }
    // 건물 업그레이드 값 계산
    public void UpGradeValueSet(UpgradeScriptableObject upgradeScriptableObject)
    {
        if (_count >= upgradeList.Count)
        {
            return;
        }

        // 다음턴 값 계산
        NextTurnGold += upgradeScriptableObject.upGradeEveryTurnGold;
        NextTurnSafety += upgradeScriptableObject.upGradeSafetyAffectValue;
        NextTurnHappy += upgradeScriptableObject.upGradeHappyAffectValue;
        NextTurnBelief += upgradeScriptableObject.upGradeFaithAffectValue;
        NextTurnCulture += upgradeScriptableObject.upGradeCulturalAffectValue;

        // 건물 업그레이드 강화시 사용할 골드
        _gold -= upgradeScriptableObject.upGradeGold;
    }
}