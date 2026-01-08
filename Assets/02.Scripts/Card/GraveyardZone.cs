using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardZone : MonoBehaviour
{
    public static GraveyardZone Instance { get; private set; }

    [SerializeField] private HandZone handZone;

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
    }
}
