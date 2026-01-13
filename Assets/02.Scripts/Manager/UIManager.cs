using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;

    [SerializeField] private List<Image> costImage;

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
            }
        }
    }

    //부모 오브젝트의 크기에 맞추기
    public void FullScreen(RectTransform rect)
    {
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.offsetMin = Vector2.zero; 
        rect.offsetMax = Vector2.zero;
    }
    //플레이어 코스 UI 표시
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
    }
}
