using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab;
    [SerializeField] private HandZone handZone;

    private Queue<GameObject> deck = new Queue<GameObject>();

    private void Start()
    {
        CreatDeck();
    }
    //게임 시작하면 플레이어의 카드가 덱으로 이동(아직 미완성 메서드)
    private void CreatDeck()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject card = Instantiate(cardPrefab[i]);
            card.SetActive(false);
            deck.Enqueue(card);
        }

        //리스트로 만들고

        //랜덤 셔플

        //다시 큐에 담기
    }
    public void DrawCard()
    {
        if (deck.Count == 0) return;

        GameObject card = deck.Dequeue();
        card.SetActive(true);

        CardView cardView = card.GetComponent<CardView>();
        handZone.AddCard(cardView);
    }
}
