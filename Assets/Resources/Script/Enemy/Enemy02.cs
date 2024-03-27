using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : Enemy
{
    public Image matchlessImage;
    public override void Awake()
    {
        base.Awake();
        matchlessImage.enabled = false;
    }
    public override void ChangeToMatchlessMode()
    {
        isHighLight = false;
        currentHealthLetters = "~";
        matchlessImage.sprite = matchLessNormalSprite;
        for (int i = 0; i < originalHealthLetters.Length; i++)
        {
            letterImages[i].enabled = false;
        }
        matchlessImage.enabled = true;
    }
    public override void ChangeToNormalMode()
    {
        matchlessImage.enabled=false;
        ResetImage();
        for (int i = 0; i < originalHealthLetters.Length; i++)
        {
            letterImages[i].enabled = true;
        }
    }
    public override void HighLightLetter(char keyPressed)
    {
        Debug.Log("enter3");
        if (!IsMathchlessMode)
        {
            int index = originalHealthLetters.IndexOf(keyPressed);
            letterImages[index].sprite = highLightLetterDict[keyPressed];
        }
        else
        {
            matchlessImage.sprite = matchLessHighLightSprite;
        }
        isHighLight = true;
        currentHealthLetters = currentHealthLetters.Replace(keyPressed.ToString(), "");
    }
}
