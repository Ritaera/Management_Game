using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData", menuName = "Create Card Data")]
public class CardScriptableObject : ScriptableObject
{
        public Image cardBackgroundSprite; // ī�� ��׶��� 

        public Image cardSprite; // ī�� ������ ���

        public int cardAffectTurn; // ī�尡 ������ ��ġ�� �� 

        public int cardFaithAffectValue; // ī�尡 �žӿ� ������ ��ġ�� ��`
        public int cardCulturalAffectValue; // ī�尡 ��ȭ�� ������ ��ġ�� ��`
        public int cardGoldAffectValue; // ī�尡 ��忡 ������ ��ġ�� ��
        public int cardSafetyAffectValue; // ī�尡 ġ�ȿ� ������ ��ġ�� ��
        public int cardHappyAffectValue; // ī�尡 �ູ�� ������ ��ġ�� ��
        public string cardDescription; // ī�� ����
}