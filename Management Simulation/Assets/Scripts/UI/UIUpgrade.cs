using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIUpgrade : MonoBehaviour
{
    private GameObject _popUpBase; // ��Ȱ��ȭ ��ų ��ġ
    private TMP_Text _upgradeDescription; // ��ȭ ���� ��� TMP �ؽ�Ʈ
    private TMP_Text _upgradePoint; // ��ȭ ��ġ ��� TMP �ؽ�Ʈ
    private TMP_Text _upgradeCost; // ��ȭ ��� ��� TMP �ؽ�Ʈ
    private Button _accapt; // ��ȭ���� ��ư
    private Button _cancel; // ��ȭ��� ��ư

    private int _samUpgradeCount = 0;
    private int _mariaUpgradeCount = 0;
    private int _heyjiUpgradeCount = 0;
    private int _haniUpgradeCount = 0;
    private int _banksyUpgradeCount = 0;

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

        _popUpBase = transform.Find("Panel - PopUpBase").GetComponent<GameObject>();
        _upgradeDescription = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) - Description").GetComponent<TMP_Text>();
        _upgradePoint = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) -  Plus").GetComponent<TMP_Text>();
        _upgradeCost = transform.Find("Panel - PopUpBase/Image - Chat/Text(TMP) - Cost").GetComponent<TMP_Text>();
        _accapt = transform.Find("Panel - PopUpBase/Image - Chat/Button - Accapt").GetComponent<Button>(); 
        _cancel = transform.Find("Panel - PopUpBase/Image - Chat/Button - Cancel").GetComponent<Button>();
    }

    public void Upgrade()
    {
        if (_playerController.HitName == "Sam smith") //Todo: ���尣 �۾� ó��.
        {
            PopUpBase.SetActive(true);
            
            switch (_samUpgradeCount)
            {
                case 0:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/blacksmithLevel1");
                    UpdateText();
                    break;
                case 1:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/blacksmithLevel2");
                    UpdateText();
                    break;
                case 2:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/blacksmithLevel3");
                    UpdateText();
                    break;
                case 3:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/blacksmithLevel4");
                    UpdateText();
                    break;
                case 4:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/blacksmithLevel5");
                    UpdateText();
                    break;
                case 5:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/blacksmithLevel6");
                    UpdateText();
                    break;
                default:
                    break;
            }
        }
        else if (_playerController.HitName == "Maria") //Todo: ����ó��
        {
            PopUpBase.SetActive(true);
            UpdateText();
            switch (_mariaUpgradeCount)
            {
                case 0:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Cathedral1");
                    UpdateText();
                    break;
                case 1:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Cathedral2");
                    UpdateText();
                    break;
                case 2:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Cathedral3");
                    UpdateText();
                    break;
                case 3:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Cathedral4");
                    UpdateText();
                    break;
                case 4:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Cathedral5");
                    UpdateText();
                    break;
                case 5:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Cathedral6");
                    UpdateText();
                    break;
                default:
                    break;
            }
        }
        else if (_playerController.HitName == "Hani") //Todo: �𷳰����
        {
            PopUpBase.SetActive(true);
            UpdateText();
            switch (_haniUpgradeCount)
            {
                case 0:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/GuildLevel1");
                    UpdateText();
                    break;
                case 1:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/GuildLevel2");
                    UpdateText();
                    break;
                case 2:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/GuildLevel3");
                    UpdateText();
                    break;
                case 3:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/GuildLevel4");
                    UpdateText();
                    break;
                case 4:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/GuildLevel5");
                    UpdateText();
                    break;
                case 5:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/GuildLevel6");
                    UpdateText();
                    break;
                default:
                    break;
            }
        }
        else if (_playerController.HitName == "Hyeji") //Todo: ��
        {
            PopUpBase.SetActive(true);
            UpdateText();
            switch (_heyjiUpgradeCount)
            {
                case 0:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/CastleLevel1");
                    UpdateText();
                    break;
                case 1:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/CastleLevel2");
                    UpdateText();
                    break;
                case 2:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/CastleLevel3");
                    UpdateText();
                    break;
                case 3:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/CastleLevel4");
                    UpdateText();
                    break;
                case 4:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/CastleLevel5");
                    UpdateText();
                    break;
                case 5:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/CastleLevel6");
                    UpdateText();
                    break;
                default:
                    break;
            }
        }
        else
        {
            PopUpBase.SetActive(true); //Todo: ����
            UpdateText();
            switch (_banksyUpgradeCount)
            {
                case 0:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Bank1");
                    UpdateText();
                    break;
                case 1:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Bank2");
                    UpdateText();
                    break;
                case 2:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Bank3");
                    UpdateText();
                    break;
                case 3:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Bank4");
                    UpdateText();
                    break;
                case 4:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Bank5");
                    UpdateText();
                    break;
                case 5:
                    Resources.Load<UpgradeScriptableObject>("UpgradeScriptableObject/Bank6");
                    UpdateText();
                    break;
                default:
                    break;
            }
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

        _upgradePoint.text = $"<color=yellow>���� ��� : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / <color=pink>�ູ�� : {upgradeScriptableObject.upGradeHappyAffectValue}</color> / <color=blue>ġ�� : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>�ž� : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>��ȭ : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";

        _upgradeCost.text = $"��� : {upgradeScriptableObject.upGradeGold}";
    }
}
