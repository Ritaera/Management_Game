using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    private TMP_Text _date;
    private TMP_Text _gold;
    private Image _happyButton;
    private Image _safetyButton;
    private Image _beliefButton;
    private Image _cultureButton;
    public Action OpenCard; // 카드매니저 에서 사용할 액션.

    // 다음턴 추가될 골드 표시.
    private string _nextDayGold;


    private void Awake()
    {
        _gold = transform.Find("Status - Date/Text (TMP) - Gold").GetComponent<TMP_Text>(); 
        _date = transform.Find("Status - Date/Text (TMP) - Date").GetComponent<TMP_Text>();
        _happyButton = transform.Find("Status - Point/HappyPoint/Button - Happy - BG/Button - Happy - Slider").GetComponent<Image>();
        _safetyButton = transform.Find("Status - Point/SafetyPoint/Button - Safety - BG/Button - Safety - Slider").GetComponent<Image>();
        _beliefButton = transform.Find("Status - Point/BeliefPoint/Button - Belief - BG/Button - Belief - Slider").GetComponent<Image>();
        _cultureButton = transform.Find("Status - Point/CulturePoint/Button - Culture - BG/Button - Culture - Slider").GetComponent<Image>();
    }

    private void Start()
    {
        GameManager.instance.PointUpdate += StatusUpdate;
    }


    // KJH => 포인트 증가 혹은 감소할때 이 함수 호출.
    public void StatusUpdate()
    {
        _happyButton.fillAmount = GameManager.instance.HappyPoint.Value / GameManager.instance.HappyPoint.Max;
        _safetyButton.fillAmount = GameManager.instance.SafetyPoint.Value / GameManager.instance.SafetyPoint.Max;
        _beliefButton.fillAmount = GameManager.instance.BeliefPoint.Value / GameManager.instance.BeliefPoint.Max;
        _cultureButton.fillAmount = GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max;
        _date.text = "날짜 : " + GameManager.instance.Date.ToString();
        _nextDayGold = GameManager.instance.nextTurnGold.ToString();
        _gold.text = "골드 : " + GameManager.instance.Gold.ToString() + $" {_nextDayGold}";
    }
}
