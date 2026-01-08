using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFieldZone : MonoBehaviour
{
    [SerializeField] private List<PlayerUnitSlot> playerUnitSlots;

    public void SummonPlayerUnit(List<UnitCardControl> playerUnit)
    {
        for (int i = 0; i < playerUnit.Count && i < playerUnitSlots.Count; i++)
        {
            playerUnitSlots[i].SetUnit(playerUnit[i]);
        }
    }
}
