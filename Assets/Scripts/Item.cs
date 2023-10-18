using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Item")] //create a new item in Unity
public class Item : ScriptableObject {

    public TileBase tile;
    public Sprite image;
}
