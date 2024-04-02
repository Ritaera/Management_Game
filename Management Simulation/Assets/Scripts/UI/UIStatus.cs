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
    private Button _setting;
    private Button _nextTurn;
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
        _setting = transform.Find("Status - Date/Button - Setting").GetComponent<Button>();
        _nextTurn = transform.Find("Button - TurnEnd").GetComponent<Button>();
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
        _nextDayGold = GameManager.instance.SumGold.ToString();
        _gold.text = "골드 : " + GameManager.instance.Gold.ToString() + $" {_nextDayGold}";
    }

    // KJH => 다음날 버튼 눌렀을때 이 함수 호출.
    public void OnTurnEndButtonClick()
    {
        if ( GameManager.instance.Date > 30)
        {
            //Todo: 게임 종료씬 
        }
        
        // 
        // KJH => Todo: 다음날 넘어가는 스크립트 제작 후 여기 와서 이름 변경하기.
        // NextTurn 오브젝트 활성화.


        // Todo: 카드뽑기.

    }

    // KJH =>  설정 버튼 눌렀을때 이 함수 호출.
    public void OnSettingButtonClick()
    {
        gameObject.GetComponent<UISetting>().enabled = true;
    }

}
