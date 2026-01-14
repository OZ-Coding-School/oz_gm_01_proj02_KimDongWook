using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        //유닛 배치
        //FieldCardManager.Instance.CreatUnitCard();
        FieldCardManager.Instance.StartAllUnitSummon();

        //덱 초기화
        DeckManager.Instance.CreatDeck();

        //시작 패 드로우
        for (int i = 0; i < 4; i++)
        {
            DeckManager.Instance.DrawCard();
        }

        DeckManager.Instance.DrawCard();

        //묘지 수 표시
        GraveyardZone.Instance.GraveyardView();

        //모든 사전 준비 끝 -> 플레이어 턴 시작
        TurnManager.Instance.StartPlayerTurn();
    }

    //PlayerTurn

    //EnemyTurn

    //게임종료
}
