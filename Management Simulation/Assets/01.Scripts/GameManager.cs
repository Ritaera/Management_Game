using DiceGame.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.AI;
=======
using UnityEngine;
>>>>>>> origin/L.Gyeol

[Serializable]
public class GameFloat
{
    public float Value = 0f;

    [NonSerialized]
    public float Min = 0f;

    [NonSerialized]
    public float Max = 100f;
}

<<<<<<< HEAD
[Serializable]
public class CardTurnData
{
    public string name;
    public int turn;
}

=======
>>>>>>> origin/L.Gyeol
public class GameManager : SingletonMonoBase<GameManager>
{
    #region 게임 내부 데이터
    public GameFloat HappyPoint = new GameFloat();
    public GameFloat SafetyPoint = new GameFloat();
    public GameFloat BeliefPoint = new GameFloat();
    public GameFloat CulturePoint = new GameFloat();

    [SerializeField]
<<<<<<< HEAD
    private int _date = 0;
    //private int _count = 0;
=======
    private int _gold = 0;
    private int _date = 0;
    private int _count = 0;
>>>>>>> origin/L.Gyeol

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
<<<<<<< HEAD
    //public List<int> cardTurns = new List<int>();
    public List<CardTurnData> cardTurns = new List<CardTurnData>();
    // 작성자 : 장승호(2024.04.15)
    // 이전 턴에 선택된 카드 인덱스 받아오기 위해 List사용 
    //public List<CardScriptableObject> previouseSelectedCardIndex = new List<CardScriptableObject>();
=======
    public List<int> cardTurns = new List<int>();
    // 작성자 : 장승호(2024.04.15)
    // 이전 턴에 선택된 카드 인덱스 받아오기 위해 List사용 
    public List<int> previouseSelectedCardIndex = new List<int>();
>>>>>>> origin/L.Gyeol

    #endregion

    #region 프로퍼티
<<<<<<< HEAD
    [SerializeField]
    public int Gold { get; private set; } = 200;

=======
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
>>>>>>> origin/L.Gyeol
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

<<<<<<< HEAD
    // 1. 카드를 뽑았을 때 카드 데이터 불러오기
    // 카드 데이터에 있는 수치를 조정
    // 주의: 카드에는 턴수가 있음. 턴수만큼 매턴 지속.
    override protected void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);

        // ScriptableObject에서 턴 값만 받아오기.
        //foreach (var card in CardDataManager.Instance.cardData)
        //{
        //    cardTurns.Add(card.cardAffectTurn);
        //}

        // 1.ScriptableObject에서 턴 값만 받아오기.
        for (int ix = 0; ix < CardDataManager.Instance.cardData.Length; ++ix)
        {
            cardTurns.Add(new CardTurnData()
            {
                name = CardDataManager.Instance.cardData[ix].name,
                turn = CardDataManager.Instance.cardData[ix].cardAffectTurn
            });
        }

        //foreach (var card in CardDataManager.Instance.cardData)
        //{
        //    cardTurns.Add(new CardTurnData() { name = card.name, turn = card.cardAffectTurn });
        //}
    }

    // 게임 결과를 JSON 파일로 저장하는 함수.
    // 테스트 중.
    public void SaveResultToJsonFile()
    {
        // 경로를 확인해 폴더가 없으면 생성해야 함.

        // Assets 경로 확인. (빌드했을때 필요할 수 있음).
        if (Directory.Exists("Assets") == false)
        {
            Directory.CreateDirectory("Assets");
        }

        // Assets 밑에 ResultData 폴더 확인.0
        if (Directory.Exists("Assets\\ResultData") == false)
        {
            Directory.CreateDirectory("Assets\\ResultData");
        }

        // 데이터 객체 생성하고, JSON으로 직렬화 후 텍스트 파일로 저장.
        //float happyPoint, float safetyPoint, float beliefPoint, float culturePoint, int gold, int date.
        GameResultData data = new GameResultData(HappyPoint.Value, SafetyPoint.Value, BeliefPoint.Value, CulturePoint.Value, Gold, Date);
        string jsonString = JsonUtility.ToJson(data);
        File.WriteAllText(@"Assets\ResultData\ResultData.txt", jsonString);
    }

    private void Update()
    {
        PointUpdate();
    }

    public void OnCardDataRemovedListener(int index)
    {
        cardTurns.RemoveAt(index);
=======
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
>>>>>>> origin/L.Gyeol
    }

    // Jang => CardScriptableObject에서 instance 호출을 통해 Coroutine을 하려했으나 불가능해서
    // 새로운 함수를 생성 후 내부에서 코루틴 호출
    public void SetStart(CardScriptableObject scriptableobj, int index)
    {

        // 선택된 카드의 인덱스 정보를 저장.
<<<<<<< HEAD
        //previouseSelectedCardIndex.Add(index);
        //previouseSelectedCardIndex.Add(scriptableobj);
=======
        previouseSelectedCardIndex.Add(index);
>>>>>>> origin/L.Gyeol

        // Jang => valueList에 받아온 scriptableobj 추가
        cardlist.Add(scriptableobj);

<<<<<<< HEAD
=======

>>>>>>> origin/L.Gyeol
        // 현재 값 + 다음 턴 증가량 값 계산 함수 호출
        GameSetValue();

        // Jang => 카드에 적용된 턴수가 모든 카드가 같지 않고 다르기 때문에 계산식을 반복문을 통해서 작성
        // Jang => 위에서 valueList에 Scriptableobj에서 받아온 scriptableobj에서 작성된 턴수가 존재하면 반복문을 돌리고
        // Jang => 턴수가 0이 되면 리스트에서 제거
<<<<<<< HEAD

        List<string> deleteNames = new List<string>();
        for (int ix = 0; ix < cardlist.Count; ++ix)
        {
            CardTurnData targetCardTurn = cardTurns.Find(card => cardlist[ix].name.Equals(card.name));
            if (targetCardTurn.turn == -1)
            {
                deleteNames.Add(targetCardTurn.name);
            }

            if (targetCardTurn.turn >= 0)
            {
                Utils.LogGreen($"반복문 시작 : {targetCardTurn}의 카드 턴수: {targetCardTurn.turn}, 카드턴수 이름: {targetCardTurn.name}");
                CardPlusSetValue(cardlist[ix], targetCardTurn);
            }
        }

        foreach (string name in deleteNames)
        {
            CardDeleteSetValue(cardlist.Find(card => name.Equals(card.name)));
            cardlist.Remove(cardlist.Find(card => name.Equals(card.name)));
            cardTurns.Remove(cardTurns.Find(card => name.Equals(card.name)));
        }

        //for (int ix = 0; ix < cardTurns.Count; ++ix)
        //{
        //    if (cardTurns[ix].turn == -1)
        //    {
        //        cardlist.Remove(cardlist.Find(card => card.name.Equals(cardTurns[ix].name)));
        //        cardTurns.RemoveAt(ix);
        //        continue;
        //    }
        //    if (cardTurns[ix].turn >= 0)
        //    {
        //        Utils.LogGreen($"{ix} : { cardTurns.Count } 반복문 시작 : {cardTurns[ix]}의 카드 턴수: {cardTurns[ix].turn}, 카드턴수 이름: {cardTurns[ix].name}");
        //        CardPlusSetValue(cardlist.Find(card => card.name.Equals(cardTurns[ix].name)), cardTurns[ix]);
        //    }
        //}

        //for (int i = 0; i < cardlist.Count; i++)
        //{
        //    // Jang => cardAffectTurn이 0, 0보다 작은경우는 리스트에서 제거
        //    //if (cardTurns[previouseSelectedCardIndex[i]] == -1)
        //    if (cardlist[i].turn == -1)
        //    {
        //        CardDeleteSetValue(cardlist[i]);
        //        cardlist.RemoveAt(i);
        //        cardTurns.RemoveAt(previouseSelectedCardIndex[i]);

        //        previouseSelectedCardIndex.RemoveAt(i);

        //        continue;
        //    }

        //    // Jang => 카드가 영향을 미치는 턴수는 valueList[i]._cardAffectTurn임으로 cardAffectTurn이 0보다 크면 코루틴 실행
        //    //if (cardTurns[previouseSelectedCardIndex[i]] >= 0)
        //    if (cardTurns[previouseSelectedCardIndex[i]].turn >= 0)
        //    {
        //        // 문제가 있을 때 검증 가능?
        //        Utils.LogGreen($"반복문 시작 : {cardlist[i]}의 카드 턴수: {cardTurns[previouseSelectedCardIndex[i]].turn}, 카드턴수 이름: {cardTurns[previouseSelectedCardIndex[i]].name}");
        //        CardPlusSetValue(cardlist[i], previouseSelectedCardIndex[i]);
        //    }
        //}

=======
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

>>>>>>> origin/L.Gyeol

        // 날짜 턴수 증가
        _date++;
    }

    // Jang => 게임내의 value값 설정 코루틴
<<<<<<< HEAD
    //public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, CardTurnData targetCardTurn)
=======
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
>>>>>>> origin/L.Gyeol
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
<<<<<<< HEAD
        //cardTurns[index]--;
        //cardTurns[index].turn--;
        targetCardTurn.turn--;
        Utils.LogGreen($"{scriptableObjects.name}의 카드 턴수 : {/*cardTurns[index]*/ targetCardTurn.turn}");

=======
        cardTurns[index]--;
>>>>>>> origin/L.Gyeol

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

<<<<<<< HEAD


=======
>>>>>>> origin/L.Gyeol
    }

    // 건물 업그레이드 + 카드 값 
    public void GameSetValue()
    {
        HappyPoint.Value += NextTurnHappy;
        SafetyPoint.Value += NextTurnSafety;
        BeliefPoint.Value += NextTurnBelief;
        CulturePoint.Value += NextTurnCulture;
<<<<<<< HEAD
        Gold += NextTurnGold;
    }
    public void AddUpgradeList(UpgradeScriptableObject upgradeScriptableObject)
    {
        //_count = upgradeList.Count;
=======
        _gold += NextTurnGold;
    }
    public void AddUpgradeList(UpgradeScriptableObject upgradeScriptableObject)
    {
        _count = upgradeList.Count;
>>>>>>> origin/L.Gyeol

        upgradeList.Add(upgradeScriptableObject);
        UpGradeValueSet(upgradeScriptableObject);
    }
    // 건물 업그레이드 값 계산
    public void UpGradeValueSet(UpgradeScriptableObject upgradeScriptableObject)
    {
<<<<<<< HEAD
        //if (_count >= upgradeList.Count)
        //{
        //    return;
        //}
=======
        if (_count >= upgradeList.Count)
        {
            return;
        }
>>>>>>> origin/L.Gyeol

        // 다음턴 값 계산
        NextTurnGold += upgradeScriptableObject.upGradeEveryTurnGold;
        NextTurnSafety += upgradeScriptableObject.upGradeSafetyAffectValue;
        NextTurnHappy += upgradeScriptableObject.upGradeHappyAffectValue;
        NextTurnBelief += upgradeScriptableObject.upGradeFaithAffectValue;
        NextTurnCulture += upgradeScriptableObject.upGradeCulturalAffectValue;

        // 건물 업그레이드 강화시 사용할 골드
<<<<<<< HEAD
        Gold -= upgradeScriptableObject.upGradeGold;
=======
        _gold -= upgradeScriptableObject.upGradeGold;
>>>>>>> origin/L.Gyeol
    }
}