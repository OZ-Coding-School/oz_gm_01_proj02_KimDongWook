using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour
{
    [SerializeField] private RectTransform rect;

    private UnitCardControl unitCard;

    private Vector2 basePos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        basePos = rect.anchoredPosition;
    }

    public void SetUnit(UnitCardControl unitCard)
    {
        this.unitCard = unitCard;
        unitCard.transform.SetParent(transform);
        unitCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    //전위 연출
    public void SetUnitFront()
    {
        rect.anchoredPosition = basePos + Vector2.up * 40f;
    }
    //후위 연출
    public void SetUnitBack()
    {
        rect.anchoredPosition = basePos;
    }
    //파괴된 오브젝트(카드)를 참조하게 되어 널 오류가 뜨는 것을 막음
    public void Clear()
    {
        unitCard = null;
    }
}
