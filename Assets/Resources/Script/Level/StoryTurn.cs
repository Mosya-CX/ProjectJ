using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "StoryTurn", menuName = "LevelSO/TurnInfo/StoryTurn")]
public class StoryTurn : TurnData
{
    public List<Dialogue> Lines;// �ı�����
    public List<AudioClip> Effects;// ��Ч
    public List<string> actorsName;
    public StoryTellingPanel storyTellingUI;

    // ��ʱ��
    public float curTime;
    public float duationTime_PerTenWords;// ����ÿ10���ֵ�ͣ��ʱ�� 

    public PerformConfig performConfig;// �ݳ�����
    
    public override GameObject OnCreate()
    {
        GameObject obj = base.OnCreate();

 
        storyTellingUI = UIManager.Instance.OpenPanel(UIConst.StoryUI) as StoryTellingPanel;

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
        // ������Ӧbgm

        ProfilePerformConfig();

        return obj;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
    }

    public override void OnDestory()
    {
        base.OnDestory();
        UIManager.Instance.ClosePanel(UIConst.StoryUI);
    }


    public void ProfilePerformConfig()
    {
        performConfig.dialogue = Resources.Load("Prefab/Other/Dialogue") as GameObject;
        for (int i = 0;i < Lines.Count;i++)
        {
            performConfig.dialogueList.Enqueue(Lines[i]);
        }
        for (int i = 0; i < Effects.Count;i++)
        {
            performConfig.Effects.Enqueue(Effects[i]);
        }
        performConfig.storyTellingUI = storyTellingUI;
        for (int i = 0;i < actorsName.Count;i++)
        {
            performConfig.actors.Add(actorsName[i], FindSpeaker(actorsName[i]));
        }

    }
    public GameObject FindSpeaker(string name)
    {
        if (name == "������Ļ")
        {
            Transform text = (UIManager.Instance.FindPanel(UIConst.StoryUI) as StoryTellingPanel).BlackBoardText;
            return text.gameObject;
        }
        else if (name == "�ڽ�")
        {
            return playerData.gameObject;
        }
        else if (name == "���")
        {
            GameObject guider = GameObject.Find("Guider");
            if (guider == null)
            {
                guider = Instantiate(Resources.Load("Prefab/Charater/Guider") as GameObject);
            }
            return guider;
        }
        else if (name == "��")
        {
            Transform text = (UIManager.Instance.FindPanel(UIConst.StoryUI) as StoryTellingPanel).PlayerAside;
            return text.gameObject;
        }

        Debug.LogWarning("��Ч����:" + name);
        return null;
    }
}
[Serializable]
public class Dialogue
{
    public Dialogue(string speaker, string words)
    {
        this.speaker = speaker;
        this.words = words;
    }
    public string speaker;
    public string words;
}
