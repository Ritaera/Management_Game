using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;
using UnityEngine.Events;
using System;

public class CardManager : MonoBehaviour
{
    // Jang => ī�� ��ũ���ͺ� ����Ʈ ����
    public List<CardScriptableObject> cardDataList = new List<CardScriptableObject>();
    // carddata , listindex 
    // listindex -> random(0,30);
    // temp = testlist 
    // testlist = trashlisth
    // trashlist = temp

    private UITurnEnd _cardManager;

    private void Start()
    {
        //foreach (var cardDefaultData in CardDataManager.Instance.cardData)
        //{
        //    var newCardSO = ScriptableObject.CreateInstance<CardScriptableObject>();
        //    //newCardSO = cardDefaultData;
        //    newCardSO.cardBackgroundImage = cardDefaultData.cardBackgroundImage; // ī�� ��׶��� 
        //    newCardSO.cardImage = cardDefaultData.cardImage; // ī�� ������ ���
        //    newCardSO.cardAffectTurn = cardDefaultData.cardAffectTurn; // ī�尡 ������ ��ġ�� ��.
        //    newCardSO.cardFaithAffectValue = cardDefaultData.cardFaithAffectValue; // ī�尡 �žӿ� ������ ��ġ�� ��`
        //    newCardSO.cardCulturalAffectValue = cardDefaultData.cardCulturalAffectValue; // ī�尡 ��ȭ�� ������ ��ġ�� ��`
        //    newCardSO.cardGoldAffectValue = cardDefaultData.cardGoldAffectValue; // ī�尡 ��忡 ������ ��ġ�� ��
        //    newCardSO.cardSafetyAffectValue = cardDefaultData.cardSafetyAffectValue; // ī�尡 ġ�ȿ� ������ ��ġ�� ��
        //    newCardSO.cardHappyAffectValue = cardDefaultData.cardHappyAffectValue; // ī�尡 �ູ�� ������ ��ġ�� ��
        //    newCardSO.cardDescription = cardDefaultData.cardDescription; // ī�� ����
        //    newCardSO.isAlreadycomputation = cardDefaultData.isAlreadycomputation;

        //    cardDataList.Add(newCardSO);
        //}

        foreach (var card in CardDataManager.Instance.cardData)
        {
            cardDataList.Add(card);
            
        }

        if (_cardManager == null)
        {
            _cardManager = FindFirstObjectByType<UITurnEnd>();
        }

        _cardManager.OpenCard += () => SelectCard();
    }


    private void Update()
    {
        // Jang => Test �ܰ� 
        // todo => Jang => ���� ���۽� �����ϵ��� ���� �ʿ�
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Utils.Log(testlist.Count);
        //    StartCoroutine(SelectCard());
        //}
    }

    // Jang => ī�� ���� �ڷ�ƾ �Լ�
    public void SelectCard()
    {
        // Jang => random�� ����
        int random = Random.Range(0, cardDataList.Count);

        // Jang => �ڵ� ������ ���ؼ� varŸ������ ����
        var scriptable = cardDataList[random];

        if (cardDataList.Count > 0)
        {
            // Jang => SingleTon�� GamePlayManager�� instance�� ���� SetStart�Լ��� scriptable ����
            GameManager.instance.SetStart(scriptable, random);

            // Jang => CardUIManager�� ����
            _cardManager.GetCardInfo(scriptable);

            // Jang => ī�� ������ ���� �ڷ�ƾ ����
            RemoveCard(random);
        }
        // Jang => Test�� ����ó�� 
        // todo => Jang => GameplayManager ȣ���� ī�� Ƚ����ŭ�� �ϸ� ������ ����ó�� �� �ʿ䰡 ���� ���ɼ� ���� 
        // => �ٰ��� ����غ��� �����ϱ�
        

    }
    // Jang => ī�� ���� �ڷ�ƾ �Լ�
    public void RemoveCard(int value)
    {
        // Jang => testlist���� value index�� ����
        cardDataList.RemoveAt(value);
    }
} 