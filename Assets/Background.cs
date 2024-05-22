using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public SpriteRenderer myRenderer;
    public string[] backgroundName = new string[10];
    public Sprite[] backgroundSprites = new Sprite[10];
    public string currentBackground;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBackground(string newBackground)
    {
        for (int i = 0; i < backgroundName.Length; i++)
        {
            if (backgroundName[i] == newBackground)
            {
                myRenderer.sprite = backgroundSprites[i];
                currentBackground = newBackground;
            }
        }

    }
}
