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

        // 보여줄 스테이터스.
        _gold = transform.Find("Status/Status - Date/Text (TMP) - Gold").GetComponent<TMP_Text>(); 
        _date = transform.Find("Status/Status - Date/Text (TMP) - Date").GetComponent<TMP_Text>();
        _nextUpDown = transform.Find("Status/Panel - NextDayUpDown/Text (TMP) - NextDayUpDown").GetComponent<TMP_Text>();
        // 버튼 포인트 이벤트 + 페이드 아웃 이벤트를 위한 위치.
        _happyButton = transform.Find("Status/Status - Point/HappyPoint/Button - Happy - BG/Button - Happy - Slider").GetComponent<Button>();
        _safetyButton = transform.Find("Status/Status - Point/SafetyPoint/Button - Safety - BG/Button - Safety - Slider").GetComponent<Button>();
        _beliefButton = transform.Find("Status/Status - Point/BeliefPoint/Button - Belief - BG/Button - Belief - Slider").GetComponent<Button>();
        _cultureButton = transform.Find("Status/Status - Point/CulturePoint/Button - Culture - BG/Button - Culture - Slider").GetComponent<Button>();
        // 버튼 택스트.
        _happyText = transform.Find("Status/Status - Point/HappyPoint/Text (TMP) - HappyPoint").GetComponent<TMP_Text>();
        _safetyText = transform.Find("Status/Status - Point/SafetyPoint/Text (TMP) - SafetyPoint").GetComponent<TMP_Text>();
        _beliefText = transform.Find("Status/Status - Point/BeliefPoint/Text (TMP) - BeliefPoint").GetComponent<TMP_Text>();
        _cultureText = transform.Find("Status/Status - Point/CulturePoint/Text (TMP) - CulturePoint").GetComponent<TMP_Text>();
        // 자세히보기 버튼 위치.
        _tooltipUI = transform.Find("Status/Panel - NextDayUpDown/Button - DetailView").GetComponent<Button>();
    }

    private void Start()
    {
        GameManager.instance.PointUpdate += StatusUpdate;  // 스테이터스 업데이트 구독.
        _tooltipUI.onClick.AddListener(DetailViewButtonClick);  // 버튼 이벤트 등록.
    }


    // KJH => 포인트 증가 혹은 감소할때 이 함수 호출.
    public void StatusUpdate()
    {
        // 포인트가 MAX 값을 넘어갔을경우. 예외처리.
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
        // 버튼 페이드아웃 수치조정.
        _happyButton.image.fillAmount = GameManager.instance.HappyPoint.Value / GameManager.instance.HappyPoint.Max;
        _safetyButton.image.fillAmount = GameManager.instance.SafetyPoint.Value / GameManager.instance.SafetyPoint.Max;
        _beliefButton.image.fillAmount = GameManager.instance.BeliefPoint.Value / GameManager.instance.BeliefPoint.Max;
        _cultureButton.image.fillAmount = GameManager.instance.CulturePoint.Value / GameManager.instance.CulturePoint.Max;
        // 날짜 골드 수량 업데이트.
        _date.text = $"<color=red>날짜 : {GameManager.instance.Date}</color>";
        _gold.text = $"<color=yellow>골드 : {GameManager.instance.Gold}</color>";
        // 다음날 바뀔 수량들 업데이트.
        _nextUpDown.text = $"<color=red>다음날 증감량\n</color><color=yellow>골드 : {(GameManager.instance.NextTurnGold > 0 ? "+" : "")}{GameManager.instance.NextTurnGold}</color> / <color=#01DFD7>치안 : {(GameManager.instance.NextTurnSafety > 0 ? "+" : "")}{GameManager.instance.NextTurnSafety}</color> / <color=white>신앙 : {(GameManager.instance.NextTurnBelief > 0 ? "+" : "")}{GameManager.instance.NextTurnBelief}</color> / <color=#AC58FA>문화 : {(GameManager.instance.NextTurnCulture > 0 ? "+" : "")}{GameManager.instance.NextTurnCulture}</color> <color=#FA58F4>/행복 : {(GameManager.instance.NextTurnHappy > 0 ? "+" : "")}{GameManager.instance.NextTurnHappy}</color>";
        // Status 숫자 표기 업데이트.
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
