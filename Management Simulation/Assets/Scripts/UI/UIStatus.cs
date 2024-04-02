using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [Header("상단 Status")]
    [SerializeField]
    private TMP_Text _date;
    // KJH => 턴 수(일 수).
    [SerializeField]
    private TMP_Text _gold;
    // KJH => 플레이어가 가진 골드의 수.
    [SerializeField]
    private Button _setting;
    // KJH => 설정 버튼.
    [SerializeField]
    private Button _nextTurn;
    // KJH => 다음날 버튼.

    [Header("행복도")]
    [SerializeField]
    private Image _happyButton;
    // KJH => 행복도 게이지.

    [Header("치안도")]
    [SerializeField]
    private Image _safetyButton;
    // KJH => 치안도 게이지.

    [Header("신앙심")]
    [SerializeField]
    private Image _beliefButton;
    // KJH => 신앙심 게이지.

    [Header("문화")]
    [SerializeField]
    private Image _cultureButton;
    // KJH => 문화 게이지.

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
        _gold.text = "골드 : " + GameManager.instance.Gold.ToString();
    }

    // KJH => 다음날 버튼 눌렀을때 이 함수 호출.
    public void OnTurnEndButtonClick()
    {
        if ( GameManager.instance.Date > 30)
        {
            //Todo: 게임 종료씬 
        }
        gameObject.GetComponent<UIStatus>().enabled = false;
        // gameObject.GetComponent< 스크립트 이름 >().enabled = false;   => 설정창 스크립트 비활성화.
        // gameObject.GetComponent< 스크립트 이름 >().enabled = false;  => nextTurn 스크립트 활성화.
    }

    // KJH =>  설정 버튼 눌렀을때 이 함수 호출.
    public void OnSettingButtonClick()
    {
        //gameObject.GetComponent<UISetting>().enabled = true;
    }
}
