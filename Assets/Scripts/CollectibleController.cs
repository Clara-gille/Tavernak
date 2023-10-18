using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleController : MonoBehaviour
{
    public void PickUp()
    {
        Debug.Log(gameObject.name + " picked up!"); 
        Destroy(gameObject);
    } 
}
