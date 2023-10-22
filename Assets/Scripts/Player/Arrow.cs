using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private readonly float lifeTime = 10f;
    private InventoryManager inventoryManager;
    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Mob"))
        {
            GameObject mob = other.transform.parent.gameObject;
            MobController mobController = mob.GetComponent<MobController>();
            Ingredient drop = mobController.TakeDamage(10f);
            if (drop != null)
            {
                //drop item
                inventoryManager.AddItem(ref drop); 
            }
            Destroy(gameObject);
            
        }
    }
}
