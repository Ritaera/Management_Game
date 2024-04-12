using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    private GameObject _popUpBase; // 강화UI 비활성화 시킬 위치
    private GameObject _barrier; // Barrier 위치
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

        _barrier = transform.Find("Barrier").gameObject;
        _popUpBase = transform.Find("Panel - PopUpBase").gameObject;
        _upgradeDescription = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) - Description").GetComponent<TMP_Text>();
        _upgradePoint = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) -  Plus").GetComponent<TMP_Text>();
        _upgradeCost = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) - Cost").GetComponent<TMP_Text>();
        _accapt = transform.Find("Panel - PopUpBase/Image - Chat/Button - Accapt").GetComponent<Button>(); 
        _cancel = transform.Find("Panel - PopUpBase/Image - Chat/Button - Cancel").GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelButtonClick();
        }
    }


    public void Upgrade()
    {
        _popUpBase.SetActive(true);
        _barrier.SetActive(true);

        // 예외 처리(오류).
        if (_playerController.GetHitName() == PlayerController2.EHitName.None)
        {
            Utils.LogRed("_playerController.GetHitName()에서 None을 반환하면 안됨.");
            return;
        }

        if (_playerController.GetHitName() == PlayerController2.EHitName.SamSmith)
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"blacksmithLevel{_samUpgradeCount}");
        }
        else if (_playerController.GetHitName() == PlayerController2.EHitName.Maria)
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"Cathedral{_mariaUpgradeCount}");
        }
        else if (_playerController.GetHitName() == PlayerController2.EHitName.Hani)
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"GuildLevel{_haniUpgradeCount}");
        }
        else if (_playerController.GetHitName() == PlayerController2.EHitName.Hyeji)
        {
            upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData($"CastleLevel{_heyjiUpgradeCount}");
        }
        else if (_playerController.GetHitName() == PlayerController2.EHitName.Banksy)
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
        if (upgradeScriptableObject == null)
        {
            _upgradeCost.text = "<color=red>강화를 모두 소진했습니다.</color>";
            _accapt.interactable = false;
            return;
        }

        if (GameManager.instance.Gold < upgradeScriptableObject.upGradeGold)
        {
            _upgradeCost.text = "<color=red>골드가 부족합니다.</color>";
            _accapt.interactable = false;
            return;
        }

        string upgradeDataName = string.Empty;
        GameManager.instance.AddUpgradeList(upgradeScriptableObject);

        switch (_playerController.GetHitName())  // 버튼누른 대상 카운트 1 증가.
        {
            case PlayerController2.EHitName.SamSmith:
                upgradeDataName = $"blacksmithLevel{++_samUpgradeCount}";
                break;
            case PlayerController2.EHitName.Maria:
                upgradeDataName = $"Cathedral{++_mariaUpgradeCount}";
                //_mariaUpgradeCount++;
                break;
            case PlayerController2.EHitName.Hani:
                upgradeDataName = $"GuildLevel{++_haniUpgradeCount}";
                //_haniUpgradeCount++;
                break;
            case PlayerController2.EHitName.Hyeji:
                upgradeDataName = $"CastleLevel{++_heyjiUpgradeCount}";
                //_heyjiUpgradeCount++;
                break;
            case PlayerController2.EHitName.Banksy:
                //GameManager.instance.AddUpgradeList(upgradeScriptableObject);
                upgradeDataName = $"Bank{++_banksyUpgradeCount}";
                //_banksyUpgradeCount++;
                break;
            default:
                break;
        }

        upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData(upgradeDataName);
        if (upgradeScriptableObject == null)
        {
            Utils.LogRed("강화를 모두 소진했습니다. 그만하세요.");
            return;
        }

        UpdateText();

        //// todo => 각 case 마다  GameManager.instance.AddUpgradeList(upgradeScriptableObject) 추가
        //// upgradescriptable 오브젝트 불러오기
        //if (GameManager.instance.Gold >= upgradeScriptableObject.upGradeGold)
        //{
        //    string upgradeDataName = string.Empty;
        //    GameManager.instance.AddUpgradeList(upgradeScriptableObject);

        //    switch (_playerController.GetHitName())  // 버튼누른 대상 카운트 1 증가.
        //    {
        //        case PlayerController2.EHitName.SamSmith:
        //            upgradeDataName = $"blacksmithLevel{++_samUpgradeCount}";
        //            break;
        //        case PlayerController2.EHitName.Maria:
        //            upgradeDataName = $"Cathedral{++_mariaUpgradeCount}";
        //            //_mariaUpgradeCount++;
        //            break;
        //        case PlayerController2.EHitName.Hani:
        //            upgradeDataName = $"GuildLevel{++_haniUpgradeCount}";
        //            //_haniUpgradeCount++;
        //            break;
        //        case PlayerController2.EHitName.Hyeji:
        //            upgradeDataName = $"CastleLevel{++_heyjiUpgradeCount}";
        //            //_heyjiUpgradeCount++;
        //            break;
        //        case PlayerController2.EHitName.Banksy:
        //            //GameManager.instance.AddUpgradeList(upgradeScriptableObject);
        //            upgradeDataName = $"Bank{++_banksyUpgradeCount}";
        //            //_banksyUpgradeCount++;
        //            break;
        //        default:
        //            break;
        //    }

        //    upgradeScriptableObject = CardDataManager.Instance.GetUpgradeData(upgradeDataName);
        //    UpdateText();
        //}
        //else
        //{
        //    _upgradeCost.text = "<color=red>골드가 부족합니다.</color>";
        //    _accapt.interactable = false;
        //}

    }
    public void CancelButtonClick()
    {
        _popUpBase.SetActive(false);
        _barrier.SetActive(false);
    }

    public void UpdateText()
    {
        _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // 업그레이드 정보.

        _upgradePoint.text = $"<color=yellow>매턴 골드 : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / 행복도 : {upgradeScriptableObject.upGradeHappyAffectValue} / <color=blue>치안 : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>신앙 : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>문화 : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";

        _upgradeCost.text = $"비용 : {upgradeScriptableObject.upGradeGold}";
    }
}
