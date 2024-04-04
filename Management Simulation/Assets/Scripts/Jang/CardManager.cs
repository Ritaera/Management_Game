using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CardManager : MonoBehaviour
{
    // Jang => ī�� ��ũ���ͺ� ����Ʈ ����
    public List<CardScriptableObject> testlist = new List<CardScriptableObject>();
    // carddata , listindex 
    // listindex -> random(0,30);
    // temp = testlist 
    // testlist = trashlisth
    // trashlist = temp

    private UITurnEnd _cardManager = new UITurnEnd();
    private UIStatus status = new UIStatus();

    private void Start()
    {
        status.OpenCard += () => SelectCard();
    }


    private void Update()
    {
        // Jang => Test �ܰ� 
        // todo => Jang => ���� ���۽� �����ϵ��� ���� �ʿ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(testlist.Count);
            StartCoroutine(SelectCard());
        }
    }

    // Jang => ī�� ���� �ڷ�ƾ �Լ�
    IEnumerator SelectCard()
    {
        // Jang => random�� ����
        int random = UnityEngine.Random.Range(0, testlist.Count);
        // Jang => �ڵ� ������ ���ؼ� varŸ������ ����
        var scriptable = testlist[random];

        if (testlist.Count > 0)
        {
            // Jang => SingleTon�� GamePlayManager�� instance�� ���� SetStart�Լ��� scriptable ����
            GameManager.instance.SetStart(scriptable);

            // todo => Jang => CardUIManager�� ����
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
        testlist.RemoveAt(value);
        yield return null;
    }
} 