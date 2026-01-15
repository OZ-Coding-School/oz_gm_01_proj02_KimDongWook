using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldCardManager : MonoBehaviour
{
    public static FieldCardManager Instance { get; private set; }

    [Header("유닛 카드 프리팹")]
    [SerializeField] private UnitCardControl unitCardPrefab;

    //===플레이어 스트라이커 유닛===
    [Header("플레이어 스트라이커 유닛 데이터(스크립터블))")]
    [SerializeField] private List<CardData> playerStrikerDatas;

    [Header("플레이어 스트라이커 슬롯")]
    [SerializeField] private List<UnitSlot> playerStrikerSlots;

    //===플레이어 스페셜 유닛===
    [Header("플레이어 스페셜 유닛 데이터(스크립터블))")]
    [SerializeField] private List<CardData> playerSpecialDatas;

    [Header("플레이어 스페셜 슬롯")]
    [SerializeField] private List<UnitSlot> playerSpecialSlots;

    //===적 스트라이커 유닛===
    [Header("적 스트라이커 데이터(스크립터블)")]
    [SerializeField] private List<CardData> enemyStrikerDatas;

    [Header("적 스트라이커 슬롯")]
    [SerializeField] private List<UnitSlot> enemyStrikerSlots;

    //===적 스페셜 유닛===
    [Header("적 스페셜 유닛 데이터(스크립터블))")]
    [SerializeField] private List<CardData> enemySpecialDatas;

    [Header("적 스페셜 슬롯")]
    [SerializeField] private List<UnitSlot> enemySpecialSlots;

    //===카드등록===
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
        if (playerStrikerDatas.Count > 0)
            CreatUnitCard(playerStrikerDatas, playerStrikerSlots, UnitOwner.Player);

        if (playerSpecialDatas.Count > 0)
            CreatUnitCard(playerSpecialDatas, playerSpecialSlots, UnitOwner.Player);

        if (enemyStrikerDatas.Count > 0)
            CreatUnitCard(enemyStrikerDatas, enemyStrikerSlots, UnitOwner.Enemy);

        if (enemySpecialDatas.Count > 0)
            CreatUnitCard(enemySpecialDatas, enemySpecialSlots, UnitOwner.Enemy);
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
            CardData data = unitCards[i];

            if (data == null)
            {
                Debug.LogError($"{owner} 유닛 데이터 [{i}]가 null 입니다");
                continue;
            }

            UnitCardControl card = Instantiate(unitCardPrefab);

            RectTransform cardRect = card.GetComponent<RectTransform>();

            card.SetCardData(unitList[i], owner);

            unitSlots[i].SetUnit(card);
            card.SetSlot(unitSlots[i]);

            UIManager.Instance.FullScreen(cardRect);

            UnitCardUI ui = card.GetComponent<UnitCardUI>();
            ui.BaseTransform();

            card.gameObject.SetActive(true);

            if (owner == UnitOwner.Player)
            {
                AddPlayerUnit(card);

                if (card.IsStriker)
                {
                    ui.SetStartSize();
                }
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
