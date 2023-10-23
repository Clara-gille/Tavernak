using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
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
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Debug.Log(itemInSlot.ingredient.name + " and " + ingredient.name + " are stacked.");
            }
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
                Debug.Log("Slot is empty. Adding ingredient.");
                GameObject itemToAdd = Instantiate(itemsPrefabs[order[ingredient.name]], slot.transform);
                itemToAdd.transform.SetParent(slot.transform);
                return;
            }
        }
    }

    // CREATE A FUNCTION TO ADD ALL ITEMS FROM COUISINE INVENTORY
    public void WriteInventory(ref Ingredient ingredient)
    {
        string path = "Assets/Scripts/Player/PlayerInventory.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(ingredient.name + "\t");
        writer.Close();
    }

    public void PassInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)             // empty physical inventory
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            Debug.Log(itemInSlot);
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);
            }
        }
        int pr = 0;
        for (int i = 0; i < inventorySlots.Length; i++)             // empty physical inventory
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                pr++;
            }
        }
        Debug.Log(pr);

        string path = "Assets/Scripts/Player/PlayerInventory.txt";
        StreamReader reader = new StreamReader(path);
        while (reader.ReadLine() != null) {
        string ingredient = reader.ReadLine();
            switch (ingredient)
            {
                case string a when a.Contains("Strawberry"):
                    string sname = "Strawberry";
                    List<int> sstats = new List<int>() { 0, 2, 3, 0, 0 };

                    Ingredient strawberry;
                    strawberry = ScriptableObject.CreateInstance<Ingredient>();
                    strawberry.name = sname;
                    strawberry.Stats = sstats;
                    AddItem(ref strawberry);
                    break;
                case string a when a.Contains("Apple"):
                    string aname = "Apple";
                    List<int> astats = new List<int>() { 2, 0, 0, 1, 1 };

                    Ingredient apple;
                    apple = ScriptableObject.CreateInstance<Ingredient>();
                    apple.name = aname;
                    apple.Stats = astats;
                    AddItem(ref apple);
                    break;
                case string a when a.Contains("Mushroom"):
                    string muname = "Mushroom";
                    List<int> mustats = new List<int>() { 0, 0, 2, 0, 2 };

                    Ingredient mushroom;
                    mushroom = ScriptableObject.CreateInstance<Ingredient>();
                    mushroom.name = muname;
                    mushroom.Stats = mustats;
                    AddItem(ref mushroom);
                    break;
                case string a when a.Contains("Egg"):
                    string ename = "Egg";
                    List<int> estats = new List<int>() { 2, 0, 0, 3, 3 };

                    Ingredient egg;
                    egg = ScriptableObject.CreateInstance<Ingredient>();
                    egg.name = ename;
                    egg.Stats = estats;
                    AddItem(ref egg);
                    break;
                case string a when a.Contains("Meat"):
                    string mename = "Meat";
                    List<int> mestats = new List<int>() { 3, 0, 0, 0, 1 };

                    Ingredient meat;
                    meat = ScriptableObject.CreateInstance<Ingredient>();
                    meat.name = mename;
                    meat.Stats = mestats;
                    AddItem(ref meat);
                    break;
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            Debug.Log(itemInSlot);
        }
    }
}
