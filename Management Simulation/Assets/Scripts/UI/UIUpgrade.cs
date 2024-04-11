using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIUpgrade : MonoBehaviour
{
    private GameObject _popUpBase; // 비활성화 시킬 위치
    private TMP_Text _upgradeDescription; // 강화 설명 띄울 TMP 텍스트
    private TMP_Text _upgradePoint; // 강화 수치 띄울 TMP 텍스트
    private TMP_Text _upgradeCost; // 강화 비용 띄울 TMP 텍스트
    private Button _accapt; // 강화수락 버튼
    private Button _cancel; // 강화취소 버튼

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

    public UpgradeScriptableObject upgradeScriptableObject;

    private void Awake()
    {
        if (upgradeScriptableObject == null)
        {
            upgradeScriptableObject = FindFirstObjectByType<UpgradeScriptableObject>();
        }

        _popUpBase = transform.Find("Panel - PopUpBase").GetComponent<GameObject>();
        _upgradeDescription = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) - Description").GetComponent<TMP_Text>();
        _upgradePoint = transform.Find("Panel - PopUpBase/Image - Chat/Text (TMP) -  Plus").GetComponent<TMP_Text>();
        _upgradeCost = transform.Find("Panel - PopUpBase/Image - Chat/Text(TMP) - Cost").GetComponent<TMP_Text>();
        _accapt = transform.Find("Panel - PopUpBase/Image - Chat/Button - Accapt").GetComponent<Button>(); 
        _cancel = transform.Find("Panel - PopUpBase/Image - Chat/Button - Cancel").GetComponent<Button>();
    }

    private void Start()
    {
        _accapt.onClick.AddListener(AccaptButtonClick);
        _cancel.onClick.AddListener(CancelButtonClick);
    }

    public void AccaptButtonClick()
    {
        _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // 업그레이드 정보.

        _upgradePoint.text = $"<color=yellow>매턴 골드 : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / <color=pink>행복도 : {upgradeScriptableObject.upGradeHappyAffectValue}</color> / <color=blue>치안 : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>신앙 : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>문화 : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";
        
        _upgradeCost.text = $"비용 : {upgradeScriptableObject.upGradeGold}";
    }
    public void CancelButtonClick()
    {
        _popUpBase.SetActive(false);
    }
}
