using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject draggingIcon;
    RectTransform draggingPlane;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Canvas canvas = HUDManager.instance.hudCanvas;

        draggingIcon = new GameObject("icon");
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling();

        CanvasGroup group = draggingIcon.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
        RectTransform rect = draggingIcon.AddComponent<RectTransform>();
        Image image = draggingIcon.AddComponent<Image>();
        Transform oriImage = transform.GetChild(0).GetChild(0);
        image.sprite = oriImage.GetComponent<Image>().sprite;
        rect.sizeDelta = new Vector2(oriImage.GetComponent<RectTransform>().rect.width, oriImage.GetComponent<RectTransform>().rect.height);

        draggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingIcon != null)
            Destroy(draggingIcon);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        RectTransform rect = draggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rect.position = globalMousePos;
            rect.rotation = draggingPlane.rotation;
        }
    }
}
