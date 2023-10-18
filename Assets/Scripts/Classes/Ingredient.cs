using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Ingredients : ScriptableObject
{
    public class Ingredient
    {
        public string Name { get; private set; } // "CERISE"
        public string Sprite { get; private set; } // "cerise.png" for sprite rendering in inventory
        public List<Taste> Stats= new List<Taste>(); // [["sweet", 1], ["sour", 2]]

        // bitter, acidic, salty, sour, spicy, sweet, creamy, crunchy should be enough ?

        public Ingredient(string name, string sprite, List<Taste> stats)
        {
            Name = name;
            Sprite = sprite;
            Stats = stats;
        }
    }
}