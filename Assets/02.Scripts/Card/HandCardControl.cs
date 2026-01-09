using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCardControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("카드 데이터")]
    [SerializeField] CardData cardData;

    [Header("카드 UI")]
    [SerializeField] HandCardUI handCardUI;

    //RectTransform : UI전용 위치정보 transform
    private RectTransform rectTransform;

    //여러 UI요소들을 한번에 제어한다.(투명도, 상호작용 가능여부, 레이캐스트 차단 등)
    private CanvasGroup canvasGroup;

    //드래그 시작 전에 위치했던 부모(패 존)
    private Transform originParent;

    //드래그 후 카드가 있던 원래 위치
    private Vector2 originPosition;

    public CardData CardData
    {
        get { return cardData; }
        set { cardData = value; }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    //매개변수로 받는 데이터를 카드 프리팹의 데이터로 전환
    public void IsCardData(CardData cardData)
    {
        this.cardData = cardData;
        handCardUI.SetUIData(cardData);
        ShowToBack();
    }
    //핸드카드 앞면 표시
    public void ShowToFront()
    {
        handCardUI.ShowFront(cardData);
    }
    //핸드카드 뒷면 표시
    public void ShowToBack()
    {
        handCardUI.ShowBack();
    }
    //터치, 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent = transform.parent; //현재 부모(패) 저장

        originPosition = rectTransform.anchoredPosition; //현재 위치 저장, anchoredPosition = UI의 Position

        //드래그 중에는 카드가 클릭을 막지 않도록
        //blocksRaycasts = 클릭(터치)의 레이캐스트 이벤트를 받을지 결정
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(UIManager.Instance.Canvas.transform, true);
        transform.SetAsLastSibling();
    }
    //드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        //마우스(터치) 이동만큼 카드 이동
        rectTransform.anchoredPosition += eventData.delta / UIManager.Instance.Canvas.scaleFactor;
    }
    //드롭, 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        //만약 필드 존에 드롭하지 않는다면 -> 원래 위치로(패)
        //if (transform.parent == originParent)
        //{
        //    rectTransform.anchoredPosition = originPosition;
        //}

        //드래그가 끝났을 때 포인터 아래에 있는 오브젝트 확인
        GameObject dropPos = eventData.pointerEnter;

        if (dropPos == null)
        {
            ReturnToHand();
            return;
        }

        FieldCardSlot fieldSlot = dropPos.GetComponentInParent<FieldCardSlot>();
        if (fieldSlot != null && CardData.cardType == CardType.Field)
        {
            fieldSlot.SetFieldCard(this);
            return;
        }

        EventOrEquipmentDrop(dropPos);
    }
    //사건 카드, 장비 카드 사용 시
    private void EventOrEquipmentDrop(GameObject dropPos)
    {
        HandZone handZone = dropPos.GetComponentInParent<HandZone>();

        if (handZone == null)
        {
            if (CardData.cardType == CardType.Event || CardData.cardType == CardType.Equipment)
            {
                //여기에 카드 이벤트 발동

                //사용한 사건,장비 카드는 묘지로
                GraveyardZone.Instance.GoToGrave(this);
                GraveyardZone.Instance.GraveyardView();
            }
            return;
        }

        if (handZone != null)
        {
            ReturnToHand();
            return;
        }
    }

    private void ReturnToHand()
    {
        transform.SetParent(originParent);
        rectTransform.anchoredPosition = originPosition;
    }
}
