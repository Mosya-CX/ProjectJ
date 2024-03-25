using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelaxTurn", menuName = "LevelSO/TurnInfo/RelaxTurn")]
public class PathFindingTurn : TurnData
{
    public GameObject PathFindingPreBg;
    public float durationTime = 3;
    float Timer;
    Transform startPoint;
    Transform endPoint;
    public override void OnCreate()
    {
        base.OnCreate();
        Timer = 0;
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.PathFinding;
        startPoint = PathFindingPreBg.transform.Find("Start");
        endPoint = PathFindingPreBg.transform.Find("End");
        // 播放相应bgm

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // 计时
        if (Timer >= durationTime)
        {
            OnDestory();
        }
        else
        {
            Timer += Time.deltaTime;
        }

    }
    public override void OnDestory()
    {
        base.OnDestory();
    }

}
