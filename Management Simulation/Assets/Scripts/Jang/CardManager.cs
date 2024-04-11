using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

using Random = UnityEngine.Random;

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
        //    Debug.Log(testlist.Count);
        //    StartCoroutine(SelectCard());
        //}
    }

    public void StartC()
    {
        StartCoroutine(SelectCard());
    }



    // Jang => ī�� ���� �ڷ�ƾ �Լ�
    IEnumerator SelectCard()
    {
        // Jang => random�� ����
        int random = Random.Range(0, cardDataList.Count);
        // Jang => �ڵ� ������ ���ؼ� varŸ������ ����
        var scriptable = cardDataList[random];

        if (cardDataList.Count > 0)
        {
            // Jang => SingleTon�� GamePlayManager�� instance�� ���� SetStart�Լ��� scriptable ����
            GameManager.instance.SetStart(scriptable);

            // Jang => CardUIManager�� ����
            _cardManager.GetCardInfo(scriptable);

            // Jang => ī�� ������ ���� �ڷ�ƾ ����
            yield return StartCoroutine(RemoveCard(random));
        }
        // Jang => Test�� ����ó�� 
        // todo => Jang => GameplayManager ȣ���� ī�� Ƚ����ŭ�� �ϸ� ������ ����ó�� �� �ʿ䰡 ���� ���ɼ� ���� 
        // => �ٰ��� ����غ��� �����ϱ�
        else
        {
            GameManager.instance.SetStart(scriptable);
        }

    }
    // Jang => ī�� ���� �ڷ�ƾ �Լ�
    IEnumerator RemoveCard(int value)
    {
        // Jang => testlist���� value index�� ����
        cardDataList.RemoveAt(value);
        yield return null;
    }
} 