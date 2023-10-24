using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingSlot : MonoBehaviour, IDropHandler
{

    //list of items that can be cooked
    private List<Ingredient> itemsToCook = new List<Ingredient>();

    private int listMax = 3;


    public void OnDrop(PointerEventData eventData)
    {
        if (itemsToCook.Count < listMax){
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

            AddItem(inventoryItem.ingredient);
            WriteInventory(inventoryItem.ingredient);

            if(inventoryItem.count != 1){
                inventoryItem.count--;
                inventoryItem.countText.text = inventoryItem.count.ToString();
            }
            else{
                Destroy(inventoryItem.gameObject);
            }  
        }
        else{
            Debug.Log("List is full");
        }
       
    }

    //put an item in the list
    public void AddItem(Ingredient item)
    {
        itemsToCook.Add(item);
    }


    //remove all the items from the list
    public void RemoveAllItems()
    {
        itemsToCook.Clear();
    }


    public List<Ingredient> Cook()
    {
        if (itemsToCook.Count !=0){
            var itemReturn = new List<Ingredient>();
            itemReturn.AddRange(itemsToCook); // Copie les éléments de itemsToCook dans itemReturn
            RemoveAllItems();

            return itemReturn;
        }

        return null;
    }
    private void WriteInventory(Ingredient ingredient)
    {
        string path = "Assets/Scripts/Player/PlayerInventory.txt";
        StreamReader reader = new StreamReader(path);

        string line;
        string temp = "";
        bool removed = false;

        while ((line = reader.ReadLine()) != null)
        {
            if ((line.Contains(ingredient.name)) && (removed == false))
            {
                removed = true;
            }
            else
            {
                temp += line + "\n";
            }
        }
        reader.Close();
        File.WriteAllText(path, temp);
    }
}