//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class NPCChack : MonoBehaviour
//{
//    PlayerController2 _playerController;
//    UIUpgrade _uIUpgrade;
//    public UpgradeScriptableObject upgradeScriptableObject;

//    private void Awake()
//    {
//        if (_playerController == null)
//        {
//            _playerController = FindFirstObjectByType<PlayerController2>();
//        }
//        if (_uIUpgrade == null)
//        {
//            _uIUpgrade = FindFirstObjectByType<UIUpgrade>();
//        }
//        if (upgradeScriptableObject == null)
//        {
//            upgradeScriptableObject = FindFirstObjectByType<UpgradeScriptableObject>();
//        }
//    }
//    public void Upgrade()
//    {
//        if (_playerController.HitName == "Sam smith") //Todo: 대장간 작업 처리.
//        {
//            _uIUpgrade.PopUpBase.SetActive(true);
//            _upgradeDescription.text = upgradeScriptableObject.upGradeDescription;  // 업그레이드 정보.

//            _upgradePoint.text = $"<color=yellow>매턴 골드 : {upgradeScriptableObject.upGradeEveryTurnGold}</color> / <color=pink>행복도 : {upgradeScriptableObject.upGradeHappyAffectValue}</color> / <color=blue>치안 : {upgradeScriptableObject.upGradeSafetyAffectValue}</color> / <color=white>신앙 : {upgradeScriptableObject.upGradeFaithAffectValue}</color> / <color=purple>문화 : {upgradeScriptableObject.upGradeCulturalAffectValue}</color>";

//            _upgradeCost.text = $"비용 : {upgradeScriptableObject.upGradeGold}";

//        }
//        else if (_playerController.HitName == "Maria") //Todo: 성당처리
//        {
//            _uIUpgrade.PopUpBase.SetActive(true);

            
//        }
//        else if(_playerController.HitName == "Hani") //Todo: 모럼가길드
//        {
//            _uIUpgrade.PopUpBase.SetActive(true);

            
//        }
//        else if (_playerController.HitName == "Hyeji") //Todo: 성
//        {
//            _uIUpgrade.PopUpBase.SetActive(true);

            
//        }
//        else
//        {
//            _uIUpgrade.PopUpBase.SetActive(true); //Todo: 은행


//        }
//    }
//}
