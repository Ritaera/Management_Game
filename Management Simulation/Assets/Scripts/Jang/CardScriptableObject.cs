using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData", menuName = "Create Card Data")]
public class CardScriptableObject : ScriptableObject
{
        public Image _cardBackgroundSprite; // ī�� ��׶��� 

        public Image _cardSprite; // ī�� ������ ���

        public int _cardAffectTurn; // ī�尡 ������ ��ġ�� �� 

        public int _cardFaithAffectValue; // ī�尡 �žӿ� ������ ��ġ�� ��`
        public int _cardCulturalAffectValue; // ī�尡 ��ȭ�� ������ ��ġ�� ��`
        public int _cardGoldAffectValue; // ī�尡 ��忡 ������ ��ġ�� ��
        public int _cardSafetyAffectValue; // ī�尡 ġ�ȿ� ������ ��ġ�� ��
        public int _cardHappyAffectValue; // ī�尡 �ູ�� ������ ��ġ�� ��
        public string _cardDescription; // ī�� ����
}