using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : ScriptableObject
{
    [Serializable] public class Ingredient
    {
        public string Name { get; private set; } // "CERISE"
        public Sprite Sprite { get; private set; } // "cerise.png" for sprite rendering in inventory
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