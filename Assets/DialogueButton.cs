using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public string MyName = "Watson";
    public int MyChoice = 0;
    public SpeechBox meBox;
    public bool chosen;
    
    // Start is called before the first frame update
    void Start()
    {
        chosen = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) == true)
        {
            meBox = FindmeBox();
            Reposition(3);
        }*/
    }

    // Finds the dialogue box with the same character as this button
    public SpeechBox FindmeBox()
    {
        SpeechBox[] speechBoxes = FindObjectsOfType<SpeechBox>();
        foreach(SpeechBox speechBox in speechBoxes)
        {
            if(speechBox.MyName == MyName)
            {
                return speechBox;
            }
        }
        return null;
    }

    // Positions the button relative to the dialogue box and according to which choice it is out of however many choices there are
    public void Reposition(int noChoices)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        // Sets button Width equal to text width
        RectTransform textTeansform = meBox.MyMouth.GetComponent<RectTransform>();
        float width = textTeansform.sizeDelta.x;
        // Calculates button height according to number of choices and text height
        float textheight = textTeansform.sizeDelta.y;
        float height = textheight / noChoices;
        rectTransform.sizeDelta = new Vector2(width, height);
        // Repositions button according to text position and which choice it is
        float texty = meBox.textPosition.y;
        float x = meBox.textPosition.x;
        double moveUp;
        if(noChoices%2 == 0)
        {
            moveUp = noChoices / 2;
            moveUp = moveUp - 0.5;
        }
        else if(noChoices%2 == 1) 
        {
            moveUp = noChoices + 1;
            moveUp = moveUp / 2;
            moveUp = moveUp - 1;
        }
        else
        {
            print("error, noChoices neither odd nor even, moveUp set to 0, noChoices = " + noChoices);
            moveUp = 0;
        }
        
        //print("moving up by: " + moveUp + " spaces for " + noChoices + " choices");
        double topy = texty + (moveUp * height);
        double myY;
        for (int i = 0; i < noChoices; i++ )
        {
            myY = topy - (i * height);
            float myyy = Convert.ToInt32(myY);
            if (i == MyChoice)
            {
                rectTransform.anchoredPosition = new Vector2(x, myyy);
                //print("yes: " + myyy);
            }
            //else { print("no: " + myyy); }
        }

    }

    public void optionChosen()
    {
        // Both boxes need to update linecount current
        chosen = true;
        if(meBox.Arthur.currentPath == 1000)
        {
            meBox.Arthur.linecountDialogue = meBox.linecountCurrent;
        }
        else
        {
            meBox.Arthur.linecountPaths[meBox.Arthur.currentPath] = meBox.linecountCurrent;
        }
        meBox.Arthur.currentdialogue = meBox.Arthur.ABC123[MyChoice];
        meBox.Arthur.currentPath = MyChoice;
        SpeechBox[] boxes = FindObjectsOfType<SpeechBox>();
        foreach (SpeechBox box in boxes)
        {
            box.linecountCurrent = box.Arthur.linecountPaths[MyChoice];
            box.MyLines = box.Arthur.currentdialogue;
        }
        meBox.MyMouth.color = meBox.textColor;
        meBox.nextButton.interactable = true;
        if (meBox.Arthur.options[meBox.Arthur.linecountOptions] == "loop")
        {
            //print("loop");
            DialogueButton[] buttons = FindObjectsOfType<DialogueButton>();
            foreach (DialogueButton button in buttons) { button.moveOut(); }
        }
        else if(meBox.Arthur.options[meBox.Arthur.linecountOptions] == "end")
        {
            //print("end");
            meBox.KillButtons();
        }
        meBox.Arthur.nextBoxLine();
    }

    public void moveOut()
    {

        RectTransform rectTransform = GetComponent<RectTransform>();
        float x = rectTransform.anchoredPosition.x;
        float y = rectTransform.anchoredPosition.y;
        x = x - 700;
        rectTransform.anchoredPosition = new Vector2(x, y);
    }

    public void moveIn()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float x = rectTransform.anchoredPosition.x;
        float y = rectTransform.anchoredPosition.y;
        x = x + 700;
        rectTransform.anchoredPosition = new Vector2(x, y);
    }

    

    /*public void nope()
    {
        Debug.Log("This Button Already Pressed");
    }*/

}
