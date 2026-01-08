using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [Header("카드 프리팹")]
    [SerializeField] private HandCardControl handcardPrefab;

    [Header("덱에 존재하는 카드들(프리팹 말고 데이터)")]
    [SerializeField] private List<CardData> deckDataList;

    [Header("손 패")]
    [SerializeField] private HandZone handZone;

    private Queue<HandCardControl> deck = new Queue<HandCardControl>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //게임 시작하면 플레이어의 카드가 덱으로 이동
    public void CreatDeck()
    {
        List<CardData> deckList = new List<CardData>(deckDataList);
        DeckShuffle(deckList);

        foreach (var data in deckList)
        {
            HandCardControl card = Instantiate(handcardPrefab, transform);

            //card.CardData = data;
            card.IsCardData(data);
            card.gameObject.SetActive(false);
            deck.Enqueue(card);
        }
    }
    //카드 드로우!
    public void DrawCard()
    {
        if (deck.Count == 0) return;

        HandCardControl card = deck.Dequeue();
        card.gameObject.SetActive(true);
        handZone.AddCard(card);

        card.ShowToFront();
    }
    //덱 셔플
    private void DeckShuffle(List<CardData> deckList)
    {
        for (int i = 0; i < deckList.Count - 1;i++)
        {
            int rand = Random.Range(i, deckList.Count);
            //(deckList[i], deckList[rand]) = (deckList[rand], deckList[i]);

            var temp = deckList[i];
            deckList[i] = deckList[rand];
            deckList[rand] = temp;
        }
    }
}
