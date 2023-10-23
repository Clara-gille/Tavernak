using System.Collections;
using System.Collections.Generic;
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

}
