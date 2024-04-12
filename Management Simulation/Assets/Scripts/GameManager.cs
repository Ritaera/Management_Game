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

    // ������ ������
    [SerializeField]
    private int _nextTurnGold;
    private int _nextTurnHappy;
    private int _nextTurnSafety;
    private int _nextTurnBelief;
    private int _nextTurnCulture;

    // Jang => CardScriptableObject Script���� ������ ScriptableObject�� �޾ƿ� �����ϱ� ���� ����Ʈ ����
    [SerializeField] List<CardScriptableObject> cardlist = new List<CardScriptableObject>();
    //Jang => UpgradeScriptableObject Script���� ������ ScriptableObject�� �޾ƿ� �����ϱ� ���� ����Ʈ ����
    [SerializeField] List<UpgradeScriptableObject> upgradeList = new List<UpgradeScriptableObject>();
    // ī�� ScriptableObject�� ī�� �� ���� ���� �����ϴ� �迭.
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

        // ScriptableObject���� �� ���� �޾ƿ���.
        foreach (var card in CardDataManager.Instance.cardData)
        {
            cardTurns.Add(card.cardAffectTurn);
        }
    }

    private void Update()
    {
        PointUpdate();
    }

    // Jang => CardScriptableObject���� instance ȣ���� ���� Coroutine�� �Ϸ������� �Ұ����ؼ�
    // ���ο� �Լ��� ���� �� ���ο��� �ڷ�ƾ ȣ��
    public void SetStart(CardScriptableObject scriptableobj, int index)
    {
        _listCount = cardlist.Count;
        // Jang => valueList�� �޾ƿ� scriptableobj �߰�
        cardlist.Add(scriptableobj);

        // Jang => ī�忡 ����� �ϼ��� ��� ī�尡 ���� �ʰ� �ٸ��� ������ ������ �ݺ����� ���ؼ� �ۼ�
        // Jang => ������ valueList�� Scriptableobj���� �޾ƿ� scriptableobj���� �ۼ��� �ϼ��� �����ϸ� �ݺ����� ������
        // Jang => �ϼ��� 0�� �Ǹ� ����Ʈ���� ����
        for (int i = 0; i < cardlist.Count; i++)
        {
            // Jang => ī�尡 ������ ��ġ�� �ϼ��� valueList[i]._cardAffectTurn������ cardAffectTurn�� 0���� ũ�� �ڷ�ƾ ����

            if (cardTurns[index] > 0)
            {
              CardPlusSetValue(cardlist[i], index);
            }
            // Jang => cardAffectTurn�� 0, 0���� �������� ����Ʈ���� ����
            else
            {
                CardDeleteSetValue(cardlist[i]);
                cardlist.RemoveAt(index);
            }
        }
        // ���� �� + ���� �� ������ �� ��� �Լ� ȣ��
        GameSetValue();

        // ��¥ �ϼ� ����
        _date++;
    }

    // Jang => ���ӳ��� value�� ���� �ڷ�ƾ
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

    // �ǹ� ���׷��̵� �� ���
    public void UpGradeValueSet(UpgradeScriptableObject upgradeScriptableObject)
    {
        if (_count >= upgradeList.Count)
        {
            return;
        }

        // ������ �� ���
        _nextTurnGold += upgradeScriptableObject.upGradeEveryTurnGold;
        _nextTurnSafety += upgradeScriptableObject.upGradeSafetyAffectValue;
        _nextTurnHappy += upgradeScriptableObject.upGradeHappyAffectValue;
        _nextTurnBelief += upgradeScriptableObject.upGradeFaithAffectValue;
        _nextTurnCulture += upgradeScriptableObject.upGradeCulturalAffectValue;

        // �ǹ� ���׷��̵� ��ȭ�� ����� ���
        _gold -= upgradeScriptableObject.upGradeGold;

      
    }

    // �ǹ� ���׷��̵� + ī�� �� 
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