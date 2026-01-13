using UnityEngine;

//카드 종류
public enum CardType
{
    Striker,
    Special,
    Event,
    Equipment,
    Field
}
//공격 패턴
public enum AttackPattern
{
    None,
    FrontOnly,
    BackOnly,
    AllAttack,
    AllBack,
    RandomAttack
}
//패시브 발동 트리거
public enum PassiveTrigger
{
    None,
    OnAttack,
    OnTurnStart,
    OnTurnEnd,
    OnSummon
}


[CreateAssetMenu(menuName = "CardData")]
public class CardData : ScriptableObject
{
    [Header("공통정보")]
    public string cardName;
    public CardType cardType;
    public int cost;

    [Header("체력,공격,스킬 능력치")]
    public int maxHP;
    public int attack; //코하루 카드, 미숙한 티파티 임원 한정 공격 -> 회복
    public int ability;

    [Header("앞면 이미지")]
    public Sprite frontImage;      //앞면 이미지
    public Sprite EquipmentImage;  //장비 카드만 이미지 하나 더 있음
    public Sprite typeIcon;        //타입 아이콘(사건,장비,필드)

    [TextArea]
    public string abilityText; //스트라이커 기본 공격 능력 또는 서포트 카드 능력 설명

    [Header("공격 패턴")]
    public AttackPattern attackPattern;

    [Header("패시브 발동 트리거")]
    public PassiveTrigger passiveTrigger;
    [TextArea]
    public string passiveText; //패시브 설명


}
