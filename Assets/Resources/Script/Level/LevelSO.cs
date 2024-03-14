using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO/LevelInfo",menuName = "LevelInfo")]
public class LevelSO : ScriptableObject
{
    public int id;// 关卡id
    
    public GameObject PathFindingPreBg;// 寻路前景
    public GameObject PathFindingBg;// 寻路背景
    public List<TurnData> turnDataList = new List<TurnData>();// 存储关卡波次信息
}

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// 战斗场景
    public Vector3 BornPos;// 初始位置
    public PlayerController playerData;// 记录玩家信息
    public Vector2 ScenePos;
    public virtual void OnCreate()
    {
        // 重置玩家位置
        playerData.transform.position = BornPos;
        // 加载场景
        if (LevelManager.Instance.curScene != TurnScene)
        {
            Destroy(LevelManager.Instance.curScene);
            Instantiate(TurnScene, ScenePos, Quaternion.identity);
        }
        else
        {
            LevelManager.Instance.curScene.SetActive(true);
        }
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnDestory()
    {
        LevelManager.Instance.curTurn++;
    }

}

[CreateAssetMenu(fileName = "LevelSO/TurnInfo/StoryTurn", menuName = "StoryTurn")]
public class StoryTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
    }
}
[CreateAssetMenu(fileName = "LevelSO/TurnInfo/FightTurn", menuName = "FightTurn")]
public class FightTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
    }

    public override void OnDestory()
    {
        LevelManager.Instance.LoadPathFinding();
        base.OnDestory();
    }
}
[CreateAssetMenu(fileName = "LevelSO/TurnInfo/BossTurn", menuName = "BossTurn")]
public class BossTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
    }

}


