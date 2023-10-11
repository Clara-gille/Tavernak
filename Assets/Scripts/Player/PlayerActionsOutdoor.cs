using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    
    private bool isPickingUp;
    private bool isAttacking;
    
    private static readonly int Swing = Animator.StringToHash("Swing");
    private static readonly int IsSwinging = Animator.StringToHash("isSwinging");
    
    // Start is called before the first frame update
    void Start()
    {
        swordAnimator = sword.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        getActionsInputs();
    }
    void FixedUpdate()
    {
        PickUpRay();
        SwordHandler();
    }

    void getActionsInputs()
    {
        isPickingUp = Input.GetButton("Fire2"); //right click
        isAttacking = Input.GetButton("Fire1"); //left click
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
                    collectible.PickUp();
                }
                
            }
        }
        else
        {
            pickUpText.gameObject.SetActive(false);
        }
    }

    private void SwordHandler()
    {
        //sword attack animation
        swordAnimator.SetBool(IsSwinging, isAttacking);
        if (isAttacking && canSwing)
        {
            StartCoroutine(SwordAttack());
        }
    }

    IEnumerator SwordAttack()
    {   
        canSwing = false;
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
        canSwing = true;
    }
}
