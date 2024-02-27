using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBox : MonoBehaviour
{

    public ConanDoyle Arthur;
    public int Character;
    public TMP_Text MyLines;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Finds the location of this character's dialogue for this scene
        Arthur = FindObjectOfType<ConanDoyle>();
        // Finds the text object which will display this character's text
        MyLines.text = Arthur.DialogueOne;
        // Moves the dialogue box into the correct position and size relative to the canvas
        RepositionLeft();
        // Moves the box's text to line up with the box
        RepositionText();
        //Debug.Log("Offsetmax: " + MyLines.GetComponent<RectTransform>().offsetMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Debug.Log("Left-clicked on dialogue");
            MyLines.text = Arthur.DialogueTwo;
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
    private void RepositionLeft()
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
        Vector2 TopRight = new Vector2(rightstretch, topstretch);
        Vector2 BottomLeft = new Vector2(borderstretch, borderstretch);
        Debug.Log("before transform, OffsetMax: " + this.GetComponent<RectTransform>().offsetMax + ", OffsetMin: " + this.GetComponent<RectTransform>().offsetMin);
        // Transforms the dialogue box
        this.GetComponent<RectTransform>().offsetMax = TopRight;
        this.GetComponent<RectTransform>().offsetMin = BottomLeft;
        Debug.Log("after transform, OffsetMax: " + this.GetComponent<RectTransform>().offsetMax + ", OffsetMin: " + this.GetComponent<RectTransform>().offsetMin);
        
    }

    // Moves the box's text to line up with the box
    // Would do "MyLines.GetComponent<RectTransform>().rect = this.GetComponent<RectTransform>().rect;" 
    // But can't cause is read-only
    private void RepositionText()
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
        MyLines.GetComponent<RectTransform>().offsetMax = TextTopCorner;
        MyLines.GetComponent<RectTransform>().offsetMin= TextBottomCorner;
        // Repositions the text relative to canvas
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        canvasWidth = canvasWidth / 2;
        canvasHeight = canvasHeight / 2;
        //Debug.Log("Canvas dimensions: " + canvasWidth + " x " + canvasHeight);
        float textWidth = MyLines.GetComponent<RectTransform>().rect.width;
        float textHeight = MyLines.GetComponent<RectTransform>().rect.height;
        textWidth = textWidth / 2;
        textHeight = textHeight / 2;
        float x = 0 - (canvasWidth - textWidth - 20);
        float y = 0 - (canvasHeight - textHeight - 20);
        Vector2 textCenter = new Vector2(x, y);
        Debug.Log("Text Center: (" + x + ", " + y + ")");
        MyLines.GetComponent<RectTransform>().anchoredPosition = textCenter;
    }

    
}
