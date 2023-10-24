using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerControllerIndoor : MonoBehaviour
{

    [SerializeField] public CanvasGroup InventoryCanvas;
    [SerializeField] public GameObject CookingCanvas;


    [Header("Camera")]
    public GameObject cam;

   
    [Header("Cooking pot")] 
    [SerializeField] private GameObject cookingPot;
    [SerializeField] private GameObject soup;
    private CookingSlot cookingSlot;

    [SerializeField] private GameObject cookingSlotObj;

    [SerializeField] private float cookingPotDistance = 3f;
    private bool isInCookingPotRange = false;


    [Header ("Inputs")]
    [SerializeField] PlayerInput playerInput;
    private bool isInCookingPot = false;

    
    
    [SerializeField] private GameObject inventoryManagerObj;
    private InventoryManager inventoryManager;
    private List<Ingredient> soupIngredients = new List<Ingredient>();

    void Start()
    {
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        cookingSlot = cookingSlotObj.GetComponent<CookingSlot>();
    }

   void Update()
    {
        //cooking need to be in the range of the cooking pot
        isInCookingPotRange = Vector3.Distance(transform.position, cookingPot.transform.position) < cookingPotDistance;
    }
   
    public void Cooking()
    {
        if (isInCookingPotRange)
        {
            isInCookingPot = true;
            CookingCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
        
    }

    public void StopCooking()
    {
        if (isInCookingPot)
        {
            isInCookingPot = false;
            CookingCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            soupIngredients = cookingSlot.Cook();
            if (soupIngredients != null)
            {
                //show the soup
                soup.SetActive(true);
            }
        }

    }

    public List<Ingredient> GetSoupIngredients()
    {
        soup.SetActive(false);
        var ingredientsReturn = new List<Ingredient>();
        ingredientsReturn.AddRange(soupIngredients);
        soupIngredients.Clear();
        return ingredientsReturn;
    }




}