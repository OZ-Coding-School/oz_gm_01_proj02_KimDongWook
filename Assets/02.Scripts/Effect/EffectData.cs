using System.Collections.Generic;
using UnityEngine;
public enum EffectType
{
    Damage,         // 데미지
    Heal,           // 회복
    DrawCard,       // 카드 드로우
    BuffAttack,     // 공격력 상승
    BuffShield,     // 방어막 생성 
    SwapFrontBack,  // 전위/후위 스왑
    DownCostCount,  // 코스트 다운
    CopyHandCard,   // 핸드 카드 복사
    SummonUnit      // 필드에 유닛 소환
}

public enum TargetSelectType
{
    FrontTarget,     // 전위 타겟
    BackTarget,      // 후위 타겟
    MaxTarget,       // 체력이 가장 많은 적

    AllTarget,       // 모든 타겟
    AllBackTarget,   // 모든 후위 타겟

    RandomTarget,    // 랜덤 타겟

    FrontMember,
    SelectMember,    // 멤버 선택
    MinMember,       // 체력이 가장 낮은 멤버
    AllBackMember,   // 후위 멤버 전부
    AllMember,       // 멤버 전부(자신 제외)
    RandomMember,    // 랜덤 멤버

    self
}
//적용 수치 기준
public enum ValueSource
{
    AttackCount,   // 공격력 기준
    AbilityCount,  // 능력 수치 기준
    ShieldCount    // 실드 수치 기준
}

[System.Serializable]
public class EffectData
{
    public EffectType effectType;
    public TargetSelectType targetSelectType;

    public ValueSource valueSource;  //데미지,회복,증가 수치
    //public int count;  //적용 대상 수, 카드 수
}
