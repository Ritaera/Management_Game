using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CardManager : MonoBehaviour
{
    // Jang => 카드 스크립터블 리스트 생성
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
        // Jang => Test 단계 
        // todo => Jang => 게임 시작시 실행하도록 변경 필요
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(testlist.Count);
            StartCoroutine(SelectCard());
        }
    }

    // Jang => 카드 고르는 코루틴 함수
    IEnumerator SelectCard()
    {
        // Jang => random값 설정
        int random = UnityEngine.Random.Range(0, testlist.Count);
        // Jang => 코드 가독성 위해서 var타입으로 설정
        var scriptable = testlist[random];

        if (testlist.Count > 0)
        {
            // Jang => SingleTon인 GamePlayManager에 instance를 통해 SetStart함수에 scriptable 제공
            GameManager.instance.SetStart(scriptable);

            // todo => Jang => CardUIManager에 전달
            _cardManager.GetCardInfo(scriptable);

            // Jang => 카드 선택후 삭제 코루틴 실행
            yield return StartCoroutine(RemoveCard(random));
        }
        // Jang => Test용 예외처리 
        // todo => Jang => GameplayManager 호출은 카드 횟수만큼만 하면 됨으로 예외처리 할 필요가 없을 가능성 존재 
        // => 다같이 고민해보고 설정하기
        else
        {
            GameManager.instance.SetStart(scriptable);
        }

    }
    // Jang => 카드 삭제 코루틴 함수
    IEnumerator RemoveCard(int value)
    {
        // Jang => testlist에서 value index값 삭제
        testlist.RemoveAt(value);
        yield return null;
    }
} 