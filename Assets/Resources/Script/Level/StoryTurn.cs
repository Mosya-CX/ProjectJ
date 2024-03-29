using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryTurn", menuName = "LevelSO/TurnInfo/StoryTurn")]
public class StoryTurn : TurnData
{
    public List<string> Dialogue;
    int index;
    public override GameObject OnCreate()
    {
        GameObject obj = base.OnCreate();

        index = 0;

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
        // ������Ӧbgm

        return obj;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (index >= Dialogue.Count)
        {
            OnDestory();
        }
        else
        {
            // ��Ӧ�����ƽ��߼�


            index++;
        }

    }

    public override void OnDestory()
    {
        base.OnDestory();
    }
}