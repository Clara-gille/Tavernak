using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNpcSpawn : MonoBehaviour
{
    public GameObject[] myObjects;
    int randomIndex;

    Boolean isShowing;

    private void Awake()
    {
        Hide();
        randomIndex = UnityEngine.Random.Range(0, myObjects.Length);
        Debug.Log("HideStart");
    }
    void Start()
    {
        Debug.Log("Update");
        GameObject myObject = myObjects[randomIndex];
        Debug.Log(randomIndex);
        myObject.SetActive(true);
        Debug.Log("Spawn");
    }

    void Hide()
    {
        isShowing = false;
        for (int i = 0; i < myObjects.Length; i++)
        {
            myObjects[i].SetActive(false);
        }
    }
}
