using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] inventorySlots;
    [Header ("Items prefabs")]
    [SerializeField] GameObject[] itemsPrefabs; //prefabs of items to add to inventory

    private Dictionary<string, int> order = new();

    private void Start()
    {
        order.Add("Mushroom", 0);
        order.Add("Strawberry", 1);
        order.Add("Meat", 2);
        order.Add("Apple", 3);
        order.Add("Egg", 4);
    }

    // Start is called before the first frame update
    public void AddItem(ref Ingredient ingredient)          //let's see if any slot has the same item and then add it
    {
        Debug.Log("Add" + ingredient.name + "to inventory");

        InventorySlot slotToUse = inventorySlots[3];
        GameObject itemToAdd = Instantiate(itemsPrefabs[order[ingredient.name]], slotToUse.transform);
        itemToAdd.transform.SetParent(slotToUse.transform);
        
        
        // for (int i = 0; i < inventorySlots.Length; i++)
        // {
        //     InventorySlot slot = inventorySlots[i];
        //     InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        //     if (itemInSlot != null && itemInSlot.ingredient == ingredient)
        //     {
        //         itemInSlot.count++;
        //         itemInSlot.countText.text = itemInSlot.count.ToString();
        //         return true;
        //     }
        // }
        //
        // for (int i = 0; i < inventorySlots.Length; i++)             //let's see if the slot is empty, if yes add
        // {
        //     InventorySlot slot = inventorySlots[i];
        //     InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        //     if (itemInSlot == null)
        //     {
        //         return true;
        //     }
        // }
        // return false;
    }

}
