using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldsManager : MonoBehaviour
{
    private static int _golds = 0;
    [SerializeField] private TextMeshProUGUI goldsText;
    
    private void Start()
    {
        goldsText.text = _golds.ToString() + " gold";
    }
    

    public void AddGolds(int amount)
    {
        _golds += amount;
        goldsText.text = _golds.ToString() + " golds";
    }
    
    public bool RemoveGolds(int amount)
    {
        _golds -= amount;
        goldsText.text = _golds.ToString() + " golds";
        bool result = _golds < 0;
        if (result)
        {
            _golds = 0;
        }
        return result;
    }
}
