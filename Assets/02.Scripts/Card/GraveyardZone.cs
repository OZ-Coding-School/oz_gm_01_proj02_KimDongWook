using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardZone : MonoBehaviour
{
    public static GraveyardZone Instance { get; private set; }

    [SerializeField] private HandZone handZone;

    [SerializeField] private Image graveyardImage;
    [SerializeField] private TextMeshProUGUI graveyardCountText;
    [SerializeField] private Image graveyardZero;

    private Queue<HandCardControl> graveyardDeck = new Queue<HandCardControl>();

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

    public void GoToGrave(HandCardControl card)
    {
        card.transform.SetParent(transform);
        card.gameObject.SetActive(false);
        handZone.RemoveCard(card);
        graveyardDeck.Enqueue(card);
    }

    public void GraveyardView()
    {
        int graveyardCount = graveyardDeck.Count;

        graveyardCountText.text = graveyardCount.ToString();

        if (graveyardCount > 0)
        {
            graveyardImage.gameObject.SetActive(true);
            graveyardZero.gameObject.SetActive(false);
        }
        else
        {
            graveyardImage.gameObject.SetActive(false);
            graveyardZero.gameObject.SetActive(true);
        }
    }
}
