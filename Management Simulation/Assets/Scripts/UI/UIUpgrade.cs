using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    private GameObject _popUpBase; // ��Ȱ��ȭ ��ų ��ġ
    private TMP_Text _upgradeDescription; // ��ȭ ���� ��� TMP �ؽ�Ʈ
    private TMP_Text _upgradePoint; // ��ȭ ��ġ ��� TMP �ؽ�Ʈ
    private TMP_Text _upgradeCost; // ��ȭ ��� ��� TMP �ؽ�Ʈ
    private Button _accapt; // ��ȭ���� ��ư
    private Button _cancel; // ��ȭ��� ��ư

    private int _samUpgradeCount = 1;
    private int _mariaUpgradeCount = 1;
    private int _heyjiUpgradeCount = 1;
    private int _haniUpgradeCount = 1;
    private int _banksyUpgradeCount = 1;

    public GameObject PopUpBase
    {
        get
        {
            return _popUpBase;
        }
        set
        {
            _popUpBase = value;
        }
    }

    UpgradeScriptableObject upgradeScriptableObject;
    PlayerController2 _playerController;

    private void Awake()
    {
        if (upgradeScriptableObject == null)
        {
            upgradeScriptableObject = FindFirstObjectByType<UpgradeScriptableObject>();
        }
        if (_playerController == null)
        {
            _playerController = FindFirstObjectByType<PlayerController2>();
        }

        _popUpBase = transform.Find("Panel - PopUpBase").gameObject;
        _upgradeDescription = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) - Description").GetComponent<TMP_Text>();
        _upgradePoint = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) -  Plus").GetComponent<TMP_Text>();
        _upgradeCost = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) - Cost").GetComponent<TMP_Text>();
        _accapt = transform.Find("Panel - PopUpBase/Image - Chat/Button - Accapt").GetComponent<Button>(); 
        _cancel = transform.Find("Panel - PopUpBase/Image - Chat/Button - Cancel").GetComponent<Button>();
    }


    public void Upgrade()
    {
        _popUpBase.SetActive(true);
        if (_playerController.HitName == "Sam smith") //Todo: ���尣 �۾� ó��.
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"blacksmithLevel{_samUpgradeCount}");
        }
        else if (_playerController.HitName == "Maria") //Todo: ����ó��
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"Cathedral{_mariaUpgradeCount}");
        }
        else if (_playerController.HitName == "Hani") //Todo: �𷳰����
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"GuildLevel{_haniUpgradeCount}");
        }
        else if (_playerController.HitName == "Hyeji") //Todo: ��
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"CastleLevel{_heyjiUpgradeCount}");
        }
        else                                           //Todo: ����
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"Bank{_banksyUpgradeCount}");
        }
        if (upgradeScriptableObject != null)
        {
            UpdateText();
        }
    }


    private void Start()
    {
        _accapt.onClick.AddListener(AccaptButtonClick);
        _cancel.onClick.AddListener(CancelButtonClick);
    }

    public void AccaptButtonClick()
    {
        if (GameManager.instance.Gold >= upgradeScriptableObject.upGradeGold)
        {
            switch (_playerController.HitName)  // ��ư���� ��� ī��Ʈ 1 ����.
            {
                case "Sam smith":
                    _samUpgradeCount++;
                    break;
                case "Maria":
                    _mariaUpgradeCount++;
                    break;
                case "Hani":
                    _haniUpgradeCount++;
                    break;
                case "Hyeji":
                    _heyjiUpgradeCount++;
                    break;
                case "Banksy":
                    _banksyUpgradeCount++;
                    break;
                default:
                    break;
            }
            GameManager.instance.UpGradeValueSet(upgradeScriptableObject);
        }
        else
        {
            _upgradeCost.text = "<color=red>��尡 �����մϴ�.</color>";
            _accapt.interactable = false;
        }
    }
    public void CancelButtonClick()
    {
        _popUpBase.SetActive(false);
    }

    public void UpdateText()
    {
        _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // ���׷��̵� ����.

        _upgradePoint.text = $"<color=yellow>���� ��� : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / �ູ�� : {upgradeScriptableObject.upGradeHappyAffectValue} / <color=blue>ġ�� : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>�ž� : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>��ȭ : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";

        _upgradeCost.text = $"��� : {upgradeScriptableObject.upGradeGold}";
    }
}
