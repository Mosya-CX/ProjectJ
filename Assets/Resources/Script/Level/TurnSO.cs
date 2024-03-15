using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        playerData.playerState = PlayerState.None;
    }

}
