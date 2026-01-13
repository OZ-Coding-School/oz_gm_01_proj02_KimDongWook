using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCardSlot : MonoBehaviour
{
    private HandCardControl handCard;

    public void SetFieldCard(HandCardControl card)
    {
        if (handCard != null)
        {
            Destroy(handCard.gameObject);
        }

        handCard = card;
        card.transform.SetParent(transform);
        card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
