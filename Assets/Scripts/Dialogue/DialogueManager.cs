using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private NPC npc;

    Boolean isTalking = false;

    float distance;
    int curResponseTracker = 0;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject commandPress;

    private String state;
    private int currentLine = 0;

    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcDialogueBox;
    [SerializeField] private TextMeshProUGUI playerResponse;

    [SerializeField] private GameObject NpcSpawner;
    private RandomNpcSpawn npcSpawner;
    void Start()
    {
        dialogueUI.SetActive(false);
        commandPress.SetActive(false);
        npcSpawner = NpcSpawner.GetComponent<RandomNpcSpawn>();
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
                    PlayerControllerIndoor playerActions = player.GetComponent<PlayerControllerIndoor>();
                    List<Ingredient> soupIngredients = playerActions.GetSoupIngredients();
                    ReceiveOrder(soupIngredients);
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
            List<Taste> stats = DetermineTaste(ingredient.Name);
            foreach (Taste taste in stats)
            {
                if (taste.Name == wants)
                {
                    satisfaction += taste.Value;
                }
            }
        }
        
        if (satisfaction >= 3)
        {
            npcDialogueBox.text = "Wonderful! I'll give you " + satisfaction + " coins!";
        }
        else
        {
            npcDialogueBox.text = "Ewwww :( I'll give you only " + satisfaction + " coins...";
        }
        
        GoldsManager goldsManager = player.GetComponent<GoldsManager>();
        goldsManager.AddGolds((int) satisfaction);
        StartCoroutine(WaitBeforeLeaving());
    }

    IEnumerator WaitBeforeLeaving()
    {
        playerResponse.text = "";
        yield return new WaitForSeconds(3);
        EndDialogue();
        state = "arrived";
        curResponseTracker = 0;
        currentLine = 0;
        npcSpawner.SwitchNPC();
    }
    
    private void LeaveEarly()
    {
        npcDialogueBox.text = "Oh... I'll give you 0 coins then...and complain to ALL my friends and your manager you aberrant CLOWN !";
        StartCoroutine(WaitBeforeLeaving());
    }
    
    private List<Taste> DetermineTaste(String mName)
    {
        List<Taste> stats = new List<Taste>();
        switch (mName)
        {
            case "Mushroom":
                stats.Add(new Taste("Salty", 2));
                stats.Add(new Taste("Creamy", 1));
                stats.Add(new Taste("Crunchy", 1));
                break;
            case "Strawberry":
                stats.Add(new Taste("Sweet", 3));
                stats.Add(new Taste("Sour", 2));
                break;
            case "Apple":
                stats.Add(new Taste("Sweet", 2));
                stats.Add(new Taste("Crunchy", 2));
                break;
            case "Egg":
                stats.Add(new Taste("Salty", 2));
                stats.Add(new Taste("Creamy", 3));
                stats.Add(new Taste("Crunchy", 3));
                break;
            case "Meat" :
                stats.Add(new Taste("Salty", 3));
                stats.Add(new Taste("Crunchy", 1));
                break;
        }
        return stats;
    }

}

