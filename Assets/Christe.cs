using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Christe : MonoBehaviour
{
    // References the ConanDoyle and Canvas and BackgroundKeeper
    public ConanDoyle Arthur;
    public Canvas canvas;
    public Background backgroundKeeper;

    // Holds the prefabs
    public SpeechBox speechPrefab;
    public TMP_Text TMPTextPrefab;
    public UnityEngine.UI.Button NextButtonPrefab;


    // Holds created speechboxes and next button
    public SpeechBox[] speechBoxes;
    public UnityEngine.UI.Button NextButton;

    // Holds everythuing pertinent to the question box which will display things
    public QuestionBox Questiony;
    public Vector2 TopRight;
    public Vector2 BottomLeft;
    private Vector2 AwayTR;
    private Vector2 AwayBL;
    
    // Holds all interactable objects in scene
    public GameObject[] interactables = new GameObject[10];
    public string[] interactablesname = new string[10];

    // Tracks assigned names
    public string[] names;
    public int noSpeakers;
    public int speakersAssigned;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void afterArthur()
    {
        TopRight = Questiony.GetComponent<RectTransform>().offsetMax;
        BottomLeft = Questiony.GetComponent<RectTransform>().offsetMin;
        float x = TopRight.x - 830;
        float y = TopRight.y;
        AwayTR = new Vector2(x, y);
        x = BottomLeft.x - 830;
        y = BottomLeft.y;
        AwayBL = new Vector2(x, y);
        string firstName = GetName(Arthur.dialogue[0]);
        if (firstName == "Question")
        {
            string question = GetLine(Arthur.dialogue[0]);
            Questiony.GetComponent<TMP_Text>().text = question;
        }
        if (firstName == "Dialogue")
        {
            string speakers = GetLine(Arthur.dialogue[0]);
            Questiony.GetComponent<RectTransform>().offsetMax = AwayTR;
            Questiony.GetComponent<RectTransform>().offsetMin = AwayBL;
            noSpeakers = int.Parse(speakers);
            names = new string[noSpeakers];
            print(noSpeakers + " people will be speaking");
            Arthur.linecountDialogue = 1;
            CreateSpeechBoxes(noSpeakers);
        }
    }

    private string GetName(string libretto)
    {
        int end = libretto.IndexOf(":");
        string output = libretto.Substring(0, end);
        //print(output);
        return output;
    }

    private string GetLine(string libretto)
    {
        int start = libretto.IndexOf(":") + 1;
        string output = libretto.Substring(start, libretto.Length - start);
        //print(output);
        return output;
    }
    public void CreateSpeechBoxes(int HowMany)
    {
        NextButton = Instantiate(NextButtonPrefab, canvas.transform, false);
        Arthur.nextButton = NextButton;
        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(nextBoxLine);
        NextButton.transform.position = (NextButton.transform.position + new Vector3(12, 164, 0));
        speechBoxes = new SpeechBox[HowMany];
        for (int i = 0; i < speechBoxes.Length; i++)
        {
            speechBoxes[i] = Instantiate(speechPrefab);
            speechBoxes[i].canvas = canvas;
            speechBoxes[i].transform.SetParent(canvas.transform, false);
            speechBoxes[i].linecountCurrent = 1;
            speechBoxes[i].MyName = "abc";
            speechBoxes[i].MyMouth = Instantiate(TMPTextPrefab,canvas.transform,false);
            speechBoxes[i].moveOut();
            speechBoxes[i].nextButton = NextButton;
        }
        speechBoxes[0].MyName = "Watson";
        speechBoxes[0].RepositionLeft();
        speechBoxes[0].RepositionText();
        speechBoxes[0].moveOut();
        names[0] = "Watson";
        speakersAssigned = 1;
    }
    public void nextBoxLine()
    {
        int linecount = speechBoxes[0].linecountCurrent;
        string name = GetName(Arthur.dialogue[linecount]);
        string line = GetLine(Arthur.dialogue[linecount]);
        if(name == "NewScene")
        {
            Arthur.NewScene(line);
        }
        else if (name == "Background")
        {
            backgroundKeeper.ChangeBackground(line);
            foreach (SpeechBox speech in speechBoxes)
            {
                speech.linecountCurrent++;
            }
        }
        else if (line == "choice")
        {
            // run choice but buttons in different shape
        }
        else if (line == "loop")
        {
            // still SpeechBox.RepeatChoice?
        }
        else if (line == "end")
        {
            // still SpeechBox.ReturntoDialogue?
        }
        else
        {
            bool spoke = false;
            for (int i = 0; i < speechBoxes.Length; i++)
            {
                speechBoxes[i].moveOut();
                if (speechBoxes[i].MyName == name)
                {
                    speechBoxes[i].MyMouth.text = line;
                    spoke = true;
                    speechBoxes[i].moveIn();
                }
                speechBoxes[i].linecountCurrent++;
            }
            if (spoke == false)
            {
                int a = speakersAssigned;
                speechBoxes[a].RepositionLeft();
                speechBoxes[a].RepositionText();
                speechBoxes[a].MyName = name;
                names[a] = name;
                speechBoxes[a].MyMouth.text = line;
                spoke = true;
                speakersAssigned = (a + 1);
            }
        }
    }


}
