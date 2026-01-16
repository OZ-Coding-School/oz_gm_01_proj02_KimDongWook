using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectManager : MonoBehaviour
{
    public static UnitSelectManager Instance { get; private set; }

    //[Header("플레이어 유닛 카드를 등록")]
    //[SerializeField] private List<UnitCardControl> playerUnits = new List<UnitCardControl>();
    private UnitCardControl selectCard;

    private bool firstCheckFront = true;
    public bool FirstCheckFront
    {
        get {  return firstCheckFront; }
        set { firstCheckFront = value; }
    }

    public UnitCardControl SelectCard
    {
        get { return selectCard; }
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
    //유닛 카드 선택
    public void SelectUnit(UnitCardControl unitCard)
    {
        if (!TurnManager.Instance.IsPlayerTurn) return;

        if (selectCard != null)
        {
            //먼저 선택 된 카드는 해제
            selectCard.OnDeselect();
        }

        selectCard = unitCard;

        selectCard.OnSelect();

        UIManager.Instance.OkButtonTrue();

        Debug.Log(selectCard.UnitPosition);
    }
    //전위,후위 스왑
    private void PlayerUnitSwap()
    {
        foreach (var unit in FieldCardManager.Instance.PlayerUnits)
        {
            if (unit == selectCard)
            {
                unit.SetUnitPosition(UnitPosition.Front);
                unit.UnitFront();
            }
            else
            {
                if (unit.IsSpecial) return;
                unit.SetUnitPosition(UnitPosition.Back);
                unit.UnitBack();
            }
        }
    }
    //전위 공격 시작
    private void PlayerUnitAttack()
    {
        Debug.Log($" 플레이어 유닛 [{selectCard.name}] 일반 공격 실행");

        selectCard.UnitDoAttack();

        UIManager.Instance.NoButtonTrue();
    }
    public void OnClickSwapAndAttack()
    {
        if (selectCard == null) return;
        if (TurnManager.Instance.IsEnemyTrun) return; //적의 턴이면 클릭 못하게
        if (selectCard.IsEnemy) return;

        if (!selectCard.IsSpecial)
        {
            switch (selectCard.UnitPosition)
            {
                case UnitPosition.None:
                    if (selectCard.IsSpecial) return;
                    PlayerUnitSwap();
                    UIManager.Instance.EndButtonTrue();
                    firstCheckFront = false;
                    break;
                case UnitPosition.Back:
                    if (!CostManager.Instance.UseCost(1)) return;
                    PlayerUnitSwap();
                    UIManager.Instance.EndButtonTrue();
                    break;
                case UnitPosition.Front:
                    if (!CostManager.Instance.UseCost(selectCard.CardData.cost)) return;
                    PlayerUnitAttack();
                    TurnManager.Instance.EndPlayerTurn();
                    UIManager.Instance.NoButtonTrue();
                    break;
            }
        }
        else
        {
            if (firstCheckFront) return;
            if (!CostManager.Instance.UseCost(selectCard.CardData.cost)) return;
            PlayerUnitAttack();
            UIManager.Instance.EndButtonTrue();
        }
    }

    public void ClearSelectUnit()
    {
        if (selectCard != null)
        {
            selectCard.OnDeselect();
            selectCard = null;
        }
    }
}
