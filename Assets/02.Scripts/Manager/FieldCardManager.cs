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
    [SerializeField] private List<UnitSlot> playerUnitSlots;

    [Header("적 필드에 존재할 카드들(프리팹 말고 데이터)")]
    [SerializeField] private List<CardData> enemyUnits;

    [Header("플레이어 필드 슬롯")]
    [SerializeField] private List<UnitSlot> enemyUnitSlots;

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

    public void StartAllUnitSummon()
    {
        CreatUnitCard(playerUnits, playerUnitSlots, UnitOwner.Player);
        CreatUnitCard(enemyUnits, enemyUnitSlots, UnitOwner.Enemy);
    }

    public void CreatUnitCard(List<CardData> unitCards, List<UnitSlot> unitSlots, UnitOwner owner)
    {
        List<CardData> unitList = new List<CardData>(unitCards);

        for (int i = 0; i < unitList.Count; i++)
        {
            UnitCardControl card = Instantiate(unitCardPrefab);

            RectTransform cardRect = card.GetComponent<RectTransform>();

            card.SetCardData(unitList[i], owner);

            unitSlots[i].SetUnit(card);
            card.SetSlot(unitSlots[i]);

            UIManager.Instance.FullScreen(cardRect);
            
            UnitCardUI ui = card.GetComponent<UnitCardUI>();
            ui.BaseTransform();
            ui.SetStartSize();

            card.gameObject.SetActive(true);

            if (owner == UnitOwner.Player)
            {
                UnitSelectManager.Instance.AddPlayerUnit(card);
            }
        }
    }
}
