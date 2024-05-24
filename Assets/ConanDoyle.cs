using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Xml.Linq;

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
    public Button nextButton;
    public string filepathScript;
    public Christe Agatha;

    // Set in start, needs to be updated whenever a speechbox is instantiated or destroyed
    public int noSpeechBoxes;

    // Start is called before the first frame update
    void Start()
    {
        // Populates string arrays with characters' dialogue
        // From a text file
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
        dialogue = File.ReadAllLines(filepathScript + "Dialogue.txt");
        /*print("Here are the contents of the file:");
        for (int i = 0; i < dialogue.Length; i++)
        {
            print("Line " + (i + 1) + ": ");
            print(dialogue[i]);
            print(" ");
        }*/

        options = File.ReadAllLines(filepathScript + "Options.txt");
        PathA = File.ReadAllLines(filepathScript + "PathA.txt");
        PathB = File.ReadAllLines(filepathScript + "PathB.txt");
        PathC = File.ReadAllLines(filepathScript + "PathC.txt");
        PathD = File.ReadAllLines(filepathScript + "PathD.txt");
        
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

        if (Agatha == null)
        {
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
        else
        {
            noSpeechBoxes = 0;
            Agatha.afterArthur();
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

    public void NewScene(string whichscene)
    {
        print("New Scene: " +  whichscene);
        nextButton.interactable = false;
    }

    
}
