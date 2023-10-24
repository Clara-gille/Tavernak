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
    int curResponseTracker = 0;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject dialogueUI;
    [SerializeField] public GameObject commandPress;

    private String state;
    private int currentLine = 0;

    [SerializeField] public TextMeshProUGUI npcName;
    [SerializeField] public TextMeshProUGUI npcDialogueBox;
    [SerializeField] public TextMeshProUGUI playerResponse;
    void Start()
    {
        dialogueUI.SetActive(false);
        commandPress.SetActive(false);
        state = "arrived";
    }

    void Update() //OnMouseOver
    {
        //check if the player is close enough to the npc
        distance = Vector3.Distance(player.transform.position, this.transform.position); 
        if(distance <= 2.5f)
        {
            commandPress.SetActive(true);
            //trigger dialogue when key I is down
            if(Input.GetKeyDown(KeyCode.I) && isTalking == false) 
            {
                StartConversation();
            }
            else if(Input.GetKeyDown(KeyCode.I) && isTalking == true)
            {
                EndDialogue();
            }
    
            if (state == "arrived")
            {
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
            
                //modify the npc's answer after the choice of the player's line by pressing space bar
                //might need to optimize it later
            
                if(curResponseTracker == 0) 
                {
                    playerResponse.text = "Say : \"" +  npc.playerDialogue[0] + " \"";
                    if(Input.GetKeyDown(KeyCode.Space) && isTalking)    
                    {
                        currentLine = 1;
                        state = "waiting";
                        npcDialogueBox.text = npc.dialogue[currentLine];
                        playerResponse.text = "Say : \"Here you are! \" (and give the soup)";
                    }
                }
                else 
                {
                    playerResponse.text = "Say : \"" +  npc.playerDialogue[1] + " \"";
                    if (Input.GetKeyDown(KeyCode.Space) && isTalking)
                    {
                        LeaveEarly();
                    }
                }
            }
            else if (state == "waiting")
            {
                if (Input.GetKeyDown(KeyCode.Space) && isTalking)
                {
                    Debug.Log("Here you are");
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
        npcDialogueBox.text = npc.dialogue[currentLine];
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

    private void LeaveEarly()
    {
        Debug.Log("left early");
    }

}
