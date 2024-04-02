using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [Header("��� Status")]
    [SerializeField]
    private TMP_Text _date;
    // KJH => �� ��(�� ��).
    [SerializeField]
    private TMP_Text _gold;
    // KJH => �÷��̾ ���� ����� ��.
    [SerializeField]
    private Button _setting;
    // KJH => ���� ��ư.
    [SerializeField]
    private Button _nextTurn;
    // KJH => ������ ��ư.

    [Header("�ູ��")]
    [SerializeField]
    private Image _happyButton;
    // KJH => �ູ�� ������.

    [Header("ġ�ȵ�")]
    [SerializeField]
    private Image _safetyButton;
    // KJH => ġ�ȵ� ������.

    [Header("�žӽ�")]
    [SerializeField]
    private Image _beliefButton;
    // KJH => �žӽ� ������.

    [Header("��ȭ")]
    [SerializeField]
    private Image _cultureButton;
    // KJH => ��ȭ ������.

    private void Start()
    {
        GameManager.instance.PointUpdate += StatusUpdate;
    }


    // KJH => ����Ʈ ���� Ȥ�� �����Ҷ� �� �Լ� ȣ��.
    public void StatusUpdate()
    {
        _happyButton.fillAmount = GameManager.instance.HappyPoint.Value / GameManager.instance.HappyPoint.Max;
        _safetyButton.fillAmount = GameManager.instance.SafetyPoint.Value / GameManager.instance.SafetyPoint.Max;
        _beliefButton.fillAmount = GameManager.instance.BeliefPoint.Value / GameManager.instance.BeliefPoint.Max;
        _cultureButton.fillAmount = GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max;
        _date.text = "��¥ : " + GameManager.instance.Date.ToString();
        _gold.text = "��� : " + GameManager.instance.Gold.ToString();
    }

    // KJH => ������ ��ư �������� �� �Լ� ȣ��.
    public void OnTurnEndButtonClick()
    {
        if ( GameManager.instance.Date > 30)
        {
            //Todo: ���� ����� 
        }
        gameObject.GetComponent<UIStatus>().enabled = false;
        // gameObject.GetComponent< ��ũ��Ʈ �̸� >().enabled = false;   => ����â ��ũ��Ʈ ��Ȱ��ȭ.
        // gameObject.GetComponent< ��ũ��Ʈ �̸� >().enabled = false;  => nextTurn ��ũ��Ʈ Ȱ��ȭ.
    }

    // KJH =>  ���� ��ư �������� �� �Լ� ȣ��.
    public void OnSettingButtonClick()
    {
        //gameObject.GetComponent<UISetting>().enabled = true;
    }
}
