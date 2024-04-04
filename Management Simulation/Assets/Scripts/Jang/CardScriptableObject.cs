using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData", menuName = "Create Card Data")]
public class CardScriptableObject : ScriptableObject
{
        public Image _cardBackgroundSprite; // 카드 백그라운드 

        public Image _cardSprite; // 카드 내용의 배경

        public int _cardAffectTurn; // 카드가 영향을 미치는 턴 

        public int _cardFaithAffectValue; // 카드가 신앙에 영향을 미치는 값`
        public int _cardCulturalAffectValue; // 카드가 문화에 영향을 미치는 값`
        public int _cardGoldAffectValue; // 카드가 골드에 영향을 미치는 값
        public int _cardSafetyAffectValue; // 카드가 치안에 영향을 미치는 값
        public int _cardHappyAffectValue; // 카드가 행복에 영향을 미치는 값
        public string _cardDescription; // 카드 설명
}