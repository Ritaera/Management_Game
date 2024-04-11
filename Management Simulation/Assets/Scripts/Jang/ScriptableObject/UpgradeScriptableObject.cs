using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Create Upgrade Data")]

public class UpgradeScriptableObject : ScriptableObject
{
    public int upGradeEveryTurnGold;             // ���� �����ϴ� ���.
    public int upGradeGold;                       // �����.
    public int upGradeFaithAffectValue;           // �ž� �����.
    public int upGradeCulturalAffectValue;        // ��ȭ �����.
    public int upGradeSafetyAffectValue;          // ġ�� �����.
    public int upGradeHappyAffectValue;          // �ູ�� �����.

    public string upGradeDescription;            // ���׷��̵� ����.
}
