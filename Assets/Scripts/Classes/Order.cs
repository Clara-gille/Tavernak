using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[Serializable] public class Orders : ScriptableObject
{
    public class Order
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
}