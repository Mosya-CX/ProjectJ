using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformConfig : ScriptableObject
{
    public int index = 0;

    public StoryTellingPanel storyTellingUI;

    public GameObject dialogue;

    public Queue<Dialogue> dialogueList;

    public Dictionary<string, GameObject> actors;

    public Queue<AudioClip> Effects;// “Ù–ß

    public virtual void Play()
    {

    }
}

