using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [Header("Spawn")] [SerializeField] 
    private float spawnRate = 0.75f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if ( Random.Range(0f, 1f) > spawnRate)
        {   
            gameObject.SetActive(false);
        }
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
