using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    private GameObject _popUpBase; // ��ȭUI ��Ȱ��ȭ ��ų ��ġ
    private GameObject _barrier; // Barrier ��ġ
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

        // ���� ó��(����).
        if (_playerController.GetHitName() == PlayerController2.EHitName.None)
        {
            Utils.LogRed("_playerController.GetHitName()���� None�� ��ȯ�ϸ� �ȵ�.");
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
            _upgradeCost.text = "<color=red>��ȭ�� ��� �����߽��ϴ�.</color>";
            _accapt.interactable = false;
            return;
        }

        if (GameManager.instance.Gold < upgradeScriptableObject.upGradeGold)
        {
            _upgradeCost.text = "<color=red>��尡 �����մϴ�.</color>";
            _accapt.interactable = false;
            return;
        }

        string upgradeDataName = string.Empty;
        GameManager.instance.AddUpgradeList(upgradeScriptableObject);

        switch (_playerController.GetHitName())  // ��ư���� ��� ī��Ʈ 1 ����.
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
            Utils.LogRed("��ȭ�� ��� �����߽��ϴ�. �׸��ϼ���.");
            return;
        }

        UpdateText();

        //// todo => �� case ����  GameManager.instance.AddUpgradeList(upgradeScriptableObject) �߰�
        //// upgradescriptable ������Ʈ �ҷ�����
        //if (GameManager.instance.Gold >= upgradeScriptableObject.upGradeGold)
        //{
        //    string upgradeDataName = string.Empty;
        //    GameManager.instance.AddUpgradeList(upgradeScriptableObject);

        //    switch (_playerController.GetHitName())  // ��ư���� ��� ī��Ʈ 1 ����.
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
        //    _upgradeCost.text = "<color=red>��尡 �����մϴ�.</color>";
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
        _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // ���׷��̵� ����.

        _upgradePoint.text = $"<color=yellow>���� ��� : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / �ູ�� : {upgradeScriptableObject.upGradeHappyAffectValue} / <color=blue>ġ�� : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>�ž� : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>��ȭ : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";

        _upgradeCost.text = $"��� : {upgradeScriptableObject.upGradeGold}";
    }
}
