using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData", menuName = "Create Card Data")]
public class CardScriptableObject : ScriptableObject
{
    public Image cardBackgroundImage; // ī�� ��׶��� 

    public Sprite cardImage; // ī�� ������ ���

    public int cardAffectTurn; // ī�尡 ������ ��ġ�� ��.

    public int cardFaithAffectValue; // ī�尡 �žӿ� ������ ��ġ�� ��`
    public int cardCulturalAffectValue; // ī�尡 ��ȭ�� ������ ��ġ�� ��`
    public int cardGoldAffectValue; // ī�尡 ��忡 ������ ��ġ�� ��
    public int cardSafetyAffectValue; // ī�尡 ġ�ȿ� ������ ��ġ�� ��
    public int cardHappyAffectValue; // ī�尡 �ູ�� ������ ��ġ�� ��

    public string cardDescription; // ī�� ����
    
    //�ۼ��� : ���ȣ (2024.04.15)
    // GameManager���� ��꿩�θ� Ȯ���ϱ� ���� bool�� �߰�
    public bool isAlreadycomputation = false;
}