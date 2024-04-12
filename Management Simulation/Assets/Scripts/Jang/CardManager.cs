using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    // Jang => 카드 스크립터블 리스트 생성
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
        // Jang => Test 단계 
        // todo => Jang => 게임 시작시 실행하도록 변경 필요
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Utils.Log(testlist.Count);
        //    StartCoroutine(SelectCard());
        //}
    }




    // Jang => 카드 고르는 코루틴 함수
    public void SelectCard()
    {
        // Jang => random값 설정
        int random = Random.Range(0, cardDataList.Count);
        // Jang => 코드 가독성 위해서 var타입으로 설정
        var scriptable = cardDataList[random];

        if (cardDataList.Count > 0)
        {
            // Jang => SingleTon인 GamePlayManager에 instance를 통해 SetStart함수에 scriptable 제공
            GameManager.instance.SetStart(scriptable, random);

            // Jang => CardUIManager에 전달
            _cardManager.GetCardInfo(scriptable);

            // Jang => 카드 선택후 삭제 코루틴 실행
            StartCoroutine(RemoveCard(random));
        }
        // Jang => Test용 예외처리 
        // todo => Jang => GameplayManager 호출은 카드 횟수만큼만 하면 됨으로 예외처리 할 필요가 없을 가능성 존재 
        // => 다같이 고민해보고 설정하기
        

    }
    // Jang => 카드 삭제 코루틴 함수
    IEnumerator RemoveCard(int value)
    {
        // Jang => testlist에서 value index값 삭제
        cardDataList.RemoveAt(value);
        yield return null;
    }
} 