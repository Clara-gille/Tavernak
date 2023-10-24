using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomNpcSpawn : MonoBehaviour
{
    [FormerlySerializedAs("PNJs")]  public GameObject[] NPCs;
    int randomIndex;
    int currentIndex;

    
    void Start()
    {
        randomIndex = UnityEngine.Random.Range(0, NPCs.Length);
        currentIndex = randomIndex;
        NPCs[randomIndex].SetActive(true);
    }

    public void SwitchNPC()
    {
        NPCs[currentIndex].SetActive(false);
        StartCoroutine(SpawnRandom());
    }

    IEnumerator SpawnRandom()
    {
        yield return new WaitForSeconds(3);
        while(randomIndex == currentIndex) //to avoid spawning the same NPC
        {
            randomIndex = UnityEngine.Random.Range(0, NPCs.Length);
        }
        currentIndex = randomIndex;
        NPCs[randomIndex].SetActive(true);
    }
}
