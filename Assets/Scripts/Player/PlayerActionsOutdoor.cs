using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionsOutdoor : MonoBehaviour
{   
    [Header("Camera")]
    public GameObject cam;
    
    [Header("Sword")]
    [SerializeField] private GameObject sword;
    private Animator swordAnimator;
    [SerializeField] private float swordRange = 3f;
    [SerializeField] private float swordDamage = 10f;
    [SerializeField] private float swordInterval = 0.5f; //length of swing animation
    [SerializeField] private float damageDelay = 0.12f; //delay before animation reaches peak and damage is dealt
    private bool canSwing = true;
    
    [Header("Pick Up")] 
    [SerializeField] private float pickUpDistance = 3f;
    [SerializeField] private TextMeshProUGUI pickUpText;
    
    [Header ("Inputs")]
    [SerializeField] PlayerInput playerInput;
    private bool isPickingUp;
    
    [SerializeField] private GameObject inventoryManagerObj;
    private InventoryManager inventoryManager;

    private static readonly int IsSwinging = Animator.StringToHash("isSwinging");
    
    // Start is called before the first frame update
    void Start()
    {
        swordAnimator = sword.GetComponent<Animator>();
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetActionsInputs();
    }
    void FixedUpdate()
    {
        PickUpRay();
    }

    void GetActionsInputs()
    {
        isPickingUp = playerInput.actions["Pick Up"].ReadValue<float>() > 0;
    }
    
    private void PickUpRay()
    {
        //ray from center of the screen
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        //cast the ray
        if (Physics.Raycast(ray, out var hit, pickUpDistance))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Collectible"))
            {
                pickUpText.gameObject.SetActive(true);
                if (isPickingUp)
                {
                    CollectibleController collectible = other.GetComponent<CollectibleController>();
                    Ingredient drop = collectible.PickUp();
                    inventoryManager.AddItem(ref drop);
                }
                
            }
        }
        else
        {
            pickUpText.gameObject.SetActive(false);
        }
    }

    private void Attack()
    {
        if (canSwing)
        {
            StartCoroutine(SwordAttack());
        }
    }

    IEnumerator SwordAttack()
    {   
        canSwing = false;
        //sword attack animation
        swordAnimator.SetBool(IsSwinging, true);
        yield return new WaitForSeconds(damageDelay); //wait for animation to reach peak
        //ray from center of the screen
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        //cast the ray
        if (Physics.Raycast(ray, out var hit, swordRange))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Mob"))
            {
                GameObject mob = other.transform.parent.gameObject;
                MobController mobController = mob.GetComponent<MobController>();
                String drop = mobController.TakeDamage(swordDamage);
                if (drop != null)
                {
                    //drop item
                    Debug.Log("acquired " + drop); 
                }
            }
        }
        yield return new WaitForSeconds(swordInterval - damageDelay); //wait for animation to finish
        swordAnimator.SetBool(IsSwinging, false);
        canSwing = true;
    }
}
