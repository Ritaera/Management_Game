using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Create Upgrade Data")]

public class UpgradeScriptableObject : ScriptableObject
{
    public int upGradeEveryTurnGold;
    public int upGradeGold;
    public int upGradeFaithAffectValue; 
    public int upGradeCulturalAffectValue; 
    public int upGradeSafetyAffectValue;
    public int upGradeHappyAffectValue; 

    public string upGradeDescription; 




}
