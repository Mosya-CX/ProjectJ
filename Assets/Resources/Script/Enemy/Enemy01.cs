using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public override void ChangeToMatchlessMode()
    {
        isHighLight = false;
        currentHealthLetters = "~";
        letterImages[0].sprite = matchLessNormalSprite;
    }
    public override void ChangeToNormalMode()
    {
        ResetImage();
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
            letterImages[0].sprite = matchLessHighLightSprite;
        }
        isHighLight = true;
        currentHealthLetters = currentHealthLetters.Replace(keyPressed.ToString(), "");
    }
    public override void OnDeath()
    {
        base.OnDeath();
        if (IsMathchlessMode)
        {
            ChangeToNormalMode();
        }
    }
}
