using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Inventory : MonoBehaviour
{
    [SerializeField] public CanvasGroup InventoryCanvas;
    [SerializeField] private bool inventoryOpened = false;


    private void Start()
    {
        InventoryCanvas = GetComponent<CanvasGroup>();
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { // ouverture de l'inventaire en appuyant sur E
            inventoryOpened = !inventoryOpened;
        }
        if (!inventoryOpened)
        {
            InventoryCanvas.alpha = 0.0f;       //on rend "invisible" le Canvas Group en mettant l'opacité à 0
        }
        else
        {
            InventoryCanvas.alpha = 1f;         //on affiche le Canvas Group
        }
    }
}
