using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCardSlot : MonoBehaviour
{
    private CardView cardView;

    public void SetFieldCard(CardView card)
    {
        if (cardView != null)
        {
            Destroy(cardView.gameObject);
        }

        cardView = card;
        card.transform.SetParent(transform);
        card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
