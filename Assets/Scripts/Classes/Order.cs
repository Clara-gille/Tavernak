using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "Order")] //create a new item in Unity
public class Order : ScriptableObject
    {
        public string PnjName { get; private set; }
        public int OrderNumber { get; private set; }
        
        public string taste;

        public Order(string pnjName, int orderNumber, string taste)
        {
            PnjName = pnjName;
            OrderNumber = orderNumber;
            this.taste = taste;
        }

    // COMPARER L ORDER ET LA RECETTE D INGREDIENTS
    public int Serve(Order order)
    {
        return 0;
    }
    }