using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitSlot : MonoBehaviour
{
    private UnitCardControl playerUnit;

    public void SetUnit(UnitCardControl playerUnit)
    {
        this.playerUnit = playerUnit;
        playerUnit.transform.SetParent(transform);
        playerUnit.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
