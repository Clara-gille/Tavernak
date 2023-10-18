using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ingredient")] //create a new item in Unity
public class Ingredient : ScriptableObject
{
    public string Name; // "CERISE"
    public Sprite Img; // "cerise.png" for sprite rendering in inventory
    public List<Taste> Stats= new List<Taste>(); // [["sweet", 1], ["sour", 2]]

    // bitter, acidic, salty, sour, spicy, sweet, creamy, crunchy should be enough ?

    public Ingredient(string name, Sprite img, List<Taste> stats)
    {
        Name = name;
        Img = img;
        Stats = stats;
    }
}