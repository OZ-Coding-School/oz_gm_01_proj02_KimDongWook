using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCardSlot : MonoBehaviour
{
    private HandCardControl hanfCard;

    public void SetFieldCard(HandCardControl card)
    {
        if (hanfCard != null)
        {
            Destroy(hanfCard.gameObject);
        }

        hanfCard = card;
        card.transform.SetParent(transform);
        card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
