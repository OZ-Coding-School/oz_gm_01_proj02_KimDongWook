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
    //타겟 결정

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
                return data.attackCount;

            case ValueSource.AbilityCount:
                return data.abilityCount;

            case ValueSource.ShieldCount:
                return data.shieldCount;
        }

        return 0;
    }
}
