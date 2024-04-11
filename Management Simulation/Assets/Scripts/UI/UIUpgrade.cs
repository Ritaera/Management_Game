using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    private GameObject _popUpBase; // 비활성화 시킬 위치
    private TMP_Text _upgradeDescription; // 강화 설명 띄울 TMP 텍스트
    private TMP_Text _upgradePoint; // 강화 수치 띄울 TMP 텍스트
    private TMP_Text _upgradeCost; // 강화 비용 띄울 TMP 텍스트
    private Button _accapt; // 강화수락 버튼
    private Button _cancel; // 강화취소 버튼

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
        if (_playerController.HitName == "Sam smith") //Todo: 대장간 작업 처리.
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"blacksmithLevel{_samUpgradeCount}");
        }
        else if (_playerController.HitName == "Maria") //Todo: 성당처리
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"Cathedral{_mariaUpgradeCount}");
        }
        else if (_playerController.HitName == "Hani") //Todo: 모럼가길드
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"GuildLevel{_haniUpgradeCount}");
        }
        else if (_playerController.HitName == "Hyeji") //Todo: 성
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"CastleLevel{_heyjiUpgradeCount}");
        }
        else                                           //Todo: 은행
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
            switch (_playerController.HitName)  // 버튼누른 대상 카운트 1 증가.
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
            _upgradeCost.text = "<color=red>골드가 부족합니다.</color>";
            _accapt.interactable = false;
        }
    }
    public void CancelButtonClick()
    {
        _popUpBase.SetActive(false);
    }

    public void UpdateText()
    {
        _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // 업그레이드 정보.

        _upgradePoint.text = $"<color=yellow>매턴 골드 : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / 행복도 : {upgradeScriptableObject.upGradeHappyAffectValue} / <color=blue>치안 : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>신앙 : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>문화 : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";

        _upgradeCost.text = $"비용 : {upgradeScriptableObject.upGradeGold}";
    }
}
