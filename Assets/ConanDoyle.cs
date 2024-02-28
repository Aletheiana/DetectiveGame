using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConanDoyle : MonoBehaviour
{

    //public string[] Sherlock = new string[100];
    //public string[] Watson = new string[100];
    //public string[] Gregson = new string[100];
    //public string[] Lestrade = new string[100];
    //public string[] LineOrder = new string[100];
    public string[] currentdialogue;
    public string[] dialogue = new string[100];
    public int linecountDialogue;
    /*public string[] PathA = new string[100];
    public int linecountPathA;
    public string[] PathB = new string[100];
    public int linecountPathB;
    public string[] PathC = new string[100];
    public int linecountPathC;*/
    //public List<string[]> words; 

    // Start is called before the first frame update
    void Start()
    {
        /*words = new List<string[]>() { Sherlock, Gregson, Watson, Lestrade };
        this.Sherlock[0] = "Sherlock";
        this.Sherlock[1] = "Hello World";
        this.Sherlock[2] = "Oh hello Watson";
        this.Sherlock[3] = "I'm talking to the player";
        this.Sherlock[4] = "I assure you it does, but I'll desist if it makes you uncomfortable";
        this.Sherlock[5] = "Ah yes, the case";
        this.Sherlock[6] = "Thanks for playing";
        this.Watson[0] = "Watson";
        this.Watson[1] = "Who are you talking to?";
        this.Watson[2] = "Well that makes perfect sense(!)";
        this.Watson[3] = "It does, and I'm eager to hear of your progress on our case";
        this.Watson[4] = "Thanks for playing";
        this.Gregson[0] = "Gregson";
        this.Gregson[1] = "Welcome to Scotland Yard";
        this.Lestrade[0] = "Lestrade";
        this.Lestrade[1] = "Rachel";*/
        //HALFWAY THROUGH CHANGING THINGS, IS BROKEN RN
        /*this.LineOrder[0] = "Sherlock";
        this.LineOrder[1] = "Watson";
        this.LineOrder[2] = "Sherlock";
        this.LineOrder[3] = "Sherlock";
        this.LineOrder[4] = "Watson";
        this.LineOrder[5] = "Sherlock";
        this.LineOrder[6] = "Watson";
        this.LineOrder[7] = "Sherlock";
        this.LineOrder[8] = "Sherlock";
        this.LineOrder[9] = "Watson";*/
        // Populates string arrays with characters' dialogue
        dialogue[0] = "Sherlock:Hello World";
        dialogue[1] = "Watson:Who are you talking to?";
        dialogue[2] = "Sherlock:Oh hello Watson";
        dialogue[3] = "Sherlock:I'm talking to the player";
        dialogue[4] = "Watson:Well that makes perfect sense(!)";
        dialogue[5] = "Sherlock:I assure you it does, but I'll desist if it makes you uncomfortable";
        dialogue[6] = "Watson:It does, and I'm eager to hear of your progress on our case";
        dialogue[7] = "Sherlock:Ah yes, the case";
        dialogue[8] = "Sherlock:Thanks for playing";
        dialogue[9] = "Watson:Thanks for playing";
        // Sets non-branching dialogue as current dialogue
        currentdialogue = dialogue;
        //For brancing paths, use pathA[], pathB[], pathC[] etc, then when branch concluded return to dialogue[10] and onward


        SpeechBox[] speechBoxes = GameObject.FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox speechBox in speechBoxes)
        {
            // Positions the dialogue boxes and text relative to the canvas
            speechBox.RepositionLeft();
            speechBox.RepositionText();
            // Prompts dialogue boxes to find where their dialogue is and, if they're the first to speak, display the first line
            speechBox.linecountCurrent = 0;
            speechBox.FindLines();
            speechBox.nextLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}
