using UnityEngine;
using System.Collections.Generic;

public class CardDataManager : MonoBehaviour
{
    private static CardDataManager instance;
    public static CardDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<CardDataManager>();

                if (instance == null)
                {
                    Debug.LogError($"현재 씬에 {typeof(CardDataManager)} 컴포넌트를 가진 매니저 게임 오브젝트가 없음. 생성 필요함.");
                }
            }

            return instance;
        }
    }

    public CardScriptableObject[] cardData;
    public UpgradeScriptableObject[] upgradeCardData;

    private Dictionary<string, UpgradeScriptableObject> upgradeCardDictionary;

    // Getter for cardData.

    // Getter for upgradeCardData.
    private void Awake()
    {
        upgradeCardDictionary = new Dictionary<string, UpgradeScriptableObject>();

        // 업그레이드용 카드 이름을 키(Key)로 하는 해시테이블 설정.
        foreach (var card in upgradeCardData)
        {
            upgradeCardDictionary.TryAdd(card.name, card);
        }
    }

    // 이름을 키로 사용해 UpgradeScriptableObject를 반환하는 Getter 함수.
    public UpgradeScriptableObject GetUpgradeData(string cardName)
    {
        // 해시테이블에 카드 이름으로 검색을 해본 후 검색 성공하면 해당 카드 반환.
        if (upgradeCardDictionary.TryGetValue(cardName, out UpgradeScriptableObject upgradeData))
        {
            return upgradeData;
        }

        // 검색 실패하면 null 반환.
        return null;
    }
}