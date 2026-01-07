using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("카드 데이터")]
    [SerializeField] CardData cardData;

    [Header("카드 앞면과 뒷면")]
    [SerializeField] private Image frontImage;  //카드 앞면
    [SerializeField] private Image backImage;   //카드 뒷면

    //RectTransform : UI전용 위치정보 transform
    private RectTransform rectTransform;

    [SerializeField] private Canvas canvas;

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

        canvas = GetComponentInParent<Canvas>();

        ShowBack();
    }

    public void ShowFront()
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);
    }
    public void ShowBack()
    {
        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
    }
    //터치, 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent = transform.parent; //현재 부모(패) 저장

        originPosition = rectTransform.anchoredPosition; //현재 위치 저장, anchoredPosition = UI의 Position

        //드래그 중에는 카드가 클릭을 막지 않도록
        //blocksRaycasts = 클릭(터치)의 레이캐스트 이벤트를 받을지 결정
        canvasGroup.blocksRaycasts = false;

        //transform.SetAsLastSibling();
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
        if (transform.parent == originParent)
        {
            rectTransform.anchoredPosition = originPosition;
        }
    }
}
