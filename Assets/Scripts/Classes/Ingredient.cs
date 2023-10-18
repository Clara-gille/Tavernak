using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient")] //create a new item in Unity
public class Ingredients : ScriptableObject
{
    public class Ingredient
    {
        [SerializeField] public string Name { get; private set; } // "CERISE"
        [SerializeField] public Sprite Sprite { get; private set; } // "cerise.png" for sprite rendering in inventory
        public List<Taste> Stats= new List<Taste>(); // [["sweet", 1], ["sour", 2]]

        // bitter, acidic, salty, sour, spicy, sweet, creamy, crunchy should be enough ?

        public Ingredient(string name, Sprite sprite, List<Taste> stats)
        {
            Name = name;
            Sprite = sprite;
            Stats = stats;
        }
    }
}