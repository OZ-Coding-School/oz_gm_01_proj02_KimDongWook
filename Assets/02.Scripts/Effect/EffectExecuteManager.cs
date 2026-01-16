using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecuteManager : MonoBehaviour
{
    public static EffectExecuteManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //외부에서 호출하여 능력 사용(실제 능력 사용 메서드)
    public void ExecuteEffect(UnitCardControl caster, List<EffectData> effects)
    {
        if (caster == null || effects == null) return;

        foreach (var effect in effects)
        {
            //능력의 받을 대상 선정
            List<UnitCardControl> targets = TargetDecision(caster, effect);

            foreach (var target in targets)
            {
                //선정 된 대상에게 능력 발동
                ApplyEffect(caster, target, effect);
            }
        }
    }
    //타겟 결정
    private List<UnitCardControl> TargetDecision(UnitCardControl caster, EffectData effect)
    {
        bool isPlayer = caster.IsPlayer;

        List<UnitCardControl> targetCard = new List<UnitCardControl>();

        List<UnitCardControl> memberUnits = new List<UnitCardControl>();
        List<UnitCardControl> otherUnits = new List<UnitCardControl>();

        //카드의 아군과 상대방을 구별
        if (isPlayer)
        {
            memberUnits = FieldCardManager.Instance.PlayerUnits;
            otherUnits = FieldCardManager.Instance.EnemyUnits;
        }
        else
        {
           memberUnits = FieldCardManager.Instance.EnemyUnits;
           otherUnits = FieldCardManager.Instance.PlayerUnits;
        }
    
        switch (effect.targetSelectType)
        {
            case TargetSelectType.self:
                targetCard.Add(caster);
                break;
            case TargetSelectType.FrontTarget:
                foreach (var otherUnit in otherUnits)
                {
                    if (otherUnit.IsFront && !otherUnit.IsDead) 
                        targetCard.Add(otherUnit);
                }
                break;
            case TargetSelectType.BackTarget:
                foreach(var otherUnit in otherUnits)
                {
                    if (otherUnit == UnitSelectManager.Instance.SelectCard)
                    {
                        if (otherUnit.IsBack && !otherUnit.IsDead) 
                            targetCard.Add(otherUnit);
                    }
                }
                break;
            case TargetSelectType.AllTarget:
                foreach (var otherUnit in otherUnits)
                {
                    if (!otherUnit.IsDead)
                        targetCard.Add(otherUnit);
                }
                break;
            case TargetSelectType.AllBackTarget:
                foreach (var otherUnit in otherUnits)
                {
                    if (otherUnit.IsBack &&  !otherUnit.IsDead)
                        targetCard.Add(otherUnit);
                }
                break;
            case TargetSelectType.FrontMember:
                foreach (var memberUnit in memberUnits)
                {
                    if (memberUnit.IsFront && !memberUnit.IsDead)
                        targetCard.Add(memberUnit);
                }
                    break;
            case TargetSelectType.AllMember:
                foreach (var memberUnit in memberUnits)
                {
                    if (!memberUnit.IsDead)
                        targetCard.Add(memberUnit);
                }
                break;
            case TargetSelectType.AllBackMember:
                foreach (var memberUnit in memberUnits)
                {
                    if (memberUnit.IsBack && !memberUnit.IsDead)
                        targetCard.Add(memberUnit);
                }
                break;
        }
        return targetCard;
    }


    //대상에게 실제 효과 적용
    private void ApplyEffect(UnitCardControl caster, UnitCardControl target, EffectData effect)
    {
        int count = EffectValue(caster, effect);

        switch (effect.effectType)
        {
            case EffectType.Damage:
                target.TakeDamage(count);
                break;
            case EffectType.Heal:
                target.Heal(count);
                break;
            case EffectType.BuffAttack:
                target.AddAttackCount(count);
                break;
            case EffectType.BuffShield:
                target.AddShieldCount(count);
                break;
            case EffectType.DrawCard:
                for (int i = 0; i < count; i++)
                {
                    DeckManager.Instance.DrawCard();
                }
                break;
        }
    }
    //일반공격/패시브 발동 시 적용할 수치 기준
    private int EffectValue(UnitCardControl caster, EffectData effect)
    {
        CardData data = caster.CardData;

        switch (effect.valueSource)
        {
            case ValueSource.AttackCount:
                return caster.CurrentAttack;

            case ValueSource.AbilityCount:
                return data.abilityCount;

            case ValueSource.ShieldCount:
                return data.shieldCount;
        }

        return 0;
    }
}
