using DiceGame.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
<<<<<<< Updated upstream

    [SerializeField]
    private int _sumGold;


=======
    private int _nextTrunGold;
>>>>>>> Stashed changes
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
            return _nextTrunGold;
        }
        private set { }
    }

    public Action PointUpdate;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        PointUpdate();
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
            if (valueList[i].cardAffectTurn > 0)
            {
                StartCoroutine(CardValueSetGameValue(valueList[i]));
            }
            // Jang => cardAffectTurn�� 0, 0���� �������� ����Ʈ���� ����
            else
            {
                valueList.RemoveAt(i);
            }
        }
<<<<<<< Updated upstream
        _date--;
        SetGoldValue(_gold, SumGold);
=======
        _date++;
        SetGoldValue(_gold, everyTurnGold);
>>>>>>> Stashed changes
    }

    // Jang => ���ӳ��� value�� ���� �ڷ�ƾ
    IEnumerator CardValueSetGameValue(CardScriptableObject scriptableObjects)
    {
        // Jang => Property�� �����Ͽ� set�� ���� ���� ���
        HappyPoint.Value += scriptableObjects.cardHappyAffectValue;
        SafetyPoint.Value += scriptableObjects.cardSafetyAffectValue;
        BeliefPoint.Value += scriptableObjects.cardFaithAffectValue;
        CulturePoint.Value += scriptableObjects.cardCulturalAffectValue;
        _gold += scriptableObjects.cardGoldAffectValue;
        scriptableObjects.cardAffectTurn--;

<<<<<<< Updated upstream
        _sumGold += scriptableObjects.cardGoldAffectValue;
=======
        _nextTrunGold += scriptableObjects._cardGoldAffectValue;
>>>>>>> Stashed changes
        yield return null;

    }

    // ���� ���޵Ǵ� ���, �ǹ� ���׷��̵带 ���� ���׷��̵�
    public void SetGoldValue(int gold, int goldvalue)
    {
        gold += SumGold;
        PointUpdate?.Invoke();
    }
}