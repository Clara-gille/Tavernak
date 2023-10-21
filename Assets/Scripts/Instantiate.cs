using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public DialogueManager InstantiatePrefab;
    void Start()
    {
        DialogueManager obj = Instantiate(InstantiatePrefab, new Vector3(-3, 2, 16), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Test pour + tard
    /* public GameObject InstantiatePrefab;
    public int npcCount;
    void Start()
    {
        for (int i = 0; i < npcCount; i++)
        {
            CreateNpc();
        }
       
    }

    private void CreateNpc(Order order)
    {
        Vector3 position = Random.insideUnitSphere; // new Vector3(-3, 2, 16)
        GameObject obj = Instantiate(InstantiatePrefab, position, Quaternion.identity);
        obj.GetComponent<MeshRenderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f,1f));
    }
    */
}
