using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UITurnEnd:MonoBehaviour
{
    private Image _backCard;
    private Button _backButton;
    private Image _frontCard;
    private Button _nextTurnButton;  // 다음날 버튼.
    private TMP_Text _nextDay;  // 다음날 Text
    private TMP_Text _cardDescription;  // 카드 설명 Text
    private TMP_Text _cardTurn;  // 카드 턴수 Text
    private GameObject _cardSystem;
    private GameObject _panel; 
    private float _cardSpeed = 0.01f;

    private bool IsCardAnimationEnd = false;



    private void Awake()
    {
        _backCard = transform.Find("Panel/CardSystem/Button - FrontCard/Button - BackCard").GetComponent<Image>();
        _backButton = transform.Find("Panel/CardSystem/Button - FrontCard/Button - BackCard").GetComponent<Button>();
        _frontCard = transform.Find("Panel/CardSystem/Button - FrontCard").GetComponent<Image>();
        _nextDay = transform.Find("Panel/Text (TMP) - NextDay").GetComponent<TMP_Text>();
        _nextTurnButton = transform.Find("Button - TurnEnd").GetComponent<Button>();
        _cardDescription = transform.Find("Panel/CardSystem/Button - FrontCard/Description/Image/Text (TMP) - CardDescription").GetComponent<TMP_Text>();
        _cardTurn = transform.Find("Panel/CardSystem/Button - FrontCard/Turn/Image/Text (TMP) - CardTurn").GetComponent<TMP_Text>();
        _cardSystem = transform.Find("Panel/CardSystem").gameObject;
        _panel = transform.Find("Panel").gameObject;

        _nextTurnButton.onClick.AddListener(TurnEndButtonClick);
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
        if (IsCardAnimationEnd)
        {
            _panel.SetActive(false);  // 캠버스 비활성화.
            _nextTurnButton.gameObject.SetActive(true);  // 다음날 버튼 활성화.
            _backCard.fillAmount = 1;
        }
        else if (!IsCardAnimationEnd)
        {
            _backCard.fillAmount = 1;
            // 버튼 비활성화 
            _backButton.interactable = false;
            // fillAmount 감소 (Time.deltaTime 이용해서)
            StartCoroutine(C_CardOpen());
            _backButton.interactable = true;
        }
        IsCardAnimationEnd = !IsCardAnimationEnd;
    }


    // KJH => 다음날 버튼 눌렀을때 이 함수 호출.
    public void TurnEndButtonClick()
    {
        if (GameManager.instance.Date > 30)
        {
            //Todo: 게임 종료씬 
        }

        _panel.SetActive(true);  // 캠버스 활성화.
        _cardSystem.SetActive(false); // 카드시스템 비활성화.
        _nextDay.gameObject.SetActive(true);  // 다음날 Text 활성화.
        _nextTurnButton.gameObject.SetActive(false);  // 다음날 버튼 비활성화.
        StartCoroutine(C_TurnStart(3));  // 코루틴 시작.
    }


    IEnumerator C_TurnStart(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _nextDay.gameObject.SetActive(false);  // 다음날 Text 비활성화.
        _cardSystem.SetActive(true); // 카드시스템 활성화.
    }

    IEnumerator C_CardOpen()
    {
        while (_backCard.fillAmount > 0)
        {
            _backCard.fillAmount -= _cardSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
