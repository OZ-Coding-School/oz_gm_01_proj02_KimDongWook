using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObserverTest : MonoBehaviour
{
    private void OnEnable()
    {
        TurnManager.Instance.OnTurnStart += OnTurnStart;
        TurnManager.Instance.OnTurnEnd += OnTurnEnd;
    }

    private void OnDisable()
    {
        TurnManager.Instance.OnTurnStart -= OnTurnStart;
        TurnManager.Instance.OnTurnEnd -= OnTurnEnd;
    }

    private void OnTurnStart()
    {
        Debug.Log("옵저버 : 턴 시작 감지");
    }
    private void OnTurnEnd()
    {
        Debug.Log("옵저버 : 턴 종료 감지");
    }
}
