using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PerformConfig : MonoBehaviour
{
    public int index = 0;

    public StoryTellingPanel storyTellingUI;

    public GameObject dialogue;// 对话框预制体

    public Queue<Dialogue> dialogueList;// 台词

    public Dictionary<string, GameObject> actors;// 演员

    public Queue<AudioClip> Effects;// 音频

    public float duationTime_PerTenWords;// 设置每10个字的停留时间
    public float duationTime_PerWord;// 每个字的停留时间

    public GameObject speaker;// 记录当前说话人

    public Vector3 offset;// 设置对话框偏移位置

    public TextMeshProUGUI curDialogueNode;// 记录当前的文本内容

    // 计时器
    //public float curTime;

    public bool isOver;

    public AudioClip curPlayEffect;
    public AudioClip curPlayBGM;

    protected virtual void Awake()
    {
        isOver = false;
        dialogueList = new Queue<Dialogue>();
        actors = new Dictionary<string, GameObject>();
        Effects = new Queue<AudioClip>();
        speaker = null;
        curDialogueNode = null;
        curPlayEffect = null;
        curPlayBGM = null;

        
    }

    public virtual void Play()
    {
        //curTime += Time.deltaTime;
        Init();
    }

    public virtual void Init()
    {

    }

    public GameObject PrepareDialogueObj()
    {
        // 初始化聊天框
        GameObject dialogueObj = Instantiate(dialogue);
        curDialogueNode = dialogueObj.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        curDialogueNode.text = "";
        dialogueObj.transform.SetParent(speaker.transform, false);
        Vector3 realOffset = offset;
        if (speaker.transform.position.x <= 0)
        {
            realOffset.x = -realOffset.x;
        }
        else
        {
            dialogueObj.transform.Find("Bg").localScale = new Vector3 (-1, 1, 1);
        }
        dialogueObj.transform.position = realOffset;
        return dialogueObj;
    }
   
}


