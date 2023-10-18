using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Orders : ScriptableObject
{
    public class Order
    {
        public string PnjName;
        public int OrderNumber;
        public List<Taste> Stats;

        public Order(string pnjName, int orderNumber, List<Taste> stats)
        {
            PnjName = pnjName;
            OrderNumber = orderNumber;
            Stats = stats;
        }
    }
}