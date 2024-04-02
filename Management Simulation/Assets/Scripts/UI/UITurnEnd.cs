using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UITurnEnd:MonoBehaviour
{
    private Image _backCard;
    private Button _backButton;
    private Image _frontCard;

    private TMP_Text _nextDay;  // 다음날 Text
    private TMP_Text _cardDescription;  // 카드 설명 Text
    private TMP_Text _cardTurn;  // 카드 턴수 Text


    private void Awake()
    {
        _backCard = transform.Find("Panel/CardSystem/Button - BackCard").GetComponent<Image>();
        _backButton = transform.Find("Panel/CardSystem/Button - BackCard").GetComponent<Button>();
        _frontCard = transform.Find("Panel/CardSystem/Button - FrontCard").GetComponent<Image>();
        _nextDay = transform.Find("Panel/Text (TMP) - NextDay").GetComponent<TMP_Text>();
        _cardDescription = transform.Find("Panel/CardSystem/Button - FrontCard/Text (TMP) - CardDescription").GetComponent<TMP_Text>();
        _cardTurn = transform.Find("Panel/CardSystem/Button - FrontCard/Text (TMP) - CardTurn").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _backButton.onClick.AddListener(BackCardButtonClick);
    }


    // KJH => 카드매니저 에서 선택된 카드정보를 가져와서 읽을 함수.
    public void GetCardInfo(CardScriptableObject scriptable)
    {
        _cardDescription.text = scriptable._cardDescription.ToString();
        _cardTurn.text = scriptable._cardAffectTurn.ToString();
    }


    // KJH => 카드뒷면 버튼을 클릭했을시, 발생할 fillAmount 애니메이션.
    public void BackCardButtonClick()
    {
        // Todo: 버튼클릭시, Time.deltaTiem 을 이용해 아래에서 위로 카드뒷면이 사라지는 효과 연출 코드 작성.
        // 애니메이션 진행중에는 버튼 클릭이 비활성화 해야함. 2번째 버튼클릭을 한다면, 다음턴 실행.
        int count = 0;
        if (count >= 1)
        {
            count = 0;
            // 버튼 활성화
            // 패널들 비활성화
        }
        else if( count == 0)
        {
            count++;
            // 버튼 비활성화 
            // fillAmount 감소 (Time.deltaTime 이용해서)
        }
    }
}
