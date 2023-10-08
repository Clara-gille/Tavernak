using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PickUp()
    {
        Debug.Log(gameObject.name + " picked up!");
        Destroy(gameObject);
    } 
}
