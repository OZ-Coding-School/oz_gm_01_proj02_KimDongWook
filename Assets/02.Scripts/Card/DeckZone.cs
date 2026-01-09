using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckZone : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image deckImage;
    [SerializeField] private TextMeshProUGUI deckCountText;
    [SerializeField] private Image deckZero;

    public void OnPointerClick(PointerEventData eventData)
    {
        DeckManager.Instance.DrawCard();
    }

    public void DeckView(Queue<HandCardControl> handCard, int deckCount)
    {
        deckCountText.text = deckCount.ToString();

        if (deckCount > 0)
        {
            deckImage.gameObject.SetActive(true);
            deckZero.gameObject.SetActive(false);
        }
        else
        {
            deckImage.gameObject.SetActive(false);
            deckZero.gameObject.SetActive(true);
        }
    }
}
