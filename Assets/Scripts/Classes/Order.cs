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
        public List<Taste> Stats;

        public Order(string pnjName, int orderNumber, List<Taste> stats)
        {
            PnjName = pnjName;
            OrderNumber = orderNumber;
            Stats = stats;
        }
    }