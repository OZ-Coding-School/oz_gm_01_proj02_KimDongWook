using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<CardData> deckDataList;

    [SerializeField] private HandZone handZone;

    private Queue<GameObject> deck = new Queue<GameObject>();
    private List<GameObject> grave = new List<GameObject>();

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

    private void Start()
    {
        CreatDeck();

        for (int i = 0; i < 4;  i++)
        {
            DrawCard();
        }

        DrawCard();
    }
    //게임 시작하면 플레이어의 카드가 덱으로 이동(아직 미완성 메서드)
    private void CreatDeck()
    {
        foreach (var data in deckDataList)
        {
            GameObject card = Instantiate(cardPrefab);
            card.SetActive(false);

            CardView cardView = card.GetComponent<CardView>();
            cardView.CardData = data;
            cardView.ShowBack();

            deck.Enqueue(card);
        }

        //리스트로 만들고

        //랜덤 셔플

        //다시 큐에 담기
    }
    //카드 드로우!
    public void DrawCard()
    {
        if (deck.Count == 0)
        {
            foreach(var isCard in grave)
            {
                deck.Enqueue(isCard);
            }

            //메모리 누수 방지, 데이터 중복 및 오류 방지
            grave.Clear();
        }


        if (deck.Count == 0) return;

        GameObject card = deck.Dequeue();
        card.SetActive(true);

        CardView cardView = card.GetComponent<CardView>();
        handZone.AddCard(cardView);
    }
}
