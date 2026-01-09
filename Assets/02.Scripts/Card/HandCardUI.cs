using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandCardUI : MonoBehaviour
{
    [Header("카드 앞면과 뒷면")]
    [SerializeField] private Image frontImage;  //카드 앞면
    [SerializeField] private Image backImage;   //카드 뒷면

    [Header("장비 카드의 추가 이미지")]
    [SerializeField] private Image equipmentImage;

    [Header("코스트")]
    [SerializeField] private TextMeshProUGUI costText;

    [Header("타입 이미지")]
    [SerializeField] private Image typeIcon;

    //스크립터블 오브젝트의 UI데이터를 가져옴
    public void SetUIData(CardData cardData)
    {
        frontImage.sprite = cardData.frontImage;
        typeIcon.sprite = cardData.typeIcon;

        //if (cardData.cardType == CardType.Equipment)
        //{
        //    EquipmentImage.sprite = cardData.equipmentImage;
        //}

        if (cardData.cardType == CardType.Equipment && equipmentImage != null)
        {
            equipmentImage.sprite = cardData.EquipmentImage;
            equipmentImage.gameObject.SetActive(true);
        }
        else if (equipmentImage != null)
        {
            equipmentImage.gameObject.SetActive(false);
        }
    }

    public void ShowFront(CardData cardData)
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);


        costText.text = cardData.cost.ToString();
    }

    public void ShowBack()
    {
        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
    }
}
