using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    public static CostManager Instance { get; private set; }

    public int CurrentCost { get; private set; }
    public int MaxCost { get; private set; }

    private int playerTurnCount = 0;

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

    private void Start()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnPlayerTurnStart += OnPlayerTuenStart;
        }
    }
    private void OnDestroy()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnPlayerTurnStart -= OnPlayerTuenStart;
        }
    }
    private void OnPlayerTuenStart()
    {
        if (!TurnManager.Instance.IsPlayerTurn) return;

        playerTurnCount++;

        MaxCost = playerTurnCount + 2;
        CurrentCost = MaxCost;

        Debug.Log($"플레이어 {playerTurnCount}턴 / 코스트 {CurrentCost}");
    }
    //코스트를 사용할 수 있는지 확인
    public bool CheckUseCost(int useCost)
    {
        return CurrentCost >= useCost;
    }
    //실제 코스트 사용
    public bool UseCost(int cost)
    {
        if (CurrentCost < cost)
        {
            return false;
        }

        CurrentCost -= cost;
        return true;
    }
}
