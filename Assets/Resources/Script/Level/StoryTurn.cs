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
    public List<Dialogue> Lines;// �ı�����
    public List<AudioClip> Effects;// ��Ч
    public List<string> actorsName;
    public StoryTellingPanel storyTellingUI;
    public GameObject dialogueObj;// �󶨶Ի���Ԥ����
    public Vector3 offset;// ���öԻ���ƫ��λ��

    public float duationTime_PerTenWords;// ����ÿ10���ֵ�ͣ��ʱ�� 

    public StoryAct select_Act;
    public GameObject performConfigObj;// �ݳ�����

    public override GameObject OnCreate()
    {
        Debug.Log("׼����ʼ��StoryTurn");

        offset = new Vector3(2, 2, 0);
        duationTime_PerTenWords = 2.5f;

        GameObject obj = base.OnCreate();

        dialogueObj = Resources.Load("Prefab/Other/Dialogue") as GameObject;
        storyTellingUI = UIManager.Instance.OpenPanel(UIConst.StoryUI) as StoryTellingPanel;

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
        playerData.AttackArea.SetActive(false);
        // ������Ӧbgm

        // �Ŵ��������Ұ
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
        // �ظ��������Ұ
        Camera.main.orthographicSize = 6;

        UIManager.Instance.ClosePanel(UIConst.StoryUI);
        Destroy(performConfigObj);

        Debug.Log("׼������StoryTurn");
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
            Debug.LogWarning("performConfigObjΪ��");
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
                guider = Instantiate(Resources.Load("Prefab/Character/Guider") as GameObject);
            }
            return guider;
        }
        else if (name == "��")
        {
            Transform text = (UIManager.Instance.FindPanel(UIConst.StoryUI) as StoryTellingPanel).PlayerAside;
            return text.gameObject;
        }
        else if(name == "����")
        {
            GameObject tmp = GameObject.Find("Enemy01_StoryMode");
            if (tmp == null)
            {
                Debug.Log("δ�ҵ�����");
            }
            return tmp;
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
