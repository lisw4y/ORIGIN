using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject item = Instantiate(eventData.pointerDrag.transform.GetChild(0).gameObject);
            item.transform.localScale /= 2;
            item.transform.SetParent(transform);
            //Component[] components = eventData.pointerDrag.gameObject.GetComponents<Component>();
            //foreach (Component component in components)
            //{
            //    UnityEditorInternal.ComponentUtility.CopyComponent(component);
            //    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(gameObject);
            //}
        }
    }
}
