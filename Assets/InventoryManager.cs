using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public bool AddItem(Item item)          //let's see if any slot has the same item and then add it
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                itemInSlot.count++;
                itemInSlot.countText.text = itemInSlot.count.ToString();
                return true;
            }
        }
        
        for (int i = 0; i < inventorySlots.Length; i++)             //let's see if the slot is empty, if yes add
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                return true;
            }
        }
        return false;
    }

}
