using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDetailView : MonoBehaviour
{
    private Button _cancelButton;
    private Image _cardImage;
    private TMP_Text _cardTurn;
    private TMP_Text _cardHappy;
    private TMP_Text _cardSafety;
    private TMP_Text _cardBelief;
    private TMP_Text _cardCulture;
    private GameObject _detail;


    private void Awake()
    {
        _cancelButton = transform.Find("Detail/Panel - Cover/Panel - Title/Button - Cancel").GetComponent<Button>();

        _cardImage = transform.Find("Detail/Panel - Cover/Panel - Contents/Scroll View/Viewport/Content/CardSlot/Image - CardImage").GetComponent<Image>();

        _cardTurn = transform.Find("Detail/Panel - Cover/Panel - Contents/Scroll View/Viewport/Content/CardSlot/Text (TMP) - Turn").GetComponent<TMP_Text>();
        _cardHappy = transform.Find("Detail/Panel - Cover/Panel - Contents/Scroll View/Viewport/Content/CardSlot/Text (TMP) - Happy").GetComponent<TMP_Text>();
        _cardSafety = transform.Find("Detail/Panel - Cover/Panel - Contents/Scroll View/Viewport/Content/CardSlot/Text (TMP) - Safety").GetComponent<TMP_Text>();
        _cardBelief = transform.Find("Detail/Panel - Cover/Panel - Contents/Scroll View/Viewport/Content/CardSlot/Text (TMP) - Belief").GetComponent<TMP_Text>();
        _cardCulture = transform.Find("Detail/Panel - Cover/Panel - Contents/Scroll View/Viewport/Content/CardSlot/Text (TMP) - Culture").GetComponent<TMP_Text>();

        _detail = transform.Find("Detail").gameObject;
    }

    private void Start()
    {
        _cancelButton.onClick.AddListener(CancelButtonClick);
    }



    // 취소버튼 눌렀을때.
    public void CancelButtonClick()
    {
        _detail.SetActive(false);
    }
    // UIStatus.cs 에서 사용하는 함수.
    public void ShowDetailUI()
    {
        _detail.SetActive(true);
    }

    public void SetValue()
    {
        for(int i = 0; i < GameManager.instance.cardlist.Count; i++)
        {
            _cardBelief.text = $"{GameManager.instance.cardlist[i].cardHappyAffectValue}";

            //_cardTurn.text = $"{GameManager.instance.cardTurns[GameManager.instance.previouseSelectedCardIndex[i]]}";

        }
    }
}
