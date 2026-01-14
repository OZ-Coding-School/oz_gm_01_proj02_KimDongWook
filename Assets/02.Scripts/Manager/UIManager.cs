using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;

    [SerializeField] private List<Image> costImage;
    [SerializeField] private TextMeshProUGUI costCountText;

    public Image NoButton;
    public Button okButton;
    public Button endButton;

    public Canvas Canvas { get { return canvas; } }

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                okButton.gameObject.SetActive(false);
                endButton.gameObject.SetActive(true);

                foreach (var playerUnit in FieldCardManager.Instance.PlayerUnits)
                {
                    playerUnit.UnitCardUI.SetNormalSize();
                }
            }
        }

        CostView(CostManager.Instance.CurrentCost);
    }

    //부모 오브젝트의 크기에 맞추기
    public void FullScreen(RectTransform rect)
    {
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.offsetMin = Vector2.zero; 
        rect.offsetMax = Vector2.zero;
    }
    //플레이어 코스트 UI 표시
    public void CostView(int costCount)
    {
        if (costImage == null) return;

        for (int i = 0; i < costImage.Count; i++)
        {
            if (i < costCount)
            {
                costImage[i].gameObject.SetActive(true);
            }
            else
            {
                costImage[i].gameObject.SetActive(false);
            }
        }

        costCountText.text = costCount.ToString();
    }
    //선택 확인 버튼 활성화
    public void OkButtonTrue()
    {
        NoButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
        endButton.gameObject.SetActive(false);
    }
    //턴 종료 버튼 활성화
    public void EndButtonTrue()
    {
        NoButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(true);
    }
    //적 턴일 때 버튼 클릭 못하게 하는 이미지 활성화
    public void NoButtonTrue()
    {
        NoButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(false);
    }
}
