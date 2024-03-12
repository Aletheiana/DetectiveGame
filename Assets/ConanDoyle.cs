using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    public string[] options = new string[100];
    public int linecountOptions;
    public DialogueButton buttonPrefab;
    public string[] PathA = new string[100];
    public string[] PathB = new string[100];
    public string[] PathC = new string[100];
    public string[] PathD = new string[100];
    public List<string[]> ABC123;
    public int[] linecountPaths = new int[26];
    public int currentPath;

    // Set in start, needs to be updated whenever a speechbox is instantiated or destroyed
    public int noSpeechBoxes;

    // Start is called before the first frame update
    void Start()
    {
        
        // Populates string arrays with characters' dialogue
        /* INSTRUCTIONS:
                        Name of character speaking goes at start of string
                        Name separated from speech by a colon ":"
                        To give a choice, put "Choice" after the colon
                        Put what you want on the buttons in the "options" string array
                        Number of entries in options[] = number of buttons
                        End list of options with "end" for choice which returns to normal dialogue after branched "Path" dialogue
                        End list of options with "loop" for choic which returns to buttons screen after branched "Path" dialogue
                        If choose "loop", paths must also end with "loop"
        */
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
        dialogue[10] = "Watson:Choice";
        dialogue[11] = "Watson:Wow, that was quite the conversation!";
        options[0] = "Talk to Sherlock";
        options[1] = "Talk to Gregson";
        options[2] = "Talk to Lestrade";
        options[3] = "loop";
        PathA[0] = "Sherlock:I don't expect it to be very interesting, a simple murder";
        PathA[1] = "Watson:sldkalsdk";
        PathA[2] = "Sherlock:eiqoepruqeiortuqpoerip";
        PathA[3] = "Sherlock:Yeah, I can't think what to say either";
        PathA[4] = "Watson:loop";
        PathB[0] = "Sherlock:Pretend I'm Gregson, Welcome to the crime scene";
        PathB[1] = "Watson:Why are we pretending?";
        PathB[2] = "Sherlock:Maddy hasn't made a dialogue box for Gregson yet";
        PathB[3] = "Watson:loop";
        PathC[0] = "Sherlock:Or Lestrade, for that matter";
        PathC[1] = "Watson:Huh? And where is Lestrade, i asked to talk to him";
        PathC[2] = "Sherlock:Go back and choose to talk to Gregson, I'll explain there";
        PathC[3] = "Watson:loop";
        PathD[0] = "Sherlock:You've gone the wrong way, try again";
        PathD[1] = "Watson:loop";
        // Sets non-branching dialogue as current dialogue (currentPath 1000 is code for dialogue)
        currentdialogue = dialogue;
        currentPath = 1000;
        linecountDialogue = 0;
        linecountOptions = 0;
        //For brancing paths, use pathA[], pathB[], pathC[] etc, then when branch concluded return to dialogue[10] and onward

        linecountPaths[0] = 0;
        linecountPaths[1] = 0;
        linecountPaths[2] = 0;
        linecountPaths[3] = 0;
        ABC123 = new List<string[]> { PathA, PathB, PathC, PathD };

        noSpeechBoxes = 0;
        SpeechBox[] speechBoxes = GameObject.FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox speechBox in speechBoxes)
        {
            // Positions the dialogue boxes and text relative to the canvas
            speechBox.RepositionLeft();
            speechBox.RepositionText();
            // Prompts dialogue boxes to find where their dialogue is and, if they're the first to speak, display the first line
            speechBox.linecountCurrent = 0;
            speechBox.FindLines();
            speechBox.amISpeaking();
            noSpeechBoxes++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextBoxLine()
    {
        SpeechBox[] speechBoxes = GameObject.FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox speechBox in speechBoxes)
        {
            speechBox.amISpeaking();
        }
    }
    

    
}
