using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)                  //if item dropped on the slot
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();              //if not occupied
        Debug.Log(inventoryItem.name);
    }
}
