using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleController : MonoBehaviour
{
    [Header("Spawn")] 
    [SerializeField] private float spawnRate = 0.75f;

    [Header("Stats")] 
    [SerializeField] private String name;
    [SerializeField] private Sprite img;
    
    private Ingredient ingredient;
    // Start is called before the first frame update
    void Start() 
    {
        ingredient = ScriptableObject.CreateInstance<Ingredient>();
        ingredient.name = name;
        List<Taste> stats = new List<Taste>();
        switch (name)
        {
            case "Mushroom":
                stats.Add(new Taste("Salty", 2));
                stats.Add(new Taste("Creamy", 1));
                stats.Add(new Taste("Crunchy", 1));
                break;
            case "Strawberry":
                stats.Add(new Taste("Sweet", 3));
                stats.Add(new Taste("Sour", 2));
                break;
            case "Apple":
                stats.Add(new Taste("Sweet", 2));
                stats.Add(new Taste("Crunchy", 2));
                break;
            case "Egg":
                stats.Add(new Taste("Salty", 2));
                stats.Add(new Taste("Creamy", 3));
                stats.Add(new Taste("Crunchy", 3));
                break;
        }
        ingredient.Stats = stats;
        
        if ( Random.Range(0f, 1f) > spawnRate)
        {   
            gameObject.SetActive(false);
        }
    }
    public Ingredient PickUp()
    {
        
        Destroy(gameObject);
        return ingredient;
    } 
}
