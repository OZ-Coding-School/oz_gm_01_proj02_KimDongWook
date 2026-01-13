using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectManager : MonoBehaviour
{
    public static UnitSelectManager Instance { get; private set; }

    [Header("플레이어 유닛 카드를 등록")]
    [SerializeField] private List<UnitCardControl> playerUnits = new List<UnitCardControl>();
    private UnitCardControl selectUnit;

    //카드 선택 확정
    private bool decisionSelect = false;

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
    //필드에 유닛이 소환될 때 호출
    public void AddPlayerUnit(UnitCardControl unitCard)
    {
        if(!playerUnits.Contains(unitCard))
        {
            playerUnits.Add(unitCard);
        }
    }

    public void SelectUnit(UnitCardControl unitCard)
    {
        if (decisionSelect) return;

        if (selectUnit != null)
        {
            selectUnit.OnDeselect();
        }

        selectUnit = unitCard;

        selectUnit.OnSelect();

        UIManager.Instance.okButton.gameObject.SetActive(true);
        UIManager.Instance.endButton.gameObject.SetActive(false);
    }

    public void OnClickUnit()
    {
        if (selectUnit == null) return;
        if (TurnManager.Instance.IsEnemyTrun) return; //적의 턴이면 클릭 못하게

        //decisionSelect = true;

        foreach (var unit in playerUnits)
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

        UIManager.Instance.okButton.gameObject.SetActive(false);
        UIManager.Instance.endButton.gameObject.SetActive(true);
    }
}
