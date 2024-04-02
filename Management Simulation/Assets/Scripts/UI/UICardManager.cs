using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UICardManager:MonoBehaviour
{
    private Image _backCard;
    private Button _backButton;

    private Image _frontCard;
    private Button _frontButton;

    private TMP_Text _nextDay;

    private void Awake()
    {
        _backCard = transform.Find("Panel/CardSystem/Button - BackCard").GetComponent<Image>();
        _backButton = transform.Find("Panel/CardSystem/Button - BackCard").GetComponent<Button>();
        _frontCard = transform.Find("Panel/CardSystem/Button - FrontCard").GetComponent<Image>();
        _frontButton = transform.Find("Panel/CardSystem/Button - FrontCard").GetComponent<Button>();
        _nextDay = transform.Find("Panel/Text (TMP) - NextDay").GetComponent<TMP_Text>();
    }




}
