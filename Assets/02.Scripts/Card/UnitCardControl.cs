using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//플레이어와 적 구별
public enum UnitOwner
{
    Player,
    Enemy
}
//유닛의 포지션이 전열인지 후열인지
public enum UnitPosition
{
    None,
    Front,
    Back
}

public class UnitCardControl : MonoBehaviour, IPointerClickHandler
{
    [Header("카드 데이터")]
    [SerializeField] private CardData cardData;

    [Header("카드 UI")]
    [SerializeField] private UnitCardUI unitCardUI;

    [Header("플레이어와 적 구별")]
    [SerializeField] private UnitOwner unitOwner;

    [Header("유닛의 전열/후열 구별")]
    [SerializeField] private UnitPosition unitPosition;

    private int currentHP;      //현재 hp
    private int currentShield;  //현재 실드

    private UnitSlot currentSlot;

    public bool IsSelect { get; private set; }

    public bool IsPlayer => unitOwner == UnitOwner.Player;
    public bool IsEnemy => unitOwner == UnitOwner.Enemy;
    public bool IsStriker => cardData.cardType == CardType.Striker;
    public bool IsSpecial => cardData.cardType == CardType.Special;
    public bool IsFront => unitPosition == UnitPosition.Front;
    public bool IsBack => unitPosition == UnitPosition.Back;
    public bool IsDead => IsStriker && currentHP <= 0;
    public UnitPosition UnitPosition => unitPosition;
    public UnitCardUI UnitCardUI => unitCardUI;
    public CardData CardData => cardData;

    //매개변수로 받는 데이터를 카드 프리팹의 데이터로 전환
    public void SetCardData(CardData data, UnitOwner owner)
    {
        cardData = data;
        unitOwner = owner;
        unitPosition = UnitPosition.None;

        if (IsStriker)
        {
            currentHP = cardData.maxHP;
            unitCardUI.ShowStriker(cardData, currentHP);
        }
        else if (IsSpecial)
        {
            unitCardUI.ShowSpecial(cardData);
        }
    }
    //매개변수로 받은 슬롯을 현재 카드가 위치한 슬롯으로 지정
    public void SetSlot(UnitSlot slot)
    {
        currentSlot = slot;
    }
    //카드 전위/후위 변경
    public void SetUnitPosition(UnitPosition newPos)
    {
        unitPosition = newPos;
    }
    //카드 선택
    public void OnSelect()
    {
        IsSelect = true;
        unitCardUI.SetSelectUI();
    }
    //카드 선택 해제
    public void OnDeselect()
    {
        IsSelect = false;
        unitCardUI.SetNormalSize();
    }
    //카드 전위 UI 연출
    public void UnitFront()
    {
        unitPosition = UnitPosition.Front;
        unitCardUI.SetNormalSize();
        currentSlot.SetUnitFront();
    }
    //카드 후위 UI 연출
    public void UnitBack()
    {
        unitPosition = UnitPosition.Back;
        unitCardUI.SetNormalSize();
        currentSlot.SetUnitBack();
    }
    //해당 카드가 적에게 데미지를 받는 기능
    public void TakeDamage(int damage)
    {
        if (!IsStriker) return;

        //만약 실드가 있다면 데미지를 실드가 대신 받음
        if (currentShield > 0)
        {
            currentShield -= damage;
            unitCardUI.UpdateHP(currentShield);

            //받은 데미지가 실드보다 높으면 그 만큼의 데미지를 HP에서 차감
            if (currentShield < 0)
            {
                currentHP -= currentShield;
                unitCardUI.UpdateHP(currentHP);
            }
        }
        else
        {
            currentHP -= damage;
            unitCardUI.UpdateHP(currentHP);
        }

        if (currentHP <= 0)
        {
            //유닛의 hp가 0이 되면
            Die();
        }
    }
    //유닛 사망
    private void Die()
    {
        if (currentSlot != null)
        {
            currentSlot.Clear();
        }

        Destroy(gameObject);
    }
    //해당 카드가 회복받을 때
    public void Heal(int healCount)
    {
        if (!IsStriker) return;

        currentHP += healCount;
        currentHP = Mathf.Min(currentHP, cardData.maxHP);

        unitCardUI.UpdateHP(currentHP);
    }
    //공격력 증가 버프를 받았을 때
    public void AddAttackCount(int addCount)
    {
        cardData.attackCount += addCount;
    }
    //실드를 받았을 때
    public void AddShieldCount(int addCount)
    {
        currentShield += addCount;
        unitCardUI.UpdateShield(currentShield);
    }
    //플레이어 유닛 선택 터치
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsPlayer) return;

        UnitSelectManager.Instance.SelectUnit(this);
    }
    //유닛 카드의 일반 공격
    public void UnitDoAttack()
    {
        Debug.Log($"[{cardData.cardName}] 공격 발동");
        Debug.Log($"설명: {cardData.abilityText}");

        //나중에 공격 처리 입력할 거임

    }
}
