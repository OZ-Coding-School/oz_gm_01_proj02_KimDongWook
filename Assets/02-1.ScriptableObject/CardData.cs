using System.Collections.Generic;
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
public enum FactionType
{
    None,
    Justice,       // 장의실현부
    TeaParty,      // 티파티
    Study,         // 보충수업부
    PretectTeam,   // 선도부
    Hyakkiyako     // 백귀야행
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
    public FactionType factionType;
    public int cost;

    [Header("체력,공격력,스킬 능력치")]
    public int maxHP;
    public int attackCount; //코하루 카드, 미숙한 티파티 임원 한정 공격 -> 회복
    public int abilityCount; //일반 공격에서 특수 능력 수치, 패시브 능력 수치
    public int shieldCount; //기본적으로 다 0 -> 자신 혹은 다른 카드의 도움으로 증가

    [Header("앞면 이미지")]
    public Sprite frontImage;      //앞면 이미지
    public Sprite EquipmentImage;  //장비 카드만 이미지 하나 더 있음
    public Sprite typeIcon;        //타입 아이콘(사건,장비,필드)

    [Header("일반 공격 패턴")]
    public List<EffectData> attackEffects;

    [TextArea]
    public string abilityText; //유닛 기본 공격 설명

    [Header("패시브 발동 트리거")]
    public PassiveTrigger passiveTrigger;
    public List<EffectData> passiveEffects;

    [TextArea]
    public string passiveText; //패시브 설명


}
