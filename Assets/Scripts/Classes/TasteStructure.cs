using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Taste
{
    public Taste(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public string Name;
    public int Value;
}