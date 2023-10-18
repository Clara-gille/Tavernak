using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)                  //if item dropped on the slot
    {
        Debug.Log("drop0");
        if(transform.childCount == 0)
        {
            Debug.Log("drop");
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();              //if not occupied
            inventoryItem.parentAfterDrag = transform;                                                      //drop Item
        }
    }

}
