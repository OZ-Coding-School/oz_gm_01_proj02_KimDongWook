using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandZone : MonoBehaviour
{
    [SerializeField] float cardWidth = 160f;   //카드 한 장의 폭
    [SerializeField] float baseSpacing = 100f;  //카드 사이 기본 간격
    [SerializeField] float minSpacing = 15f;   //카드 사이 최소 간격

    private List<HandCardControl> handCards = new List<HandCardControl>();

    //손 패에 카드 추가
    public void AddCard(HandCardControl card)
    {
        handCards.Add(card);

        card.transform.SetParent(transform);  //덱에서 꺼낸 카드를 손패를 부모로 넣기
        card.ShowToFront();

        ArrangeCard();
    }

    //패에 있는 카드가 패에서 다른 곳으로 이동할 때(필드, 묘지)
    public void RemoveCard(HandCardControl card)
    {
        handCards.Remove(card);
        ArrangeCard();
    }

    public void ArrangeCard()
    {
        if (handCards.Count == 0) return;

        float spacing = baseSpacing;

        if (handCards.Count > 5)
        {
            // 손 패의 카드 수가 5를 넘으면 1장당 5f씩 카드 사이 간격이 줄어듬
            spacing = Mathf.Max(minSpacing, baseSpacing - (handCards.Count - 5) * 5f); 
        }

        //처음 받는 손 패 5장의 좌표 계산
        float startX = -((handCards.Count - 1) * spacing) / 2f;

        for (int i = 0; i < handCards.Count; i++)
        {
            RectTransform rt = handCards[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + spacing * i, 0);
        }
    }
}
