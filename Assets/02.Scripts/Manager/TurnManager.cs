using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public event Action OnPlayerTurnStart;
    public event Action OnPlayerTurnEnd;

    private bool isPlayerTurn = true;
    private bool isEnemyTurn;

    public bool IsPlayerTurn
    {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }
    public bool IsEnemyTrun
    {
        get { return isEnemyTurn; }
        set { isEnemyTurn = value; }
    }


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
    public void StartPlayerTurn()
    {
        Debug.Log("플레이어 턴 시작");
        isPlayerTurn = true;
        isEnemyTurn = false;

        //?.Invoke() = 델리게이트나 이벤트가 null이 아닐때만 실행해라는 안전한 호출 방식
        OnPlayerTurnStart?.Invoke();

        UIManager.Instance.EndButtonTrue();
    }
    public void EndPlayerTurn()
    {
        Debug.Log("플레이어 턴 종료");
        isPlayerTurn = false;
        isEnemyTurn = true;

        OnPlayerTurnEnd?.Invoke();
    }
    //턴 종료 버튼
    public void OnClickEndTurn()
    {
        if (!isPlayerTurn) return;
        if (UnitSelectManager.Instance.FirstCheckFront) return;

        EndPlayerTurn();
        UIManager.Instance.NoButtonTrue();
    }

}
