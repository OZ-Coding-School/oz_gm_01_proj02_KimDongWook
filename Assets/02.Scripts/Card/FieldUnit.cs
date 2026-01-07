using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType
{
    Player, Enemy
}
public enum LinePosition
{
    Front, Back
}


public class FieldUnit : MonoBehaviour
{
    public CardData cardData;
    
    public TeamType teamType;
    public LinePosition linePosition;

    public int currentHP;

    public void isFieldUnit(CardData data, TeamType type, LinePosition position)
    {
        cardData = data;
        teamType = type;
        linePosition = position;
        currentHP = data.maxHP;
    }

    //전열과 후열 교체 메서드
    public void SwapLine()
    {
        linePosition = (linePosition == LinePosition.Front) ? LinePosition.Back : LinePosition.Front;

        //BattleManager.Instantiate.
    }
}
