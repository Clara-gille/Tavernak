using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] inventorySlots;
    [Header ("Items prefabs")]
    [SerializeField] GameObject[] itemsPrefabs; //prefabs of items to add to inventory

    [Header ("Ingredients")]
    [SerializeField] GameObject[] strawberrys;
    [SerializeField] GameObject[] mushrooms;
    [SerializeField] GameObject[] apples;
    [SerializeField] GameObject[] eggs;

    [Header("Player")]
    [SerializeField] GameObject player;

    private Dictionary<string, int> order = new();
    private void Start()
    {
        order.Add("Mushroom", 0);
        order.Add("Strawberry", 1);
        order.Add("Meat", 2);
        order.Add("Egg", 3);
        order.Add("Apple", 4);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SpawnInventory();
        }
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
        string path = Path.Combine(Application.streamingAssetsPath, "PlayerInventory.txt");
        StreamWriter writer = new StreamWriter(path, true);
        string stats = "";

        foreach (Taste stat in ingredient.Stats)
        {
            stats += stat.Name + " " + stat.Value.ToString() + " ";
        }

        writer.WriteLine(ingredient.name + " " + stats +"\t");
        writer.Close();
    }

    private void SpawnInventory()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "PlayerInventory.txt");
        StreamReader reader = new StreamReader(path);

        int s_index = 1, m_index = 1, e_index = 1, a_index = 1;

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            Debug.Log(line);
            switch (line)
            {
                case string a when a.Contains("Strawberry"):
                    if (strawberrys[s_index].gameObject.activeSelf != false)
                    {
                        Debug.Log("strawberry");
                        strawberrys[s_index].gameObject.transform.position = player.gameObject.transform.position;
                    }
                    s_index++;
                    break;
                case string a when a.Contains("Mushroom"):
                    if (mushrooms[m_index].gameObject.activeSelf != false)
                    {
                        Debug.Log("mushrooms");
                        mushrooms[m_index].gameObject.transform.position = player.gameObject.transform.position;
                    }
                    m_index++;
                    break;
                case string a when a.Contains("Egg"):
                    if (eggs[e_index].gameObject.activeSelf != false)
                    {
                        Debug.Log("eggs");
                        eggs[e_index].gameObject.transform.position = player.gameObject.transform.position;
                    }
                    e_index++;
                    break;
                case string a when a.Contains("Apple"):
                    if (apples[a_index].gameObject.activeSelf != false)
                    {
                        Debug.Log("apples");
                        apples[a_index].gameObject.transform.position = player.gameObject.transform.position;
                    }
                    a_index++;
                    break;
            }
        }
        reader.Close();
        File.WriteAllText(path, string.Empty);
    }
}