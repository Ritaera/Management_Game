using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [Header("��� Status")]
    [SerializeField]
    private TMP_Text date;
    [SerializeField]
    private TMP_Text gold;
    [SerializeField]
    private Button setting;

    [Header("�ູ��")]
    [SerializeField]
    private Image happyButton;

    [Header("ġ�ȵ�")]
    [SerializeField]
    private Image safetyButton;

    [Header("�žӽ�")]
    [SerializeField]
    private Image beliefButton;

    [Header("��ȭ")]
    [SerializeField]
    private Image cultureButton;


    private void Update()
    {
        StatusUpdate();
        //Debug.Log(GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max);
    }
    public void StatusUpdate()
    {
        happyButton.fillAmount = GameManager.instance.HappyPoint.Value / GameManager.instance.HappyPoint.Max;
        safetyButton.fillAmount = GameManager.instance.SafetyPoint.Value / GameManager.instance.SafetyPoint.Max;
        beliefButton.fillAmount = GameManager.instance.BeliefPoint.Value / GameManager.instance.BeliefPoint.Max;
        cultureButton.fillAmount = GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max;
    }

    public void OnSettingButtonClick()
    {
        throw new System.Exception();
    }
}
