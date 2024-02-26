using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBox : MonoBehaviour
{

    public ConanDoyle Arthur;
    public int Character;
    public TMP_Text MyLines;

    // Start is called before the first frame update
    void Start()
    {
        Arthur = FindObjectOfType<ConanDoyle>();
        MyLines.text = Arthur.DialogueOne;
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

    

    
}
