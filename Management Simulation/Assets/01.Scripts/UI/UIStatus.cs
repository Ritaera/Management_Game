using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    private TMP_Text _date;
    private TMP_Text _gold;
    private TMP_Text _nextUpDown;
    private Button _happyButton;
    private Button _safetyButton;
    private Button _beliefButton;
    private Button _cultureButton;
    private TMP_Text _happyText;
    private TMP_Text _safetyText;
    private TMP_Text _beliefText;
    private TMP_Text _cultureText;
    private Button _tooltipUI;

    private UIDetailView _uiDetailView;

    private void Awake()
    {
        if (_uiDetailView == null)
        {
            _uiDetailView = FindFirstObjectByType<UIDetailView>();
        }

        // ������ �������ͽ�.
        _gold = transform.Find("Status/Status - Date/Text (TMP) - Gold").GetComponent<TMP_Text>(); 
        _date = transform.Find("Status/Status - Date/Text (TMP) - Date").GetComponent<TMP_Text>();
        _nextUpDown = transform.Find("Status/Panel - NextDayUpDown/Text (TMP) - NextDayUpDown").GetComponent<TMP_Text>();
        // ��ư ����Ʈ �̺�Ʈ + ���̵� �ƿ� �̺�Ʈ�� ���� ��ġ.
        _happyButton = transform.Find("Status/Status - Point/HappyPoint/Button - Happy - BG/Button - Happy - Slider").GetComponent<Button>();
        _safetyButton = transform.Find("Status/Status - Point/SafetyPoint/Button - Safety - BG/Button - Safety - Slider").GetComponent<Button>();
        _beliefButton = transform.Find("Status/Status - Point/BeliefPoint/Button - Belief - BG/Button - Belief - Slider").GetComponent<Button>();
        _cultureButton = transform.Find("Status/Status - Point/CulturePoint/Button - Culture - BG/Button - Culture - Slider").GetComponent<Button>();
        // ��ư �ý�Ʈ.
        _happyText = transform.Find("Status/Status - Point/HappyPoint/Text (TMP) - HappyPoint").GetComponent<TMP_Text>();
        _safetyText = transform.Find("Status/Status - Point/SafetyPoint/Text (TMP) - SafetyPoint").GetComponent<TMP_Text>();
        _beliefText = transform.Find("Status/Status - Point/BeliefPoint/Text (TMP) - BeliefPoint").GetComponent<TMP_Text>();
        _cultureText = transform.Find("Status/Status - Point/CulturePoint/Text (TMP) - CulturePoint").GetComponent<TMP_Text>();
        // �ڼ������� ��ư ��ġ.
        _tooltipUI = transform.Find("Status/Panel - NextDayUpDown/Button - DetailView").GetComponent<Button>();
    }

    private void Start()
    {
        GameManager.instance.PointUpdate += StatusUpdate;  // �������ͽ� ������Ʈ ����.
        _tooltipUI.onClick.AddListener(DetailViewButtonClick);  // ��ư �̺�Ʈ ���.
    }


    // KJH => ����Ʈ ���� Ȥ�� �����Ҷ� �� �Լ� ȣ��.
    public void StatusUpdate()
    {
        // ����Ʈ�� MAX ���� �Ѿ�����. ����ó��.
        if (GameManager.instance.HappyPoint.Value > GameManager.instance.HappyPoint.Max || GameManager.instance.SafetyPoint.Value > GameManager.instance.SafetyPoint.Max)
        {
            GameManager.instance.HappyPoint.Value = GameManager.instance.HappyPoint.Max;
            GameManager.instance.SafetyPoint.Value = GameManager.instance.SafetyPoint.Max;
        }
        else if(GameManager.instance.BeliefPoint.Value > GameManager.instance.BeliefPoint.Max)
        {
            GameManager.instance.BeliefPoint.Value = GameManager.instance.BeliefPoint.Max;
        }
        else if(GameManager.instance.CulturePoint.Value > GameManager.instance.CulturePoint.Max)
        {
            GameManager.instance.CulturePoint.Value = GameManager.instance.CulturePoint.Max;
        }
        // ��ư ���̵�ƿ� ��ġ����.
        _happyButton.image.fillAmount = GameManager.instance.HappyPoint.Value / GameManager.instance.HappyPoint.Max;
        _safetyButton.image.fillAmount = GameManager.instance.SafetyPoint.Value / GameManager.instance.SafetyPoint.Max;
        _beliefButton.image.fillAmount = GameManager.instance.BeliefPoint.Value / GameManager.instance.BeliefPoint.Max;
        _cultureButton.image.fillAmount = GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max;
        // ��¥ ��� ���� ������Ʈ.
        _date.text = $"<color=red>��¥ : {GameManager.instance.Date}</color>";
        _gold.text = $"<color=yellow>��� : {GameManager.instance.Gold}</color>";
        // ������ �ٲ� ������ ������Ʈ.
        _nextUpDown.text = $"<color=red>������ ������\n</color><color=yellow>��� : {(GameManager.instance.NextTurnGold > 0 ? "+" : "")}{GameManager.instance.NextTurnGold}</color> / <color=#01DFD7>ġ�� : {(GameManager.instance.NextTurnSafety > 0 ? "+" : "")}{GameManager.instance.NextTurnSafety}</color> / <color=white>�ž� : {(GameManager.instance.NextTurnBelief > 0 ? "+" : "")}{GameManager.instance.NextTurnBelief}</color> / <color=#AC58FA>��ȭ : {(GameManager.instance.NextTurnCulture > 0 ? "+" : "")}{GameManager.instance.NextTurnCulture}</color> <color=#FA58F4>/�ູ : {(GameManager.instance.NextTurnHappy > 0 ? "+" : "")}{GameManager.instance.NextTurnHappy}</color>";
        // Status ���� ǥ�� ������Ʈ.
        _happyText.text = $"{GameManager.instance.HappyPoint.Value}";
        _safetyText.text =$"{GameManager.instance.SafetyPoint.Value}";
        _beliefText.text = $"{GameManager.instance.BeliefPoint.Value}";
        _cultureText.text =$"{GameManager.instance.CulturePoint.Value}";
    }

    public void DetailViewButtonClick()
    {
        _uiDetailView.ShowDetailUI();
    }
}
