using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpeechBox : MonoBehaviour
{

    public ConanDoyle Arthur;
    public string MyName = "Sherlock";
    public string[] MyLines;
    public int linecountCurrent;
    public TMP_Text MyMouth;
    public Canvas canvas;
    public UnityEngine.UI.Button nextButton;
    //public int totallinecount;
    public Vector2 textPosition;
    public Vector2 TopRight;
    public Vector2 BottomLeft;

    public Color32 textColor;
    public Color32 hiddenColor;

    public int catsup = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        textColor = MyMouth.color;
        hiddenColor = new Color32(149, 149, 149, 255);
        // Everything that would be here is called by ConanDoyle.Start()
        //MyLines = Arthur.Sherlock;
        //Debug.Log("Offsetmax: " + MyMouth.GetComponent<RectTransform>().offsetMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            //Debug.Log("Left-clicked on dialogue");
            // nextLine now called by "Next" button
            //this.nextLine();
        }
        if (Input.GetMouseButtonDown(1) == true)
        {
            Debug.Log("Right-clicked on dialogue");
        }
        if (Input.GetMouseButtonDown(2) == true)
        {
            Debug.Log("Middle-clicked on dialogue");
        }
    }

    // Moves the dialogue box into the correct position and size relative to the canvas
    public void RepositionLeft()
    {
        // Finds the height and width of the canvas
        RectTransform canvasposition = canvas.GetComponent<RectTransform>();
        float width = canvasposition.sizeDelta.x;
        float height = canvasposition.sizeDelta.y;
        //Debug.Log("width: " + width + ", height: " + height);
        // Calculates the appropriate (reduction in) height and width for the dialogue box
        double mywidth;
        double myheight;
        mywidth = 0 - (width * 0.1875);
        myheight = 0 - (height * 0.55);
        int topstretch = Convert.ToInt32(myheight);
        int rightstretch = Convert.ToInt32(mywidth);
        int borderstretch = 10;
        // Converts the stretch into appropriate format
        if(MyName == "Watson")
        {
            TopRight = new Vector2(rightstretch, topstretch);
            BottomLeft = new Vector2(borderstretch, borderstretch);
        }
        else
        {
            TopRight = new Vector2 ((0-borderstretch), topstretch);
            BottomLeft = new Vector2((0-rightstretch), borderstretch);
        }
        //Debug.Log("before transform, OffsetMax: " + this.GetComponent<RectTransform>().offsetMax + ", OffsetMin: " + this.GetComponent<RectTransform>().offsetMin);
        // Transforms the dialogue box
        this.GetComponent<RectTransform>().offsetMax = TopRight;
        this.GetComponent<RectTransform>().offsetMin = BottomLeft;
        //Debug.Log("after transform, OffsetMax: " + this.GetComponent<RectTransform>().offsetMax + ", OffsetMin: " + this.GetComponent<RectTransform>().offsetMin);
        
    }

    // Moves the box's text to line up with the box
    // Would do "MyMouth.GetComponent<RectTransform>().rect = this.GetComponent<RectTransform>().rect;" 
    // But can't cause is read-only
    public void RepositionText()
    {
        // Finds necessary dimensions and position of dialogue box
        RectTransform BoxTransform = this.GetComponent<RectTransform>();
        float width = BoxTransform.rect.width;
        float height = BoxTransform.rect.height;
        Vector2 Center = BoxTransform.rect.center;
        // Converts dimensions into appropriate format
        Vector2 TextTopCorner = new Vector2(width, height);
        Vector2 TextBottomCorner = new Vector2(20, 20);
        // Transforms the text's size
        MyMouth.GetComponent<RectTransform>().offsetMax = TextTopCorner;
        MyMouth.GetComponent<RectTransform>().offsetMin= TextBottomCorner;
        // Repositions the text relative to canvas
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        canvasWidth = canvasWidth / 2;
        canvasHeight = canvasHeight / 2;
        //Debug.Log("Canvas dimensions: " + canvasWidth + " x " + canvasHeight);
        float textWidth = MyMouth.GetComponent<RectTransform>().rect.width;
        float textHeight = MyMouth.GetComponent<RectTransform>().rect.height;
        textWidth = textWidth / 2;
        textHeight = textHeight / 2;
        float x;
        float y;
        if (MyName == "Watson")
        {
            x = 0 - (canvasWidth - textWidth - 20);
            y = 0 - (canvasHeight - textHeight - 20);
        }
        else
        {
            x = canvasWidth - textWidth - 20;
            y = 0 - (canvasHeight - textHeight - 20);
        }
        textPosition = new Vector2(x, y);
        //Debug.Log("Text Center: (" + x + ", " + y + ")");
        MyMouth.GetComponent<RectTransform>().anchoredPosition = textPosition;
    }


    // Finds the location of this character's dialogue for this scene, called by ConanDoyle.Start
    public void FindLines()
    {
        Arthur = FindObjectOfType<ConanDoyle>();
        MyLines = Arthur.currentdialogue;
    }

    // determines if this character is the first to speak and, if so, displays the character's line (called by ConanDoyle.Start)
    
    public void amISpeaking()
    {
        string thisLine = MyLines[linecountCurrent];
        string speakingName = getName(thisLine);
        string line = getLine(thisLine);
        // I think looping is the only time it shouldn't advance
        if (speakingName == MyName)
        {
            nextLine();
        }
        else if (speakingName == "NewScene")
        {
            Arthur.NewScene(line);
        }
        else
        {
            moveOut();
            if(line != "loop")
            {
                linecountCurrent++;
            }
        }
    }
    
    public void nextLine()
        /// THEN SHERLOCK RETURNS HERE? thisline = null, linecount = 12, then thisline  = "" because it's blank, so getline or getname return error as there's no colon
    {
        // Finds the current line of dialogue
        string thisLine = MyLines[linecountCurrent];
        //print("Line no " + linecountCurrent + ": " + thisLine);
        string line = getLine(thisLine);
        // Checks for dialogue choice
        if (line == "Choice")
        {
            linecountCurrent++;
            moveIn();
            runChoice();
        }
        // Checks for loop or end
        else if (line == "loop" && Arthur.options[Arthur.linecountOptions] == "loop")
        {
            moveIn();
            repeatChoice(this);
        }
        else if (line == "end" && Arthur.options[Arthur.linecountOptions] == "end")
        {
            linecountCurrent++;
            backToDialogue();
        }
        // Displays the current line of dialogue
        else
        {
            MyMouth.text = line;
            linecountCurrent++;
            moveIn();
        }
    }


    // Moves this dialogue box and its textbox into view
    private void moveIn()
    {
        this.GetComponent<RectTransform>().offsetMax = TopRight;
        this.GetComponent<RectTransform>().offsetMin = BottomLeft;
        MyMouth.GetComponent<RectTransform>().anchoredPosition = textPosition;
    }

    // Moves this dialogue box and its textbox out of view
    private void moveOut()
    {
        this.GetComponent<RectTransform>().offsetMax = new Vector2(-883, -220);
        this.GetComponent<RectTransform>().offsetMin = new Vector2(-723, 0);
        MyMouth.GetComponent<RectTransform>().anchoredPosition = new Vector2(-900, 0);
    }


    // Finds which character is saying a given line
    // Input is one string (by count) of the currently active dialogue string[] (e.g. dialogue[linecount])
    public string getName(string libretto)
    {
        int end = libretto.IndexOf(":");
        string output = libretto.Substring(0, end);
        //print(output);
        return output;
    }

    // Finds what the character is saying
    public string getLine(string libretto)
    {
        int start = libretto.IndexOf(":") + 1;
        string output = libretto.Substring(start, libretto.Length - start);
        //print(output);
        return output;
    }

    // Creates buttons for dialogue options
    private void runChoice()
    {
        DialogueButton[] buttons = new DialogueButton[100];
        int noofChoices = 0;
        // Runs from 0 to break for first dialogue choice, from end of previous dialogue choice to break for subsequent
        for (int i = Arthur.linecountOptions; i < 100; i++)
        {
            if(Arthur.options[i] == "end" | Arthur.options[i] == "loop")
                // i.e. when list of options is over
            {
                // counts how many options there were
                noofChoices = i - Arthur.linecountOptions - 1;
                // notes where the end of this set of options was (NB: stops on "end"/"loop" message, not on first option of next set)
                Arthur.linecountOptions = i;
                //print("end = options[" + i + "]");
                // breaks loop so doesn't overrun into next set of options or null section of list
                break;
            }
            // Makes a button and puts it in an array so rest of function can reference it
            int buttonPosition = i - Arthur.linecountOptions;
            buttons[buttonPosition] = Instantiate(Arthur.buttonPrefab);
            // Sets new button's parent as Canvas so it positions correctly
            buttons[buttonPosition].transform.SetParent(canvas.transform, false);
            // Finds this button's text component and prompts it to display one of the options
            // (due to array indexing, buttons[0] will display the first option and so on)
            TMP_Text buttext = buttons[buttonPosition].GetComponentInChildren<TMP_Text>();
            buttext.text = Arthur.options[i];
        }
        // Sets variables of created buttons
        // Again, indexing means buttons[0] has MyChoice = 0, is top of list, and displays 1st option, and so on for rest of buttons
        for(int i = 0; i < (noofChoices + 1); i++)
        {
            buttons[i].MyName = this.MyName;
            buttons[i].MyChoice = i;
            buttons[i].meBox = this;
            buttons[i].Reposition(noofChoices + 1);
            // Hides the text of the dialogue box so it isn't visible behind the buttons
        }
        MyMouth.color = hiddenColor;
        nextButton.interactable = false;
    }

    // Brings back buttons from a previous dialogue choice (chosen option greyed out)
    private void repeatChoice(SpeechBox talker)
    {
        moveInButtons();
        // Checks if all buttons are clicked... you'll see
        int noButtons = 0;
        int noClicked = 0;
        // Finds all dialogue option buttons
        DialogueButton[] buttons = FindObjectsOfType<DialogueButton>();
        foreach (DialogueButton button in buttons)
        {
            // Greys out and unclickables the previously-clicked button
            if (button.chosen == true)
            {
                UnityEngine.UI.Button butt = button.gameObject.GetComponent<UnityEngine.UI.Button>();
                // No longer necessary but is how to change button behaviour
                // butt.onClick.RemoveAllListeners();
                // butt.onClick.AddListener(button.nope);
                butt.interactable = false;
                noClicked++;
            }
            noButtons++;
        }
        talker.MyMouth.color = hiddenColor;
        nextButton.interactable = false;
        // WHAT TO DO IF ALL BUTTONS CLICKED
        if (noButtons > noClicked)
        {
            //print("Continue");
        }
        else if (noButtons == noClicked)
        {
            print("out of options");
            backToDialogue();
        }
    }

    // Destroys all currently-existing dialogue buttons
    public void KillButtons()
    {
        DialogueButton[] buttons = FindObjectsOfType<DialogueButton>();
        foreach(DialogueButton button in buttons)
        {
            if (button.meBox == this)
            {
                GameObject go = button.gameObject;
                Destroy(go);
            }
        }
    }

    private void backToDialogue()
    {
        Arthur.linecountPaths[Arthur.currentPath] = linecountCurrent;
        Arthur.currentdialogue = Arthur.dialogue;
        Arthur.currentPath = 1000;
        Arthur.linecountOptions++;
        SpeechBox[] boxes = FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox box in boxes)
        {
            box.linecountCurrent = Arthur.linecountDialogue;
            box.MyLines = Arthur.currentdialogue;
            box.MyMouth.color = box.textColor;
         //   box.amISpeaking();
        }
        nextButton.interactable = true;
        this.KillButtons();
        this.amISpeaking();
    }

    public void moveInButtons()
    {
        //print("buttons moved");
        DialogueButton[] buttons = FindObjectsOfType<DialogueButton>();
        foreach (DialogueButton button in buttons)
        {
            button.moveIn();
        }
    }


    // displays this character's next line of text
    // NO LONGER NECESSARY
    // NOW == FIRSTLINE, FIRSTLINE RENAMED nextLine
    /*public void NextLine()
    {
        if (Arthur.LineOrder[totallinecount] == MyName)
        {
            MyMouth.text = MyLines[linecount];
            linecount++;
            moveIn();
        }
        else
        {
            moveOut();
        }
        totallinecount++;
    }*/

}
