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
    #region ���� ���� ������
    public GameFloat HappyPoint = new GameFloat();
    public GameFloat SafetyPoint = new GameFloat();
    public GameFloat BeliefPoint = new GameFloat();
    public GameFloat CulturePoint = new GameFloat();

    [SerializeField]
    private int _gold = 0;
    private int _date = 0;
    private int _count = 0;

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
    public List<int> cardTurns = new List<int>();
    // �ۼ��� : ���ȣ(2024.04.15)
    // ���� �Ͽ� ���õ� ī�� �ε��� �޾ƿ��� ���� List��� 
    public List<int> previouseSelectedCardIndex = new List<int>();

    #endregion

    #region ������Ƽ
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

        // ScriptableObject���� �� ���� �޾ƿ���.
        foreach (var card in CardDataManager.Instance.cardData)
        {
            cardTurns.Add(card.cardAffectTurn);
        }
    }
    private void Update()
    {
        //PointUpdate();
    }

    // Jang => CardScriptableObject���� instance ȣ���� ���� Coroutine�� �Ϸ������� �Ұ����ؼ�
    // ���ο� �Լ��� ���� �� ���ο��� �ڷ�ƾ ȣ��
    public void SetStart(CardScriptableObject scriptableobj, int index)
    {

        // ���õ� ī���� �ε��� ������ ����.
        previouseSelectedCardIndex.Add(index);

        // Jang => valueList�� �޾ƿ� scriptableobj �߰�
        cardlist.Add(scriptableobj);


        // ���� �� + ���� �� ������ �� ��� �Լ� ȣ��
        GameSetValue();

        // Jang => ī�忡 ����� �ϼ��� ��� ī�尡 ���� �ʰ� �ٸ��� ������ ������ �ݺ����� ���ؼ� �ۼ�
        // Jang => ������ valueList�� Scriptableobj���� �޾ƿ� scriptableobj���� �ۼ��� �ϼ��� �����ϸ� �ݺ����� ������
        // Jang => �ϼ��� 0�� �Ǹ� ����Ʈ���� ����
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


        // ��¥ �ϼ� ����
        _date++;
    }

    // Jang => ���ӳ��� value�� ���� �ڷ�ƾ
    public void CardPlusSetValue(CardScriptableObject scriptableObjects, int index)
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
        cardTurns[index]--;

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

    }

    // �ǹ� ���׷��̵� + ī�� �� 
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
    // �ǹ� ���׷��̵� �� ���
    public void UpGradeValueSet(UpgradeScriptableObject upgradeScriptableObject)
    {
        if (_count >= upgradeList.Count)
        {
            return;
        }

        // ������ �� ���
        NextTurnGold += upgradeScriptableObject.upGradeEveryTurnGold;
        NextTurnSafety += upgradeScriptableObject.upGradeSafetyAffectValue;
        NextTurnHappy += upgradeScriptableObject.upGradeHappyAffectValue;
        NextTurnBelief += upgradeScriptableObject.upGradeFaithAffectValue;
        NextTurnCulture += upgradeScriptableObject.upGradeCulturalAffectValue;

        // �ǹ� ���׷��̵� ��ȭ�� ����� ���
        _gold -= upgradeScriptableObject.upGradeGold;
    }
}