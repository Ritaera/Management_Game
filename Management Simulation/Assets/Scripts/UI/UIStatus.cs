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


    // KJH => 포인트 증가 혹은 감소할때 이 함수 호출.
    public void StatusUpdate()
    {
        _happyButton.fillAmount = GameManager.instance.HappyPoint.Value / GameManager.instance.HappyPoint.Max;
        _safetyButton.fillAmount = GameManager.instance.SafetyPoint.Value / GameManager.instance.SafetyPoint.Max;
        _beliefButton.fillAmount = GameManager.instance.BeliefPoint.Value / GameManager.instance.BeliefPoint.Max;
        _cultureButton.fillAmount = GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max;
        _date.text = $"<color=red>날짜 : {GameManager.instance.Date}</color>";
        _gold.text = $"<color=yellow>골드 : {GameManager.instance.Gold}</color>";

        _nextUpDown.text = $"<color=red>다음날 증감량</color> \n<color=yellow>골드 : {(GameManager.instance.NextTurnGold > 0 ? "+" : "")}{GameManager.instance.NextTurnGold}</color> " +
            $"/ <color=blue>치안 : {(GameManager.instance.NextTurnSafety > 0 ? "+" : "")}{GameManager.instance.NextTurnSafety}</color> " +
            $"/ <color=white>신앙 : {(GameManager.instance.NextTurnBelief > 0 ? "+" : "")}{GameManager.instance.NextTurnBelief}</color> " +
            $"/ <color=purple>문화 : {(GameManager.instance.NextTurnCulture > 0 ? "+" : "")} {GameManager.instance.NextTurnCulture}</color>";
    }

}
