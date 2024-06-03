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
    #region ���� ���� ������
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

    // ������ ������
    [SerializeField]
    public int NextTurnGold { get; private set; } = 10;
    public int NextTurnHappy { get; private set; }
    public int NextTurnSafety { get; private set; }
    public int NextTurnBelief { get; private set; }
    public int NextTurnCulture { get; private set; }
    #endregion

    #region ���� ���� ����� �ڷᱸ��
    // Jang => CardScriptableObject Script���� ������ ScriptableObject�� �޾ƿ� �����ϱ� ���� ����Ʈ ����
    public List<CardScriptableObject> cardlist = new List<CardScriptableObject>();
    //Jang => UpgradeScriptableObject Script���� ������ ScriptableObject�� �޾ƿ� �����ϱ� ���� ����Ʈ ����
    [SerializeField] List<UpgradeScriptableObject> upgradeList = new List<UpgradeScriptableObject>();
    // ī�� ScriptableObject�� ī�� �� ���� ���� �����ϴ� �迭.
<<<<<<< HEAD
    //public List<int> cardTurns = new List<int>();
    public List<CardTurnData> cardTurns = new List<CardTurnData>();
    // �ۼ��� : ���ȣ(2024.04.15)
    // ���� �Ͽ� ���õ� ī�� �ε��� �޾ƿ��� ���� List��� 
    //public List<CardScriptableObject> previouseSelectedCardIndex = new List<CardScriptableObject>();
=======
    public List<int> cardTurns = new List<int>();
    // �ۼ��� : ���ȣ(2024.04.15)
    // ���� �Ͽ� ���õ� ī�� �ε��� �޾ƿ��� ���� List��� 
    public List<int> previouseSelectedCardIndex = new List<int>();
>>>>>>> origin/L.Gyeol

    #endregion

    #region ������Ƽ
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
    // 1. ī�带 �̾��� �� ī�� ������ �ҷ�����
    // ī�� �����Ϳ� �ִ� ��ġ�� ����
    // ����: ī�忡�� �ϼ��� ����. �ϼ���ŭ ���� ����.
    override protected void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);

        // ScriptableObject���� �� ���� �޾ƿ���.
        //foreach (var card in CardDataManager.Instance.cardData)
        //{
        //    cardTurns.Add(card.cardAffectTurn);
        //}

        // 1.ScriptableObject���� �� ���� �޾ƿ���.
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

    // ���� ����� JSON ���Ϸ� �����ϴ� �Լ�.
    // �׽�Ʈ ��.
    public void SaveResultToJsonFile()
    {
        // ��θ� Ȯ���� ������ ������ �����ؾ� ��.

        // Assets ��� Ȯ��. (���������� �ʿ��� �� ����).
        if (Directory.Exists("Assets") == false)
        {
            Directory.CreateDirectory("Assets");
        }

        // Assets �ؿ� ResultData ���� Ȯ��.0
        if (Directory.Exists("Assets\\ResultData") == false)
        {
            Directory.CreateDirectory("Assets\\ResultData");
        }

        // ������ ��ü �����ϰ�, JSON���� ����ȭ �� �ؽ�Ʈ ���Ϸ� ����.
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

        // ScriptableObject���� �� ���� �޾ƿ���.
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

    // Jang => CardScriptableObject���� instance ȣ���� ���� Coroutine�� �Ϸ������� �Ұ����ؼ�
    // ���ο� �Լ��� ���� �� ���ο��� �ڷ�ƾ ȣ��
    public void SetStart(CardScriptableObject scriptableobj, int index)
    {

        // ���õ� ī���� �ε��� ������ ����.
<<<<<<< HEAD
        //previouseSelectedCardIndex.Add(index);
        //previouseSelectedCardIndex.Add(scriptableobj);
=======
        previouseSelectedCardIndex.Add(index);
>>>>>>> origin/L.Gyeol

        // Jang => valueList�� �޾ƿ� scriptableobj �߰�
        cardlist.Add(scriptableobj);

<<<<<<< HEAD
=======

>>>>>>> origin/L.Gyeol
        // ���� �� + ���� �� ������ �� ��� �Լ� ȣ��
        GameSetValue();

        // Jang => ī�忡 ����� �ϼ��� ��� ī�尡 ���� �ʰ� �ٸ��� ������ ������ �ݺ����� ���ؼ� �ۼ�
        // Jang => ������ valueList�� Scriptableobj���� �޾ƿ� scriptableobj���� �ۼ��� �ϼ��� �����ϸ� �ݺ����� ������
        // Jang => �ϼ��� 0�� �Ǹ� ����Ʈ���� ����
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
                Utils.LogGreen($"�ݺ��� ���� : {targetCardTurn}�� ī�� �ϼ�: {targetCardTurn.turn}, ī���ϼ� �̸�: {targetCardTurn.name}");
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
        //        Utils.LogGreen($"{ix} : { cardTurns.Count } �ݺ��� ���� : {cardTurns[ix]}�� ī�� �ϼ�: {cardTurns[ix].turn}, ī���ϼ� �̸�: {cardTurns[ix].name}");
        //        CardPlusSetValue(cardlist.Find(card => card.name.Equals(cardTurns[ix].name)), cardTurns[ix]);
        //    }
        //}

        //for (int i = 0; i < cardlist.Count; i++)
        //{
        //    // Jang => cardAffectTurn�� 0, 0���� �������� ����Ʈ���� ����
        //    //if (cardTurns[previouseSelectedCardIndex[i]] == -1)
        //    if (cardlist[i].turn == -1)
        //    {
        //        CardDeleteSetValue(cardlist[i]);
        //        cardlist.RemoveAt(i);
        //        cardTurns.RemoveAt(previouseSelectedCardIndex[i]);

        //        previouseSelectedCardIndex.RemoveAt(i);

        //        continue;
        //    }

        //    // Jang => ī�尡 ������ ��ġ�� �ϼ��� valueList[i]._cardAffectTurn������ cardAffectTurn�� 0���� ũ�� �ڷ�ƾ ����
        //    //if (cardTurns[previouseSelectedCardIndex[i]] >= 0)
        //    if (cardTurns[previouseSelectedCardIndex[i]].turn >= 0)
        //    {
        //        // ������ ���� �� ���� ����?
        //        Utils.LogGreen($"�ݺ��� ���� : {cardlist[i]}�� ī�� �ϼ�: {cardTurns[previouseSelectedCardIndex[i]].turn}, ī���ϼ� �̸�: {cardTurns[previouseSelectedCardIndex[i]].name}");
        //        CardPlusSetValue(cardlist[i], previouseSelectedCardIndex[i]);
        //    }
        //}

=======
        for (int i = 0; i < cardlist.Count; i++)
        {

            // Jang => ī�尡 ������ ��ġ�� �ϼ��� valueList[i]._cardAffectTurn������ cardAffectTurn�� 0���� ũ�� �ڷ�ƾ ����

            if (cardTurns[previouseSelectedCardIndex[i]] > 0)
            {
                CardPlusSetValue(cardlist[i], previouseSelectedCardIndex[i]);
            }
            // Jang => cardAffectTurn�� 0, 0���� �������� ����Ʈ���� ����
            else
            {
                CardDeleteSetValue(cardlist[i]);
                cardlist.RemoveAt(i);
                previouseSelectedCardIndex.RemoveAt(i);
            }
        }

>>>>>>> origin/L.Gyeol

        // ��¥ �ϼ� ����
        _date++;
    }

    // Jang => ���ӳ��� value�� ���� �ڷ�ƾ
<<<<<<< HEAD
    //public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, CardTurnData targetCardTurn)
=======
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
>>>>>>> origin/L.Gyeol
    {
        // �ۼ��� ���ȣ.(2024.04.15)
        // ScriptableObject�� ���� bool�� üũ�Ͽ� true�� �ǳʶٰ� false�� ���� ����

        if (scriptableObjects.isAlreadycomputation == false)
        {
            NextTurnGold += scriptableObjects.cardGoldAffectValue;
            NextTurnSafety += scriptableObjects.cardSafetyAffectValue;
            NextTurnHappy += scriptableObjects.cardHappyAffectValue;
            NextTurnBelief += scriptableObjects.cardFaithAffectValue;
            NextTurnCulture += scriptableObjects.cardCulturalAffectValue;
            scriptableObjects.isAlreadycomputation = true;
        }

        // ī�� �ϼ��� ���� ������� �Ź� ���ҵǾ����
<<<<<<< HEAD
        //cardTurns[index]--;
        //cardTurns[index].turn--;
        targetCardTurn.turn--;
        Utils.LogGreen($"{scriptableObjects.name}�� ī�� �ϼ� : {/*cardTurns[index]*/ targetCardTurn.turn}");

=======
        cardTurns[index]--;
>>>>>>> origin/L.Gyeol

    }
    // �ۼ��� ���ȣ(2024.04.15)
    // nextTurn �� ���� �Լ�
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

    // �ǹ� ���׷��̵� + ī�� �� 
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
    // �ǹ� ���׷��̵� �� ���
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

        // ������ �� ���
        NextTurnGold += upgradeScriptableObject.upGradeEveryTurnGold;
        NextTurnSafety += upgradeScriptableObject.upGradeSafetyAffectValue;
        NextTurnHappy += upgradeScriptableObject.upGradeHappyAffectValue;
        NextTurnBelief += upgradeScriptableObject.upGradeFaithAffectValue;
        NextTurnCulture += upgradeScriptableObject.upGradeCulturalAffectValue;

        // �ǹ� ���׷��̵� ��ȭ�� ����� ���
<<<<<<< HEAD
        Gold -= upgradeScriptableObject.upGradeGold;
=======
        _gold -= upgradeScriptableObject.upGradeGold;
>>>>>>> origin/L.Gyeol
    }
}