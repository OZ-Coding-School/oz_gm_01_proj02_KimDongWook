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
    private UnitCardControl selectUnit;

    private bool firstCheckFront = true;
    public bool FirstCheckFront
    {
        get {  return firstCheckFront; }
        set { firstCheckFront = value; }
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
    public void SelectUnit(UnitCardControl unitCard)
    {
        if (!TurnManager.Instance.IsPlayerTurn) return;

        if (selectUnit != null)
        {
            selectUnit.OnDeselect();
        }

        selectUnit = unitCard;

        selectUnit.OnSelect();

        UIManager.Instance.OkButtonTrue();
    }
    private void PlayerUnitSwap()
    {
        foreach (var unit in FieldCardManager.Instance.PlayerUnits)
        {
            if (unit == selectUnit)
            {
                unit.SetUnitPosition(UnitPosition.Front);
                unit.UnitFront();
            }
            else
            {
                unit.SetUnitPosition(UnitPosition.Back);
                unit.UnitBack();
            }
        }
    }
    private void PlayerUnitAttack()
    {
        Debug.Log($" 플레이어 유닛 [{selectUnit.name}] 일반 공격 실행");

        selectUnit.UnitDoAttack();

        UIManager.Instance.NoButtonTrue();

    }
    public void OnClickSwapAndAttack()
    {
        if (selectUnit == null) return;
        if (TurnManager.Instance.IsEnemyTrun) return; //적의 턴이면 클릭 못하게
        Debug.Log($"전위 : {selectUnit.IsFront}, 후위 : {selectUnit.IsBack}" );

        if (selectUnit.IsNone)
        {
            PlayerUnitSwap();
            UIManager.Instance.EndButtonTrue();
            firstCheckFront = false;
            return;
        }

        if (selectUnit.IsBack)
        {
            if (!CostManager.Instance.UseCost(1)) return;

            PlayerUnitSwap();
            UIManager.Instance.EndButtonTrue();
            return;
        }

        if (selectUnit.IsFront || selectUnit.IsSpecial)
        {
            PlayerUnitAttack();
            TurnManager.Instance.EndPlayerTurn();
        }

        UIManager.Instance.EndButtonTrue();
    }

    public void ClearSelectUnit()
    {
        if (selectUnit != null)
        {
            selectUnit.OnDeselect();
            selectUnit = null;
        }
    }
    //public void OnClickSwapAndAttack()
    //{
    //    if (selectUnit == null) return;
    //    if (TurnManager.Instance.IsEnemyTrun) return; //적의 턴이면 클릭 못하게
    //    if (!CostManager.Instance.UseCost(1))
    //    {
    //        Debug.Log(CostManager.Instance.CurrentCost);
    //        return;
    //    }
    //    //decisionSelect = true;
    //
    //
    //    foreach (var unit in FieldCardManager.Instance.PlayerUnits)
    //    {
    //        if (unit == selectUnit)
    //        {
    //            unit.SetUnitPosition(UnitPosition.Front);
    //            unit.UnitFront();
    //        }
    //        else
    //        {
    //            unit.SetUnitPosition(UnitPosition.Back);
    //            unit.UnitBack();
    //        }
    //    }
    //
    //    UIManager.Instance.okButton.gameObject.SetActive(false);
    //    UIManager.Instance.endButton.gameObject.SetActive(true);
    //}
}
