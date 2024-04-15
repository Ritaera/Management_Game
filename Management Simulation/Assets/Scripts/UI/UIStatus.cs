using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    private TMP_Text _date;
    private TMP_Text _gold;
    private TMP_Text _nextUpDown;
    private Image _happyButton;
    private Image _safetyButton;
    private Image _beliefButton;
    private Image _cultureButton;

    private void Awake()
    {
        _gold = transform.Find("Status/Status - Date/Text (TMP) - Gold").GetComponent<TMP_Text>(); 
        _date = transform.Find("Status/Status - Date/Text (TMP) - Date").GetComponent<TMP_Text>();
        _nextUpDown = transform.Find("Status/Panel - NextDayUpDown/Text (TMP) - NextDayUpDown").GetComponent<TMP_Text>();
        _happyButton = transform.Find("Status/Status - Point/HappyPoint/Button - Happy - BG/Button - Happy - Slider").GetComponent<Image>();
        _safetyButton = transform.Find("Status/Status - Point/SafetyPoint/Button - Safety - BG/Button - Safety - Slider").GetComponent<Image>();
        _beliefButton = transform.Find("Status/Status - Point/BeliefPoint/Button - Belief - BG/Button - Belief - Slider").GetComponent<Image>();
        _cultureButton = transform.Find("Status/Status - Point/CulturePoint/Button - Culture - BG/Button - Culture - Slider").GetComponent<Image>();
    }

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
        _date.text = $"<color=red>��¥ : {GameManager.instance.Date}</color>";
        _gold.text = $"<color=yellow>��� : {GameManager.instance.Gold}</color>";

        _nextUpDown.text = $"<color=red>������ ������</color> \n<color=yellow>��� : {(GameManager.instance.NextTurnGold > 0 ? "+" : "")}{GameManager.instance.NextTurnGold}</color> " +
            $"/ <color=blue>ġ�� : {(GameManager.instance.NextTurnSafety > 0 ? "+" : "")}{GameManager.instance.NextTurnSafety}</color> " +
            $"/ <color=white>�ž� : {(GameManager.instance.NextTurnBelief > 0 ? "+" : "")}{GameManager.instance.NextTurnBelief}</color> " +
            $"/ <color=purple>��ȭ : {(GameManager.instance.NextTurnCulture > 0 ? "+" : "")} {GameManager.instance.NextTurnCulture}</color>";
    }

}
