using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// 场景
    public Vector3 BornPos;// 初始位置
    public PlayerController playerData;// 记录玩家信息
    public Vector2 ScenePos;
    public virtual void OnCreate()
    {
        // 重置玩家位置
        playerData.transform.position = BornPos;
        // 加载场景
        if (!LevelManager.Instance.loadedScene.ContainsKey(TurnScene.name))
        {
            if (LevelManager.Instance.curScene != null)
            {
                LevelManager.Instance.curScene.SetActive(false);
            }
            GameObject scene = Instantiate(TurnScene, ScenePos, Quaternion.identity);// 生成到场景上
            scene.name = TurnScene.name;
            LevelManager.Instance.curScene = scene;
        }
        else
        {
            GameObject scene = LevelManager.Instance.loadedScene[TurnScene.name];// 从字典中获取
            scene.SetActive(true);// 激活
            LevelManager.Instance.curScene = scene;
        }


    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnDestory()
    {
        LevelManager.Instance.curTurn++;
        playerData.playerState = PlayerState.None;

        LevelManager.Instance.LoadTurn();// 加载下一个Turn
    }

}
