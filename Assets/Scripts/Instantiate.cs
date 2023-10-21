using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject InstantiatePrefab;
    void Start()
    {
        Instantiate(InstantiatePrefab, new Vector3(-3,2,16), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
