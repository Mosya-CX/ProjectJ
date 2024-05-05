using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PerformConfig : MonoBehaviour
{
    public int index = 0;

    public StoryTellingPanel storyTellingUI;

    public GameObject dialogue;// �Ի���Ԥ����

    public Queue<Dialogue> dialogueList;// ̨��

    public Dictionary<string, GameObject> actors;// ��Ա

    public Queue<AudioClip> Effects;// ��Ƶ

    public float duationTime_PerTenWords;// ����ÿ10���ֵ�ͣ��ʱ��
    public float duationTime_PerWord;// ÿ���ֵ�ͣ��ʱ��

    public GameObject speaker;// ��¼��ǰ˵����

    public Vector3 offset;// ���öԻ���ƫ��λ��

    public TextMeshProUGUI curDialogueNode;// ��¼��ǰ���ı�����

    // ��ʱ��
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
        // ��ʼ�������
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


