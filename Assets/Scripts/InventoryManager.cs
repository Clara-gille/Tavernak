using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        order.Add("Egg", 3);
        order.Add("Apple", 4);
    }

    // Start is called before the first frame update
    public void AddItem(ref Ingredient ingredient)          //let's see if any slot has the same item and then add it
    {
        WriteInventory(ingredient);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.ingredient.name == ingredient.name)
            {
                itemInSlot.count++;
                itemInSlot.countText.text = itemInSlot.count.ToString();
                return;
            }
        }
        
        for (int i = 0; i < inventorySlots.Length; i++)             //let's see if the slot is empty, if yes add
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                GameObject itemToAdd = Instantiate(itemsPrefabs[order[ingredient.name]], slot.transform);
                itemToAdd.transform.SetParent(slot.transform);
                return;
            }
        }
    }
    private void WriteInventory(Ingredient ingredient)
    {
        string path = "Assets/Scripts/Player/PlayerInventory.txt";
        StreamWriter writer = new StreamWriter(path, true);
        string stats = "";

        foreach (Taste stat in ingredient.Stats)
        {
            stats += stat.Name + " " + stat.Value.ToString() + " ";
        }

        writer.WriteLine(ingredient.name + " " + stats +"\t");
        writer.Close();
    }
}
