using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private PlayerFieldZone playerFieldZone;
    //[SerializeField] private List<UnitCardControl> unitCards;

    void Start()
    {
        //유닛 배치
        //playerFieldZone.SummonPlayerUnit(unitCards);
        FieldCardManager.Instance.CreatUnitCard();

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
    }

    //PlayerTurn

    //EnemyTurn

    //게임종료
}
