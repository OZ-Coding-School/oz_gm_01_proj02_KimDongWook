using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCardControl : MonoBehaviour
{
    [Header("카드 데이터")]
    [SerializeField] private CardData cardData;

    [Header("카드 UI")]
    [SerializeField] private UnitCardUI unitCardUI;

    private int currentHP;

    public CardData CardData => cardData;
    public bool IsStriker
    {
        get
        {
            return cardData.cardType == CardType.Striker;
        }
    }
    public bool isSpecial
    {
        get
        {
            return cardData.cardType == CardType.Special;
        }
    }

    private void Awake()
    {
        if (IsStriker)
        {
            currentHP = cardData.maxHP;
            unitCardUI.ShowStriker(CardData, currentHP);
        }
        else if (isSpecial)
        {
            unitCardUI.ShowSpecial(CardData);
        }
    }

    //나중에 쓰일 데미지 메서드
    public void TakeDamage(int damage)
    {
        if (!IsStriker) return;

        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        unitCardUI.UpdateHP(currentHP);

        if (currentHP <= 0)
        {
            //유닛의 hp가 0이 되면
        }
    }
}
