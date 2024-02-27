using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConanDoyle : MonoBehaviour
{

    public string[] Sherlock = new string[100];
    public string[] Watson = new string[100];
    public string[] Gregson = new string[100];
    public string[] Lestrade = new string[100];
    public string[] LineOrder = new string[400];
    public List<string[]> words; 

    // Start is called before the first frame update
    void Start()
    {
        words = new List<string[]>() { Sherlock, Gregson, Watson, Lestrade };
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
        this.Lestrade[1] = "Rachel";
        this.LineOrder[0] = "Sherlock";
        this.LineOrder[1] = "Sherlock";
        this.LineOrder[2] = "Watson";
        this.LineOrder[3] = "Watson";
        this.LineOrder[4] = "Sherlock";
        this.LineOrder[5] = "Sherlock";
        this.LineOrder[6] = "Watson";
        this.LineOrder[7] = "Sherlock";
        this.LineOrder[8] = "Watson";
        this.LineOrder[9] = "Sherlock";
        this.LineOrder[10] = "Sherlock";
        this.LineOrder[11] = "Watson";
        SpeechBox[] speechBoxes = GameObject.FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox speechBox in speechBoxes)
        {
            speechBox.FindLines();
            speechBox.linecount = 0;
            speechBox.RepositionLeft();
            speechBox.RepositionText();
            speechBox.FirstLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}
