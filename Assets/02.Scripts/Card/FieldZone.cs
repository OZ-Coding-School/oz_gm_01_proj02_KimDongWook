using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldZone : MonoBehaviour, IDropHandler
{
    public FieldUnit fieldUnit;
    public bool isPlayerZone;

    public void OnDrop(PointerEventData eventData)
    {
        CardView card = eventData.pointerDrag.GetComponent<CardView>();

        if (card == null) return;

        if (card.CardData.cardType != CardType.Striker && card.CardData.cardType != CardType.Special
            && card.CardData.cardType != CardType.Field)
            return;

        //필드 이동
        card.transform.SetParent(transform);
        card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        card.ShowFront();
    }
}
