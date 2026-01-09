using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCardManager : MonoBehaviour
{
    public static FieldCardManager Instance { get; private set; }

    [Header("유닛 카드 프리팹")]
    [SerializeField] private UnitCardControl unitCardPrefab;

    [Header("플레이어 필드에 존재할 카드들(프리팹 말고 데이터)")]
    [SerializeField] private List<CardData> playerUnits;

    [Header("플레이어 필드 슬롯")]
    [SerializeField] private List<PlayerUnitSlot> playerUnitSlots;

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

    public void CreatUnitCard()
    {
        List<CardData> unitList = new List<CardData>(playerUnits);

        for (int i = 0; i < unitList.Count; i++)
        {
            UnitCardControl card = Instantiate(unitCardPrefab, playerUnitSlots[i].transform);

            RectTransform cardRect = card.GetComponent<RectTransform>();

            card.IsCardData(unitList[i]);

            card.gameObject.SetActive(true);

            playerUnitSlots[i].SetUnit(card);

            UIManager.Instance.FullScreen(cardRect);
        }
    }
}
