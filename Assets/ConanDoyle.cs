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
    public List<string[]> words; 

    // Start is called before the first frame update
    void Start()
    {
        words = new List<string[]>() { Sherlock, Gregson, Watson, Lestrade };
        this.Sherlock[0] = "Sherlock";
        this.Sherlock[1] = "Hello World";
        this.Sherlock[2] = "Hello me";
        this.Sherlock[3] = "Hello Watson";
        this.Sherlock[4] = "Thanks for playing";
        this.Watson[0] = "Watson";
        this.Watson[1] = "Why are you reading my diary?";
        this.Gregson[0] = "Gregson";
        this.Gregson[1] = "Welcome to Scotland Yard";
        this.Lestrade[0] = "Lestrade";
        this.Lestrade[1] = "Rachel";
        SpeechBox[] speechBoxes = GameObject.FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox speechBox in speechBoxes)
        {
            speechBox.FindLines();
            speechBox.linecount = 0;
            speechBox.NextLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}
