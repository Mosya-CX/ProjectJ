using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// 场景
    public Vector3 BornPos;// 玩家初始位置
    public PlayerController playerData;// 记录玩家信息
    public Transform parent;
    public bool isOver;// 判断是否结束

    public virtual GameObject OnCreate()
    {
        // 重置玩家位置
        playerData.transform.position = BornPos;

        GameObject scene;
        // 加载场景
        if (!LevelManager.Instance.loadedScene.ContainsKey(TurnScene.name))
        {
            if (LevelManager.Instance.curScene != null)
            {
                LevelManager.Instance.curScene.SetActive(false);
            }
            scene = Instantiate(TurnScene, Vector3.zero, Quaternion.identity);// 生成到场景上
            scene.transform.SetParent(parent, false);
            scene.name = TurnScene.name;
            //LevelManager.Instance.curScene = scene;
        }
        else
        {
            scene = LevelManager.Instance.loadedScene[TurnScene.name];// 从字典中获取
            scene.SetActive(true);// 激活
            //LevelManager.Instance.curScene = scene;
            
        }
        isOver = false;
        return scene;
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
