using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [Header("상단 Status")]
    [SerializeField]
    private TMP_Text date;
    [SerializeField]
    private TMP_Text gold;
    [SerializeField]
    private Button setting;

    [Header("행복도")]
    [SerializeField]
    private Image happyButton;

    [Header("치안도")]
    [SerializeField]
    private Image safetyButton;

    [Header("신앙심")]
    [SerializeField]
    private Image beliefButton;

    [Header("문화")]
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
