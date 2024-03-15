using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossTurn", menuName = "LevelSO/TurnInfo/BossTurn")]
public class BossTurn : TurnData
{
    public GameObject BossPrefab;
    public List<EnemyOrder> enemyOrder;// 出怪的顺序
    int curOrder;// 记录当前波次
    public GameObject enemySpwanPoints;// 记录战斗场景的所有刷怪点的父节点
    public override void OnCreate()
    {
        base.OnCreate();
        curOrder = 0;
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // 播放相应bgm

        // 加载boss

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // 生成敌人


    }

}

[Serializable]
public class EnemyOrder
{
    public int enemy1Num;
    public int enemy2Num;
    public int enemy3Num;
}