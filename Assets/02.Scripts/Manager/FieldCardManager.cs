using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldCardManager : MonoBehaviour
{
    public static FieldCardManager Instance { get; private set; }

    [Header("유닛 카드 프리팹")]
    [SerializeField] private UnitCardControl unitCardPrefab;

    [Header("플레이어 필드 유닛 데이터(스크립터블))")]
    [SerializeField] private List<CardData> playerUnitDatas;

    [Header("플레이어 필드 슬롯")]
    [SerializeField] private List<UnitSlot> playerUnitSlots;

    [Header("적 필드 유닛 데이터(스크립터블)")]
    [SerializeField] private List<CardData> enemyUnitDatas;

    [Header("플레이어 필드 슬롯")]
    [SerializeField] private List<UnitSlot> enemyUnitSlots;

    [Header("플레이어 유닛 카드를 등록")]
    [SerializeField] private List<UnitCardControl> playerUnits;
    
    [Header("적 유닛 카드를 등록")]
    [SerializeField] private List<UnitCardControl> enemyUnits;
    
    public List<UnitCardControl> PlayerUnits
    {
        get { return playerUnits; }
    }
    public List<UnitCardControl> EnemyUnits
    {
        get { return enemyUnits; }
    }


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
        CreatUnitCard(playerUnitDatas, playerUnitSlots, UnitOwner.Player);
        CreatUnitCard(enemyUnitDatas, enemyUnitSlots, UnitOwner.Enemy);
    }
    //실제 플레이어 유닛 카드 등록
    public void AddPlayerUnit(UnitCardControl unitCard)
    {
        if (!playerUnits.Contains(unitCard))
        {
            playerUnits.Add(unitCard);
        }
    }
    public void AddEnemyUnit(UnitCardControl unitCard)
    {
        if (!enemyUnits.Contains(unitCard))
        {
            enemyUnits.Add(unitCard);
        }
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
                AddPlayerUnit(card);
            }
            else if (owner == UnitOwner.Enemy)
            {
                AddEnemyUnit(card);
            }
        }
    }

    public UnitCardControl FrontEnemyCheck()
    {
        foreach (var enemyUnit in enemyUnits)
        {
            if (enemyUnit != null && enemyUnit.IsFront && !enemyUnit.IsDead)
            {
                return enemyUnit;
            }
        }

        return null;
    }    
    
}
