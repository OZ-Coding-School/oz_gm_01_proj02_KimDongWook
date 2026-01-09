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

    public void SetUIData(CardData cardData)
    {
        frontImage.sprite = cardData.frontImage;

        if (cardData.cardType == CardType.Striker && hpRoot != null)
        {
            hpRoot.SetActive(true);
            hpText.text = cardData.maxHP.ToString();
        }
        else if (hpRoot != null)
        {
            hpRoot.SetActive(false);
        }
    }

    public void ShowStriker(CardData cardData, int currentHP)
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);

        frontImage.sprite = cardData.frontImage;

        hpRoot.SetActive(true);
        hpText.text = currentHP.ToString();
    }

    public void ShowSpecial(CardData cardData)
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);

        frontImage.sprite = cardData.frontImage;
        hpRoot.SetActive(false);
    }

    public void UpdateHP(int currentHP)
    {
        hpText.text = currentHP.ToString();
    }
}
