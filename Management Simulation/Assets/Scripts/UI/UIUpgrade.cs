using System.Collections;
using System.Collections.Generic;
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
        _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // ���׷��̵� ����.

        _upgradePoint.text = $"<color=yellow>���� ��� : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / <color=pink>�ູ�� : {upgradeScriptableObject.upGradeHappyAffectValue}</color> / <color=blue>ġ�� : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>�ž� : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>��ȭ : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";
        
        _upgradeCost.text = $"��� : {upgradeScriptableObject.upGradeGold}";
    }
    public void CancelButtonClick()
    {
        _popUpBase.SetActive(false);
    }
}
