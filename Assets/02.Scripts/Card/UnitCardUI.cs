using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour
{
    [Header("카드 앞면과 뒷면")]
    [SerializeField] private Image frontImage;  //카드 앞면
    [SerializeField] private Image backImage;   //카드 뒷면

    [Header("HP 오브젝트/HP 텍스트")]
    [SerializeField] private GameObject hpRoot;  //hp관련 UI 한번에 온/오프 용도
    [SerializeField] private TextMeshProUGUI hpText;

    [Header("실드 오브젝트/실드 텍스트")]
    [SerializeField] private GameObject shieldRoot;  //실드 관련 UI 한번에 온/오프 용도
    [SerializeField] private TextMeshProUGUI shieldText;

    private RectTransform rect;
    private Vector2 basePos;
    private Vector3 baseScale;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();     
    }
    //스트라이커 카드 앞면 표시
    public void ShowStriker(CardData cardData, int currentHP)
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);

        frontImage.sprite = cardData.frontImage;

        hpRoot.SetActive(true);
        hpText.text = currentHP.ToString();
    }
    //스페셜 카드 앞면 표시
    public void ShowSpecial(CardData cardData)
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);

        frontImage.sprite = cardData.frontImage;
        hpRoot.SetActive(false);
    }
    //슬롯에 카드 배치 후 기본 위치,크기 저장
    public void BaseTransform()
    {
        basePos = rect.anchoredPosition;
        baseScale = rect.localScale;
    }

    //게임 시작 시 카드(슬롯) 크기
    public void SetStartSize()
    {
        rect.localScale = baseScale * 1.15f;
        rect.anchoredPosition = basePos;
    }
    // 카드(슬롯) 크기 원상복귀
    public void SetNormalSize()
    {
        rect.localScale = baseScale;
    }
    //선택된 카드 연출
    public void SetSelectUI()
    {
        rect.localScale = baseScale * 1.15f;
    }
    //카드 HP 텍스트 실시간 업데이트 용
    public void UpdateHP(int currentHP)
    {
        hpText.text = currentHP.ToString();
    }
    //카드 shield
    public void UpdateShield(int currentShield)
    {
        shieldRoot.gameObject.SetActive(true);
        shieldText.text = currentShield.ToString();

        if (currentShield <= 0)
        {
            shieldRoot.gameObject.SetActive(false);
        }
    }
}
