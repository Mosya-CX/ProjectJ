using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelaxTurn", menuName = "LevelSO/TurnInfo/RelaxTurn")]
public class PathFindingTurn : TurnData
{
    //public GameObject PathFindingPreBg;
    public float durationTime = 3;
    public float Timer;
    //Transform startPoint;
    //Transform MiddlePoint;
    //Transform endPoint;
    Vector3 startPos;
    Vector3 endPos;

    Vector3 speed;
    Transform curScene;

    public override GameObject OnCreate()
    {
        GameObject obj = base.OnCreate();
        Timer = 0;
        playerData.playerState = PlayerState.PathFinding;
        

        // 解除摄像机跟随

        startPos = TurnScene.transform.Find("Start").position;
        endPos = TurnScene.transform.Find("End").position;
        curScene = obj.transform;

        speed = (endPos - startPos) / durationTime;

        //startPoint = PathFindingPreBg.transform.Find("Start");
        //MiddlePoint = PathFindingPreBg.transform.Find("Middle");
        //endPoint = PathFindingPreBg.transform.Find("End");

        // 播放相应bgm

        return obj;
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
            curScene.transform.position -= speed * Time.deltaTime;

            Timer += Time.deltaTime;
        }

    }
    public override void OnDestory()
    {
        base.OnDestory();
    }

}
