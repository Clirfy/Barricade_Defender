using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image ToolTip;
    public TextMeshProUGUI ToolTipTMP;
    public string ToolTipText;

    private bool mouseOver = false;

    private void Update()
    {
        if (mouseOver)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            ToolTip.transform.position = new Vector2(mousePosition.x, Mathf.Clamp(mousePosition.y, -90f, 3f));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipTMP.text = ToolTipText;
        mouseOver = true;
        ToolTip.gameObject.SetActive(true);
        Debug.Log(gameObject.name + " OnPointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        ToolTip.gameObject.SetActive(false);
        Debug.Log(gameObject.name + " OnPointer Exit");
    }
}
