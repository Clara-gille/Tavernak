using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    [SerializeField] public NPC npc;

    Boolean isTalking = false;

    float distance;
    float curResponseTracker = 0;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject dialogueUI;
    [SerializeField] public GameObject commandPress;

    [SerializeField] public TextMeshProUGUI npcName;
    [SerializeField] public TextMeshProUGUI npcDialogueBox;
    [SerializeField] public TextMeshProUGUI playerResponse;
    void Start()
    {
        dialogueUI.SetActive(false);
        commandPress.SetActive(false);
    }

    void Update() //OnMouseOver
    {
        //check if the player is close enough to the npc
        distance = Vector3.Distance(player.transform.position, this.transform.position); 
        if(distance <= 2.5f)
        {
            commandPress.SetActive(true);
            //allow the choice of the player lines when scrolling the mouse scrollwheel
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;
                if(curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;
                if(curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }

            //trigger dialogue when key E is down
            if(Input.GetKeyDown(KeyCode.I) && isTalking == false) 
            {
                StartConversation();
            }
            else if(Input.GetKeyDown(KeyCode.I) && isTalking == true)
            {
                EndDialogue();
            }

            //modify the npc's answer after the choice of the player's line by pressing space bar
            //might need to optimize it later

            if(curResponseTracker == 0 && npc.playerDialogue.Length >= 0) 
            {
                playerResponse.text = npc.playerDialogue[0];
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if(curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }
        }
        else
        {
            EndDialogue();
        }
    }

    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        commandPress.SetActive(false);
        dialogueUI.SetActive(false);
        
    }
    
    public void ReceiveOrder(List<Ingredient> ingredients)
    {
        String wants = npc.order.taste;
        float satisfaction = 0;
        foreach (Ingredient ingredient in ingredients)
        {
            foreach (Taste taste in ingredient.Stats)
            {
                if (taste.Name == wants)
                {
                    satisfaction += taste.Value;
                }
            }
            {
                
            }
        }
    }

}
