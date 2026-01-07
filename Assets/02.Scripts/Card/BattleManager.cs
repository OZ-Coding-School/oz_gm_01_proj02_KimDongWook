using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public void Attack(FieldUnit attacker, FieldUnit target)
    {
        if (attacker.linePosition != LinePosition.Front) return;

        int damage = attacker.cardData.attack;
        
    }
}
