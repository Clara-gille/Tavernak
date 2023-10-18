using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleInstantier : MonoBehaviour
{   
    [Header("Spawn")] 
    [SerializeField] private float spawnRate = 0.75f;

    [Header("Stats")] 
    [SerializeField] private String name;
    [SerializeField] private Sprite img;
    private List<Taste> stats;
    
    private Ingredient ingredient;
    // Start is called before the first frame update
    void Start() 
    {
        ingredient = new Ingredient(name, img, stats);
        if ( Random.Range(0f, 1f) > spawnRate)
        {   
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
