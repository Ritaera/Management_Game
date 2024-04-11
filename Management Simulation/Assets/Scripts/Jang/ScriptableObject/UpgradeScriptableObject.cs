using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Create Upgrade Data")]

public class UpgradeScriptableObject : ScriptableObject
{
    public int upGradeEveryTurnGold;             // 매턴 증가하는 골드.
    public int upGradeGold;                       // 사용골드.
    public int upGradeFaithAffectValue;           // 신앙 영향력.
    public int upGradeCulturalAffectValue;        // 문화 영향력.
    public int upGradeSafetyAffectValue;          // 치안 영향력.
    public int upGradeHappyAffectValue;          // 행복도 영향력.

    public string upGradeDescription;            // 업그레이드 정보.
}
