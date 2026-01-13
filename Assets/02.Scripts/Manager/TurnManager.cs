using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public event Action OnTurnStart;
    public event Action OnTurnEnd;

    private bool isPlayerTurn = true;
    private bool isEnemyTurn;

    public bool IsPlayerTrun
    {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }
    public bool IsEnemyTrun
    {
        get { return isEnemyTurn; }
        set { isEnemyTurn = value; }
    }

    public bool IsPlayerTurn => isPlayerTurn;

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
    //턴 종료 버튼
    public void OnClickEndTurn()
    {
        if (!isPlayerTurn) return;

        EndPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;

        Debug.Log("플레이어 턴 시작");
        //?.Invoke() = 델리게이트나 이벤트가 null이 아닐때만 실행해라는 안전한 호출 방식
        OnTurnStart?.Invoke(); 
    }
    public void EndPlayerTurn()
    {
        Debug.Log("플레이어 턴 종료");
        OnTurnEnd?.Invoke();

        isPlayerTurn = false;

        StartPlayerTurn();
    }
}
