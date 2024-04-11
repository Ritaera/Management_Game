using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData", menuName = "Create Card Data")]
public class CardScriptableObject : ScriptableObject
{
        public Image cardBackgroundSprite; // 카드 백그라운드 

        public Image cardSprite; // 카드 내용의 배경

        public int cardAffectTurn; // 카드가 영향을 미치는 턴 

        public int cardFaithAffectValue; // 카드가 신앙에 영향을 미치는 값`
        public int cardCulturalAffectValue; // 카드가 문화에 영향을 미치는 값`
        public int cardGoldAffectValue; // 카드가 골드에 영향을 미치는 값
        public int cardSafetyAffectValue; // 카드가 치안에 영향을 미치는 값
        public int cardHappyAffectValue; // 카드가 행복에 영향을 미치는 값
        public string cardDescription; // 카드 설명
}