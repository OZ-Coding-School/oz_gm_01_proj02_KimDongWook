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
    AllEnemy
}
//패시브 발동 트리거
public enum PassiveTrigger
{
    None,
    OnAttack,
    OnTurnStart,
    OnSummon
}


[CreateAssetMenu(menuName = "CardData")]
public class CardData : ScriptableObject
{
    [Header("공통정보")]
    public string cardName;
    public CardType cardType;
    public int cost;

    [Header("체력과 공격")]
    public int maxHP;
    public int attack;

    [TextArea]
    public string abilityText; //스트라이커 기본 공격 능력 또는 서포트 카드 능력

    [Header("공격 패턴")]
    public AttackPattern attackPattern;

    [Header("패시브 발동 트리거")]
    public PassiveTrigger passiveTrigger;
    public string passiveText;

}
