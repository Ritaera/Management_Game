using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SceneManagers;


public class UITurnEnd : MonoBehaviour
{
    private Image _backCard;
    private Button _backButton;
    private Image _frontCard;
    private Button _nextTurnButton;  // 다음날 버튼.
    private TMP_Text _nextDay;  // 다음날 Text

    private TMP_Text _cardDescription;  // 카드 설명 Text
    private TMP_Text _cardTurn;  // 카드 턴수 Text
    private Image _cardImage;

    private GameObject _cardSystem;
    private GameObject _panel;
    private float _cardSpeed = 0.01f;

    private bool IsCardAnimationEnd = false;

    public Action OpenCard; // 카드매니저 에서 사용할 액션.

    CardManager _cardManager;
    UIStatus _uIStatus;

    private void Awake()
    {
        if (_cardManager == null)
        {
            _cardManager = FindFirstObjectByType<CardManager>();
        }
        if (_uIStatus == null)
        {
            _uIStatus = FindFirstObjectByType<UIStatus>();
        }

        _backCard = transform.Find("Panel/CardSystem/Button - FrontCard/Button - BackCard").GetComponent<Image>();
        _backButton = transform.Find("Panel/CardSystem/Button - FrontCard/Button - BackCard").GetComponent<Button>();
        _frontCard = transform.Find("Panel/CardSystem/Button - FrontCard").GetComponent<Image>();
        _nextDay = transform.Find("Panel/Text (TMP) - NextDay").GetComponent<TMP_Text>();
        _nextTurnButton = transform.Find("Button - TurnEnd").GetComponent<Button>();

        _cardDescription = transform.Find("Panel/CardSystem/Button - FrontCard/Description/Image/Text (TMP) - CardDescription").GetComponent<TMP_Text>();
        _cardTurn = transform.Find("Panel/CardSystem/Button - FrontCard/Turn/Image/Text (TMP) - CardTurn").GetComponent<TMP_Text>();
        _cardImage = transform.Find("").GetComponent<Image>();

        _cardSystem = transform.Find("Panel/CardSystem").gameObject;
        _panel = transform.Find("Panel").gameObject;
    }

    private void Start()
    {
        _backButton.onClick.AddListener(BackCardButtonClick);
        _nextTurnButton.onClick.AddListener(TurnEndButtonClick);
    }


    // KJH => 카드매니저 에서 선택된 카드정보를 가져와서 읽을 함수.
    public void GetCardInfo(CardScriptableObject scriptable)
    {
        _cardDescription.text = scriptable.cardDescription;
        _cardTurn.text = $"<color=black>턴수: {(scriptable.cardAffectTurn > 0 ? "+" : "")}{scriptable.cardAffectTurn}</color><color=red> 신앙: {(scriptable.cardFaithAffectValue > 0 ? "+" : "")}{scriptable.cardFaithAffectValue} 문화: {(scriptable.cardCulturalAffectValue > 0 ? "+" : "")}{scriptable.cardCulturalAffectValue}\n골드: {(scriptable.cardGoldAffectValue> 0 ? "+" : "")}{scriptable.cardGoldAffectValue} 치안: {(scriptable.cardSafetyAffectValue> 0 ? "+" : "")}{scriptable.cardSafetyAffectValue} 행복: {(scriptable.cardHappyAffectValue> 0 ? "+" : "")}{scriptable.cardHappyAffectValue}</color>";
         
        //_cardTurn.text = "턴수:" + scriptable.cardAffectTurn.ToString() + "신앙:" + scriptable.cardFaithAffectValue.ToString() +
        //                 "문화:" + scriptable.cardCulturalAffectValue.ToString() + "골드:" + scriptable.cardGoldAffectValue.ToString() +
        //                 "치안:" + scriptable.cardSafetyAffectValue.ToString() + "행복:" + scriptable.cardHappyAffectValue.ToString();
        _cardImage = scriptable.cardImage;
    }


    // KJH => 카드뒷면 버튼을 클릭했을시, 발생할 fillAmount 애니메이션.
    public void BackCardButtonClick()
    {
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
            // fillAmount 감소 (Time.deltaTime 이용해서)
            StartCoroutine(C_CardOpen());
        }
        IsCardAnimationEnd = !IsCardAnimationEnd;
    }


    // KJH => 다음날 버튼 눌렀을때 이 함수 호출.
    public void TurnEndButtonClick()
    {
        if (GameManager.instance.Date > 30 || GameManager.instance.HappyPoint.Value <= 0 || 
            GameManager.instance.SafetyPoint.Value <= 0 || GameManager.instance.Gold <= 0)
        {
            SceneManagers.LoadScenes(MoveScene.EndGame);
        }


        _panel.SetActive(true);  // 캠버스 활성화.
        _cardManager.SelectCard();
        _cardSystem.SetActive(false); // 카드시스템 비활성화.
        _nextDay.gameObject.SetActive(true);  // 다음날 Text 활성화.
        _nextTurnButton.gameObject.SetActive(false);  // 다음날 버튼 비활성화.
        StartCoroutine(C_TurnStart(3));  // 코루틴 시작.
    }

    /// <summary>
    /// 다음날 버튼 클릭시 잠시 기다리는 코루틴
    /// </summary>
    /// <param name="delayTime"> 지연시간 </param>
    /// <returns></returns>
    IEnumerator C_TurnStart(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _nextDay.gameObject.SetActive(false);  // 다음날 Text 비활성화.
        _cardSystem.SetActive(true); // 카드시스템 활성화.
    }

    /// <summary>
    /// 카드 애니메이션 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator C_CardOpen()
    {
        _backButton.interactable = false;
        while (_backCard.fillAmount > 0)
        {
            _backCard.fillAmount -= _cardSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _backButton.interactable = true;
    }
}
