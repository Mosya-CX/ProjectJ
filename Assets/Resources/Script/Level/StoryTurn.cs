using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryAct
{
    Lv1_P1,Lv1_P2, Lv1_P3, Lv1_P4, Lv1_P5, Lv1_P6, Lv1_P7, Lv1_P8, Lv1_P9, Lv1_P10, Lv1_P11, Lv1_P12, Lv1_P13,
}

[CreateAssetMenu(fileName = "StoryTurn", menuName = "LevelSO/TurnInfo/StoryTurn")]
public class StoryTurn : TurnData
{
    public List<Dialogue> Lines;// 文本内容
    public List<AudioClip> Effects;// 音效
    public List<string> actorsName;
    public StoryTellingPanel storyTellingUI;
    public GameObject dialogueObj;// 绑定对话框预制体
    public Vector3 offset;// 设置对话框偏移位置

    public float duationTime_PerTenWords;// 设置每10个字的停留时间 

    public StoryAct select_Act;
    public GameObject performConfigObj;// 演出配置

    public override GameObject OnCreate()
    {
        Debug.Log("准备初始化StoryTurn");

        offset = new Vector3(2, 2, 0);
        duationTime_PerTenWords = 2.5f;

        GameObject obj = base.OnCreate();

        dialogueObj = Resources.Load("Prefab/Other/Dialogue") as GameObject;
        storyTellingUI = UIManager.Instance.OpenPanel(UIConst.StoryUI) as StoryTellingPanel;

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
        playerData.AttackArea.SetActive(false);
        // 播放相应bgm

        // 放大摄像机视野
        Camera.main.orthographicSize = 4f;

        ProfilePerformConfig();

        performConfigObj.GetComponent<PerformConfig>().Play();

        return obj;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (performConfigObj == null)
        {
            return;
        }
        if (performConfigObj.GetComponent<PerformConfig>() == null)
        {
            return;
        }
        if (performConfigObj.GetComponent<PerformConfig>().isOver)
        {
            isOver = true;
            OnDestory();
            return;
        }

    }

    public override void OnDestory()
    {
        // 回复摄像机视野
        Camera.main.orthographicSize = 6;

        UIManager.Instance.ClosePanel(UIConst.StoryUI);
        Destroy(performConfigObj);

        Debug.Log("准备销毁StoryTurn");
        base.OnDestory();
        
        

    }


    public void ProfilePerformConfig()
    {
        performConfigObj = new GameObject();
        switch (select_Act)
        {
            case StoryAct.Lv1_P1:
                performConfigObj.AddComponent<Lv1_P1>();
                performConfigObj.name = "Lv1_P1";
                break;
            case StoryAct.Lv1_P2:
                performConfigObj.AddComponent<Lv1_P2>();
                performConfigObj.name = "Lv1_P2";
                break;
            case StoryAct.Lv1_P3:
                performConfigObj.AddComponent<Lv1_P3>();
                performConfigObj.name = "Lv1_P3";
                break;
            case StoryAct.Lv1_P4:
                performConfigObj.AddComponent<Lv1_P4>();
                performConfigObj.name = "Lv1_P4";
                break;
            case StoryAct.Lv1_P5:
                performConfigObj.AddComponent<Lv1_P6>();
                performConfigObj.name = "Lv1_P5";
                break;
            case StoryAct.Lv1_P6:
                performConfigObj.AddComponent<Lv1_P7>();
                performConfigObj.name = "Lv1_P6";
                break;
            case StoryAct.Lv1_P7:
                performConfigObj.AddComponent<Lv1_P7>();
                performConfigObj.name = "Lv1_P7";
                break;
            case StoryAct.Lv1_P8:
                performConfigObj.AddComponent<Lv1_P8>();
                performConfigObj.name = "Lv1_P8";
                break;
            case StoryAct.Lv1_P9:
                performConfigObj.AddComponent<Lv1_P9>();
                performConfigObj.name = "Lv1_P9";
                break;
            case StoryAct.Lv1_P10:
                performConfigObj.AddComponent<Lv1_P10>();
                performConfigObj.name = "Lv1_P10";
                break;
            case StoryAct.Lv1_P11:
                performConfigObj.AddComponent<Lv1_P11>();
                performConfigObj.name = "Lv1_P11";
                break;
            case StoryAct.Lv1_P12:
                performConfigObj.AddComponent<Lv1_P12>();
                performConfigObj.name = "Lv1_P12";
                break;
            case StoryAct.Lv1_P13:
                performConfigObj.AddComponent<Lv1_P13>();
                performConfigObj.name = "Lv1_P13";
                break;
        }

        if (performConfigObj == null)
        {
            Debug.LogWarning("performConfigObj为空");
            return;
        }

        PerformConfig performConfig = performConfigObj.GetComponent<PerformConfig>();

        performConfig.dialogue = Resources.Load("Prefab/Other/Dialogue") as GameObject;
        performConfig.duationTime_PerTenWords = duationTime_PerTenWords;
        performConfig.dialogue = dialogueObj;
        performConfig.offset = offset;
        performConfig.storyTellingUI = storyTellingUI;
        for (int i = 0;i < Lines.Count;i++)
        {
            performConfig.dialogueList.Enqueue(Lines[i]);
        }
        for (int i = 0; i < Effects.Count;i++)
        {
            performConfig.Effects.Enqueue(Effects[i]);
        }
        for (int i = 0;i < actorsName.Count;i++)
        {
            performConfig.actors.Add(actorsName[i], FindSpeaker(actorsName[i]));
        }
    }
    public GameObject FindSpeaker(string name)
    {
        if (name == "黑屏字幕")
        {
            Transform text = (UIManager.Instance.FindPanel(UIConst.StoryUI) as StoryTellingPanel).BlackBoardText;
            return text.gameObject;
        }
        else if (name == "乌酱")
        {
            return playerData.gameObject;
        }
        else if (name == "香草")
        {
            GameObject guider = GameObject.Find("Guider");
            if (guider == null)
            {
                guider = Instantiate(Resources.Load("Prefab/Character/Guider") as GameObject);
            }
            return guider;
        }
        else if (name == "你")
        {
            Transform text = (UIManager.Instance.FindPanel(UIConst.StoryUI) as StoryTellingPanel).PlayerAside;
            return text.gameObject;
        }
        else if(name == "团子")
        {
            GameObject tmp = GameObject.Find("Enemy01_StoryMode");
            if (tmp == null)
            {
                Debug.Log("未找到团子");
            }
            return tmp;
        }
        
        Debug.LogWarning("无效名字:" + name);
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
