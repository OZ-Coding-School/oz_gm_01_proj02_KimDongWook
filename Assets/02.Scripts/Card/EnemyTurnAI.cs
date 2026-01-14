using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnAI : MonoBehaviour
{
    private void OnEnable()
    {
        TurnManager.Instance.OnPlayerTurnEnd += OnPlayerTurnEnd;
    }
    private void OnDisable()
    {
        TurnManager.Instance.OnPlayerTurnEnd -= OnPlayerTurnEnd;
    }

    private void OnPlayerTurnEnd()
    {
        StartCoroutine(EnemyTurnRoutine());
    }
    IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("적 턴 시작");

        UIManager.Instance.NoButtonTrue();

        yield return new WaitForSeconds(0.5f);
        EnemyAction();
        yield return new WaitForSeconds(0.5f);

        Debug.Log("적 턴 종료");

        TurnManager.Instance.IsEnemyTrun = false;
        TurnManager.Instance.StartPlayerTurn();
    }
    private void EnemyAction()
    {
        UnitCardControl frontEnemy = FieldCardManager.Instance.FrontEnemyCheck();

        if (frontEnemy == null) return;

        Debug.Log($"적 유닛 [{frontEnemy.name}] 이(가) 일반 공격을 사용");

        frontEnemy.UnitDoAttack();
    }
}
