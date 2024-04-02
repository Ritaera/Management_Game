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

    // Jang => CardScriptableObject Script���� ������ ScriptableObject�� �޾ƿ� �����ϱ� ���� ����Ʈ ����
    [SerializeField] List<CardScriptableObject> valueList = new List<CardScriptableObject>();

    // Jang => CardScriptableObject���� instance ȣ���� ���� Coroutine�� �Ϸ������� �Ұ����ؼ�
    // ���ο� �Լ��� ���� �� ���ο��� �ڷ�ƾ ȣ��
    public void SetStart(CardScriptableObject scriptableobj)
    {
        // Jang => valueList�� �޾ƿ� scriptableobj �߰�
        valueList.Add(scriptableobj);

        // Jang => ī�忡 ����� �ϼ��� ��� ī�尡 ���� �ʰ� �ٸ��� ������ ������ �ݺ����� ���ؼ� �ۼ�
        // Jang => ������ valueList�� Scriptableobj���� �޾ƿ� scriptableobj���� �ۼ��� �ϼ��� �����ϸ� �ݺ����� ������
        // Jang => �ϼ��� 0�� �Ǹ� ����Ʈ���� ����
        for (int i = 0; i < valueList.Count; i++)
        {
            // Jang => ī�尡 ������ ��ġ�� �ϼ��� valueList[i]._cardAffectTurn������ cardAffectTurn�� 0���� ũ�� �ڷ�ƾ ����
            if (valueList[i]._cardAffectTurn > 0)
            {
                StartCoroutine(CardValueSetGameValue(valueList[i]));
            }
            // Jang => cardAffectTurn�� 0, 0���� �������� ����Ʈ���� ����
            else
            {
                valueList.RemoveAt(i);
            }
        }
        SetGoldValue(gold, everyTurnGold);
        date--;
    }

    // Jang => ���ӳ��� value�� ���� �ڷ�ƾ
    IEnumerator CardValueSetGameValue(CardScriptableObject scriptableObjects)
    {
        // Jang => Property�� �����Ͽ� set�� ���� ���� ���
        HappyPoint.Value += scriptableObjects._cardHappyAffectValue;
        SafetyPoint.Value += scriptableObjects._cardSafetyAffectValue;
        BeliefPoint.Value += scriptableObjects._cardFaithAffectValue;
        CulturePoint.Value += scriptableObjects._cardCulturalAffectValue;
        gold += scriptableObjects._cardGoldAffectValue;
        scriptableObjects._cardAffectTurn--;

        yield return null;

    }

    // ���� ���޵Ǵ� ���, �ǹ� ���׷��̵带 ���� ���׷��̵�
    public void SetGoldValue(int gold, int goldvalue)
    {
        gold += everyTurnGold;
        //todo => event invoke;
    }
}